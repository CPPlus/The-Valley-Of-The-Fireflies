using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {

    [Header("Movement")]
    public float speed;
    public float leftBoundary;
    public float rightBoundary;
    public float forwardBoundary;
    public float backwardBoundary;
    public float screenBoundingFrameWidth;

    [Header("Positioning")]
    public int initialXRotation;

    [Header("Zoom")]
    public int zoomAnimationSpeed;
    public int zoomSpeed;
    public int ZOOM_MIN;
    public int ZOOM_MAX;

    public float zoom { get; private set; }
    private int rotDegPerZoomUnit;

    void Start () {
        // transform.position = new Vector3(-50, 0, 0);

        zoom = ZOOM_MIN;

        int zoomMinMaxDif = ZOOM_MIN - ZOOM_MAX;
        rotDegPerZoomUnit = initialXRotation / zoomMinMaxDif;
	}
	
	void Update () {
        MoveCamera();
        LockCamera();

        Zoom();
        LockZoom();
        UpdateZoom();
    }

    private void LockCamera()
    {
        Vector3 lockedPosition = transform.position; 
        if (transform.position.x < leftBoundary) lockedPosition.x = leftBoundary;
        if (transform.position.x > rightBoundary) lockedPosition.x = rightBoundary;
        if (transform.position.z > forwardBoundary) lockedPosition.z = forwardBoundary;
        if (transform.position.z < backwardBoundary) lockedPosition.z = backwardBoundary;

        transform.position = lockedPosition;
    }

    private void ZoomIn()
    {
        zoom -= zoomSpeed;
        
    }

    private void ZoomOut()
    {
        zoom += zoomSpeed;
        
    }

    private void LockZoom()
    {
        if (zoom < ZOOM_MAX) zoom = ZOOM_MAX;
        else if (zoom > ZOOM_MIN) zoom = ZOOM_MIN;
    }

    private void UpdateZoom()
    {
        transform.position = Vector3.Lerp(
            transform.position,
            new Vector3(
                transform.position.x,
                zoom,
                transform.position.z
            ),
            zoomAnimationSpeed * Time.deltaTime);

        int zoomLevel = (int)zoom;
        transform.rotation = Quaternion.Lerp(
            transform.rotation,
            Quaternion.Euler(
                zoomLevel * rotDegPerZoomUnit,
                0,
                0),
            zoomAnimationSpeed * Time.deltaTime);
    }

    private void Zoom()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            ZoomIn();
        } else if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            ZoomOut();
        }
    }

    private void MoveCamera()
    {
        float movementSpeed = speed;

        Debug.DrawRay(transform.position, transform.forward, Color.red);

        // Move left.
        if (Input.mousePosition.x < screenBoundingFrameWidth)
        {
            transform.Translate(-movementSpeed * Time.deltaTime, 0, 0);
        }

        // Move right.
        if (Input.mousePosition.x > Screen.width - screenBoundingFrameWidth)
        {
            transform.Translate(movementSpeed * Time.deltaTime, 0, 0);
        }

        // Move forward.
        if (Input.mousePosition.y > Screen.height - screenBoundingFrameWidth)
        {
            transform.Translate(0, 0, movementSpeed * Time.deltaTime, Space.World);
        }

        // Move backward.
        if (Input.mousePosition.y < screenBoundingFrameWidth)
        {
            transform.Translate(0, 0, -movementSpeed * Time.deltaTime, Space.World);
        }
    }
}
