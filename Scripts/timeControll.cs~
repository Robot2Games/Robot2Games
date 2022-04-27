using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timeControll : MonoBehaviour
{

	public List<Vector3> timeLine = new List<Vector3> ();
	public List<Quaternion> timeLineR = new List<Quaternion> ();
	public int maxCapacity;
	public bool Gravando;
	public bool Voltando;
	private Rigidbody rb;

	// Use this for initialization
	void Start ()
	{
		rb = GetComponent<Rigidbody> ();
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Voltando)
			Voltar ();
		else {
			Gravando = true;
			rb.isKinematic = false;
		}
		if (Gravando)
			Gravar ();
		if (Input.GetKey(KeyCode.Backspace))
		{
			Voltando = true;
		}
	}

	void Voltar ()
	{
		if (timeLine.Count - 1 > 0) {
			Gravando = false;
			rb.isKinematic = true;
			transform.rotation = timeLineR [timeLineR.Count - 1];
			transform.position = timeLine [timeLine.Count - 1];
			timeLine.RemoveAt (timeLine.Count - 1);
		}
	}

	void Gravar ()
	{
		if (timeLine.Count > maxCapacity)
			timeLine.RemoveAt (0);
		if (timeLineR.Count > maxCapacity)
			timeLineR.RemoveAt (0);

		timeLineR.Add (transform.rotation);
		timeLine.Add (transform.position);
	}
}
