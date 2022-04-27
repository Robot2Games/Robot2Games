using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	[Header ("System")]
	public Camera cam;


	// Sistema de velocidade do Player.
	//São privados devido a um suposto Modo online pra evitar Hack.
	private Vector3 velocity = Vector3.zero;
	private Vector3 rotation = Vector3.zero;
	private Vector3 cameraRotation = Vector3.zero;
	private Vector3 jumheight = Vector3.zero;
	private bool rFoot = false;
	private Rigidbody rb;


	

	// Use this for initialization
	void Start () 
	{
		rb = GetComponent<Rigidbody> ();
	}



		
	//*Variáveis concernente ao Movimento do Player*//
		
				//De pulo.
	 public void Jump (float _jumpHeight)
	{
		jumheight = new Vector3 (0f, _jumpHeight, 0f);
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
			//De movimento.
	public void Move(Vector3 _velocity) 
	{
		velocity =  _velocity;
	}
			//De rotation.
	public void Rotation(Vector3 _rotation) 
	{
		rotation =  _rotation;

	}
			// Rotação com cam.
	public void RotationCamera(Vector3 _cameraRotation) 
	{

		cameraRotation =  _cameraRotation;
	}



	void FixedUpdate()
	{
		PlayRotation ();
		PlayMoviment ();
	}

	void PlayRotation()
	{
		rb.MoveRotation (rb.rotation * Quaternion.Euler (rotation));
		if (cam != null) 
		{
			cam.transform.Rotate (cameraRotation);

		}
	}

	void PlayMoviment()
	{
		//Movimento.
		if (velocity != Vector3.zero)
			rb.MovePosition (rb.position + velocity * Time.fixedDeltaTime);
		
		//Salto.
		if (jumheight != Vector3.zero)
			rb.MovePosition (rb.position + jumheight * Time.fixedDeltaTime);
		
	}
}
