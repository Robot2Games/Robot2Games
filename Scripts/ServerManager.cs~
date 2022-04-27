using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ServerManager : MonoBehaviour {
	
	[Header ("ServerConfig")]
	public string ip;
	public int port;
	public string ServerName;
	public bool useNet;
	public int maxPlayers;
	public GameObject canvas;

	void CreateServers()
	{
		Network.InitializeServer (maxPlayers, port, useNet);
		MasterServer.RegisterHost ("FPSNormal", ServerName);
		print (ServerName + "Iniciado!");
		canvas.SetActive (false);
	}

	public void Connect()
	{
		Network.Connect (ip, port);	
	}

	private void OnConnectedToServer()
	{
		
	}

	// Use this for initialization
	void Start () {

		useNet = !Network.HavePublicAddress ();
		canvas.SetActive (false);
		print ("Conectado a" + ip);

		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
