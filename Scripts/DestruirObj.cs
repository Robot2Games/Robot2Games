using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestruirObj : MonoBehaviour {
	

	public float health;
	public List<GameObject> Parts = new List<GameObject>();
	public int cooldown = 100;
	public bool iscooldown;

	private BoxCollider bc;
	private Rigidbody rbb;

	void Start()
	{
		bc = GetComponent<BoxCollider>();
		rbb = GetComponent<Rigidbody>();
	}

	public void takeDamage(float i) //Causar danos.
	{
		//Se tiver a vida menor ou igual a zero,então Die.
		health -= i;
		if (health <= 0)
		{
			die(); 
		}
	}
	void die()
	{
		for (int i = 0; i < Parts.Count; i++) //Isso tem haver com a gravidade do corpo depois de morrer ou for atingido.
		{
			Parts[i].AddComponent<Rigidbody>();
			Rigidbody rb = Parts[i].GetComponent<Rigidbody>();
			float x = Random.Range(-100, 100);
			float y = Random.Range(0, 100);
			float z = Random.Range(-100, 100);
			rb.AddForce(new Vector3(x, y, z));
		}
		bc.isTrigger = true;
		rbb.isKinematic = true;
		iscooldown = true;
	}

	private void Update()
	//Para fazer o corpo desaparecer depois de morto.
	{
		if (iscooldown == true)
		if (cooldown > 0) 
		{
			cooldown -= 1;


		} else 
		{

			Destroy (gameObject);

		}
			



	}
}