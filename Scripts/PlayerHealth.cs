using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

    public int startingHealth = 100; //Vida Totoal
    public int currentHealth; // Estado da vida
    public Slider healthSlider; // Barra de vida
    public Image damageImage; //Imagen ao receber danos
    public float flashSpeed = 5f; //Tempo concernente a imagem de dano
    public Color flashColour = new Color (1f,0f,0f,0.1f);


    Animator anim;
    PalyerController palyerController;
    bool isDead;
    bool damaged;

    void Awake()
    {
        anim = GetComponent <Animator>();
        palyerController = GetComponent<PalyerController>();
        currentHealth = startingHealth;
    }


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if(damaged)
        {
        damageImage.color = flashColour;
        }
        else
        {
        damageImage.color = Color.Lerp (damageImage.color,Color.clear,flashSpeed * Time.deltaTime);
        }
        damaged = false;
	}

	public void TakeDamage(int amount)
	{
		damaged = true;
		currentHealth -= amount;
		healthSlider.value = currentHealth;
		if (currentHealth <= 0 && !isDead) {
			Death ();
		}
	}

    void Death()
    {
        isDead = true;
        anim.SetTrigger("Die");
        palyerController.enabled = false;
    }


}
