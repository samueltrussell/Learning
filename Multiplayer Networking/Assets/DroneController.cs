using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class DroneController : NetworkBehaviour {

    public GameObject body;

	// Use this for initialization
	void Start () {

        if (!isLocalPlayer)
        {
            GetComponentInChildren<Camera>().enabled = false;
        }
        else
        {
            GetComponentInChildren<Camera>().tag = "MainCamera";
        }
    }
	
	// Update is called once per frame
	void Update () {

        body.transform.Rotate(new Vector3(1.5f, .4f, -1.5f));

        if (!isLocalPlayer)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            getPosition();
            Debug.Log("Got Click Event");
        }
    }
    
    void getPosition()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorHit;

        if (Physics.Raycast(camRay, out floorHit, 100f))
        {
            GetComponentInChildren<NavMeshAgent>().SetDestination(floorHit.point);
        }
    }

}
