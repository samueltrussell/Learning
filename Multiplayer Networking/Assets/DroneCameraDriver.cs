using UnityEngine;
using System.Collections;

public class DroneCameraDriver : MonoBehaviour {

    private Quaternion initRotation;

	// Use this for initialization
	void Start () {

        initRotation = transform.rotation;

    }
	
	// Update is called once per frame
	void Update () {
        transform.rotation = initRotation;
	}
}
