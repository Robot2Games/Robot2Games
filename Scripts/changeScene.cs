using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeScene : MonoBehaviour {


	public void ChangeScenes (string nomeCena)
	{
		Application.LoadLevel (nomeCena);
	}


}
