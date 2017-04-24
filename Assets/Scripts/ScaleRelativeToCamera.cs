using UnityEngine;
using System.Collections;

public class ScaleRelativeToCamera : MonoBehaviour
{
    public new Camera camera;
    public float objectScale = 1.0f;
    private Vector3 initialScale;
    
    void Start()
    {
        initialScale = transform.localScale;
        
        if (camera == null)
            camera = Camera.main;
    }

    public void ResetScale()
    {
        transform.localScale = new Vector3(1, 1, 1);
    }
    
    void Update()
    {
        Plane plane = new Plane(camera.transform.forward, camera.transform.position);
        float dist = plane.GetDistanceToPoint(transform.position);

        Vector3 scale = initialScale * dist * objectScale;

        transform.localScale = scale;
    }
}