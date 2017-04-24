using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowardsCameraRotator : MonoBehaviour {

    public new Camera camera;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        RotateTowardsCamera();
	}

    private void RotateTowardsCamera()
    {
        if (camera == null) camera = Camera.main;
        transform.LookAt(
                    transform.position + camera.transform.rotation * Vector3.forward,
                    camera.transform.rotation * Vector3.up);
    }
    
}
