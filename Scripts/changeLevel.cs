using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeLevel : MonoBehaviour {

	void Update()
	{
		if (Input.GetKeyDown (KeyCode.T)) {
			Application.LoadLevel ("Main");
		} 
	}

}
