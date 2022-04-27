using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour {

	[Header ("Ammo")]
	public int maxammoPent;
	public int ammoPent;
	public int ammo;
	public int timeRecharge;


	private bool rechargeb = false;
	private int timetr;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Recharge") && ammo != maxammoPent && ammo != 0 && rechargeb == false)
		{
			rechargeb = true;
	}
	if (rechargeb == true)
	{

			if(timetr > timeRecharge)
			{
				for (int i = 0; i <maxammoPent; i++)
				{
					if(ammoPent < maxammoPent && ammo > 0)
					{
					ammo += 1;
					ammoPent += 1;

				}
					else
					{
						break;

					}
					rechargeb = false;
					timetr = timeRecharge;

		}

	}
			else 
			{
				timetr += 1;

			}
		}
	}

}