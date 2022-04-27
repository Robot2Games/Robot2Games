using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour {
	[Header("Gun Configuration")] // Config da arma.
	public float damage; 
	public float range;		//Alcance.
	public float firerate;  //Quantas balas atira por segundo.
	public float waitToFirerate; //Tempo de espera entre disparo.
	public Camera cam;   // Como o seu Player vê o mundo?.
	public ParticleSystem ammoParticle; //Visual Effects do disparo da arma
	public GameObject impact; //Impacto do Tiro.
	public bool hold = false; // Carregar botão de atirar (falso).
	public GameObject BulletHole; //Tiro nos objects.


	[Space]
	[Header ("Ammo")] // Config da munição.
	public int maxammoPent; //Muniçao Máxima
	public int ammoPent; //Muniçao no Pente
	public int ammo; //Muniçao
	public int timeRecharge; //Tempo de recarga

	[Space]
	[Header ("Canvas")]	//Config do HUD da barra de recarga (que só aparece no momento do carregamento,nesse caso, o Slider), e texto da bala.
	public Text ammotext;
	public Slider rechargeShow; //Mostrar slider de recarga
	public GameObject rechargeGo; //Pegar o Obj de recarga (slider)

	[Header ("Audio")]
	public GameObject Fire; //Qual é o som que arma faz quando dispara???
	public GameObject FireNoBullet; //Sem balas faz um barulhinho diferente.
	public AudioSource Reload; // Sound de quando trocamos o carregador.

	[Header ("Privados")]
	private bool rechargeb = false; // Condição pra recarregar.
	private int timetr; //Valor do tempo da recarga.
	private PalyerController pc;
	public bool holdright = false;


	// Update is called once per frame
	void Update() { 
		rechargeShow.value = timetr;
		ammotext.text = ammoPent + "/" + ammo;

		//O botão do Mouse está clicado segurado?
		if (Input.GetButtonDown ("Fire2")) 
			holdright = true;

		if (Input.GetButtonUp ("Fire2")) 
			holdright = false;
		//Animação se for true o clique do Mouse.
		if (holdright == true)
		{
			//Animação de Mira true.
			pc.anim.SetBool ("aim", true);
			//Valor da Perspectiva diminui quando o Player vê o mundo enquanto mira.
			cam.fieldOfView = 62;
		} 
		else
		{
			//Animação de Mira falso.
			pc.anim.SetBool ("aim", false);
			//Valor da Perspectiva aumenta quando o Player vê o mundo enquanto não mira.
			cam.fieldOfView = 66;

		}


		//Condição pra disparar concernente ao Button e sem mira.
		if (Input.GetButtonDown("Fire1") && pc.enableMouse == true)
			hold = true;
		if (Input.GetButtonUp("Fire1"))
			hold = false;

		//Tempo de espera pra disparar de novo.
		if (hold == true)
			waitToFirerate += 1;
		
		//Primeiro espers de alguns segundos pra disparar de novoi, && seguno, see no carregador a bala for > que zero então continua a disparar.

		if (waitToFirerate > firerate && ammoPent > 0) 
			Shoot (); //Metodo e funcções dessa conição tem haver com Shoot() {..Disparar..}

		//Condição pra instanciar som da arma sem balas (O barulhinho).

		else if (waitToFirerate > firerate && ammoPent <= 0) 
		{
			Instantiate (FireNoBullet, transform.position, transform.rotation, transform);
			waitToFirerate = 0;
		}
			
		//Condição pra recarregar,"de um jeito mais longo" com várias condições pra dar um pequeno tempo na animation de recharge.

		if (Input.GetButtonDown ("Recharge") && ammo != maxammoPent && ammo != 0 && rechargeb == false)
		{
			Reload.Play ();
			rechargeGo.SetActive (true);
			rechargeb = true;
		}
		if (rechargeb == true) 
		{
			pc.anim.SetBool ("reloading", true);


			if (timetr > timeRecharge) 
			{
				for (int i = 0; i < maxammoPent; i++) 
				{
					// se a munição que está no pente for menor que a quantidade total de balas e munição do pente for igual a zero,então tira do pente e recarrega pra munição,essa condição foi feita fundamentalmente pra evitar bugs.
					if (ammoPent < maxammoPent && ammo > 0)
					{
						ammo -= 1;
						ammoPent += 1;
					} 
					else 
					{
						break;
					
					}
					rechargeb = false;
					timetr = 0;

					rechargeGo.SetActive (false); // Tem haver com Slider.
				}

			} else
			{
				timetr += 1;

			}
		} 
		else
		{
			pc.anim.SetBool ("reloading", false);

		}
	}
	private void Start()
	{
		pc = (FindObjectOfType(typeof(PalyerController)) as PalyerController);
		//Mostrar a barra de recarregar com Slider.
		rechargeShow.maxValue = timeRecharge;

		}

	//ARMA.
	void Shoot()
	{
		//Pra disparar o audio da Gun.
		Instantiate (Fire, transform.position, transform.rotation, transform);
		if(holdright)
			// //Animação de disparar,se estiver mirando.
			pc.anim.Play("aimshoot");
			else 
			//Animação de disparar,se não estiver mirando.
			pc.anim.Play("shoot"); 
		
		//Condição pra Disparar concernente o número de balas.
		if (ammoPent > 0) // se for > que zero atira.
		{
			waitToFirerate = 0;
			ammoPent -= 1;
			ammoParticle.Play (); //Disparar os efeitos.
			RaycastHit hit;

			// Descrever aonde estamos a mirar.
			if (Physics.Raycast (cam.transform.position, cam.transform.forward , out hit, range)) 
			{
				Debug.Log ("Mirando em:" + hit.transform.name);
				//Destruir Objeto depois de alguns tiros.
				DestruirObj ob = hit.transform.GetComponent<DestruirObj> ();
				GameObject impactGo = Instantiate (BulletHole, hit.point, Quaternion.LookRotation (hit.normal));

				if (ob != null)
					ob.takeDamage (damage);
			}
	
		}
	}

}