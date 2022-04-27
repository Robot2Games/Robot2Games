using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bot : MonoBehaviour {

    public float range;
	public float speed;
    public float speedAim;
    public float speedFire;
    public int damage;
    public int precisao;
    public Vector3 minArea;
    public Vector3 maxArea;

    private NavMeshAgent agent;
	private List<PalyerController> players = new List<PalyerController>();

	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent>();
		agent.speed = speed;
	}
	
	// Update is called once per frame
	void Update () {
	        //Lista de Players
		if (FindObjectsOfType<PalyerController> ().Length > 0) {

			players.Clear ();
			for (int i = 0; i < FindObjectsOfType<PalyerController>().Length; i++)
			{

				players.Add(FindObjectsOfType<PalyerController>()[i]);
			}


		}
            //Verifica se há Players no Range.
		int pl = -1;
		bool GetSomeBody = false;

	    for (int i = 0; i < players.Count; i++)
	    {
	        if(Vector3.Distance(players[i].transform.position,transform.position)< range)
	        {
                pl = i;
                GetSomeBody = true;
	        }

	    }
            if(GetSomeBody == true)
                pl = -1;

            if(pl != -1)
            {

            if(Vector3.Distance(players[pl].transform.position,transform.position)< range/ 2)
            {
                StartCoroutine(ainTime(pl));
            }
            else
            {
             agent.SetDestination(players[pl].transform.position);

             }
        }
        else
        {
            if(Vector3.Distance(transform.position, agent.destination)< 2)
            {
                float x = Random.Range(0 + minArea.x, maxArea.x);
                float y = minArea.y;
                float z = Random.Range(0 + minArea.z, maxArea.z);

                agent.SetDestination(new Vector3 (x,y,z));
            }
        }
	}
        IEnumerator ainTime(int pls)
        {
            yield return new WaitForSeconds(speedAim);
            transform.LookAt(players[pls].transform.position);
        }

        IEnumerator fireTime(int pls)
        {
            yield return new WaitForSeconds(speedFire);
            int rnd = Random.Range (0,precisao);

        }
}
