using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PalyerController : MonoBehaviour {

	[Header ("PlayerConfig Move & Rotation")]
	public float speed = 5f; 
	public float speedRun = 7f;
	public float speedWalk = 3;
	public float sensibility = 3f; //Sensibiliade do Mouse.
	public bool enableMouse = true; //Desativar Mouse e ativar Mouse da tela.
	public float jumpHeight = 5f;


//	[Header ("SystemConfig")]
//	//Sistema de audio concernente aos passos do Player.
//	public GameObject leftFoot;
//	public GameObject rightFoot;
//	public float Foots = 0f;

	// Animation de correr ou andar
	[Header ("Animações")]
	public Animator anim; 


	//Sistema de audio concernente aos passos do Player.
	[Header ("Audios")]
	public GameObject walk;
	public GameObject run;



	public bool hold = false;

	[Header ("Privados")]
	private Player motor;
	private float RealSpeed = 0f;
	private float waitingTime = 0f; 
	private float timeToaudio;

	// Use this for initialization
	void Start () {
		
		//Pegar o Componente Player.

		motor = GetComponent <Player> ();
	}
	
	// Update is called once per frame
	void Update ()
	{


		//MouseLock
		if (enableMouse == true) {
			//Quando for ativado o cursor vai ficar invisível.
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		} else {
			//Quando o Mouse for desativado o cursor vai ficar visível.
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;

		}


		// Controle da velociade anim de andar e correr.
		if (Input.GetButton ("Run") == true)
			RealSpeed = speedRun;
		else if (Input.GetButton ("Walk") == true)
			RealSpeed = speedWalk;
		else
			RealSpeed = speed;
		
		
		//Movimento do Palyer
		float _xMov = Input.GetAxisRaw ("Horizontal");
		float _zMov = Input.GetAxisRaw ("Vertical");

		//Condição pra disparar concernente ao Button.
		if (enableMouse == true) {
			if (_xMov != 0 || _zMov != 0)
				hold = true;
			else
				hold = false;
		}


		//Condições,Concenente ao controlo da animação, e sons de passos do Player.
		if (_zMov != 0) {
			anim.SetBool ("walk", true);

			if (RealSpeed == speedRun)
			{
				if (timeToaudio < 0)
				{
					Instantiate (run, transform.position, transform.rotation, transform.parent);
					timeToaudio = 2f;
				} 
				else 
				{
					timeToaudio -= 0.1f;
				}
				anim.SetBool ("run", true);
			}
			else
			{
				anim.SetBool ("run", false);

				if (timeToaudio < 0)
				{
					Instantiate (walk, transform.position, transform.rotation, transform.parent);
					timeToaudio = 2;
				} 
				else 
				{
					timeToaudio -= 0.1f;
				}
			}
		} 
		else
		{
			anim.SetBool ("walk", false);

		}
			
		
		//Tempo de espera pra disparar de novo.
		if (hold == true)
			waitingTime += 1;
		
//		//Condições de sons no pé enquanto o Palyer anda.
//		if (waitingTime > 10 && hold == true)
//		{
//
//		if (Foots > 10 && Foots < 15) 
//			{
//				if (Input.GetButton ("Walk") == false)
//				Instantiate (rightFoot, transform.position, transform.rotation);
//				Foots += 1 * RealSpeed;
//			} 
//			else if ( Foots > 1)
//			{
//					if (Input.GetButton ("Walk") == false)
//				Instantiate (rightFoot, transform.position, transform.rotation);
//
//					Foots = 0;
//			}
//			else 
//			{
//				Foots += 1 * RealSpeed;
//
//			}
//				
//			waitingTime = 0;

		//CONFIGURANDO O TRANSFORM.

		Vector3 _MoveHorizontal = transform.right * _xMov;
		Vector3 _MoveVertical = transform.forward * _zMov;

	//Place Movimwent
		Vector3 _velocity = (_MoveHorizontal + _MoveVertical).normalized * RealSpeed;
		motor.Move (_velocity);

	//Rotation
		float _yMouse = Input.GetAxisRaw("Mouse X");
		Vector3 _rotation = new Vector3 (0f, _yMouse, 0f) * sensibility;

		if (enableMouse == true)
			motor.Rotation (_rotation);

	//CameraRotation
		float _xMouse = Input.GetAxisRaw("Mouse Y");
		Vector3 _cameraRotation = new Vector3 (_xMouse, 0f, 0f) * sensibility;

		if (enableMouse == true)
			motor.RotationCamera (_cameraRotation);
		
		//Se apertar "Space" o Player Salta,se segurar salta mais alto.
		if (Input.GetButton ("Jump") == true)
			motor.Jump (jumpHeight);
		else
			motor.Jump (0);
	}
}
	
