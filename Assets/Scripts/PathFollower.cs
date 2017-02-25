using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollower : MonoBehaviour {

    private const float POINT_REACH_RADIUS = 0.1f;

    public GameObject path;
    public float movementSpeed;
    public float rotationSpeed;

    private Vector3[] pathPoints;
    private int currentPathPoint = 0;
    
	void Start () {
        ExtractPointsFromPath();
	}
	
	void Update () {
        FollowPath();
    }

    private void FollowPath()
    {
        // If there are no more points to follow.
        if (currentPathPoint >= pathPoints.Length) return;

        // Go to next point if current one is reached.
        Vector3 direction = pathPoints[currentPathPoint] - transform.position;
        if (direction.magnitude < POINT_REACH_RADIUS) currentPathPoint++;

        // Move towards current point.
        direction.Normalize();
        transform.Translate(direction * movementSpeed * Time.deltaTime, Space.World);

        // Rotate towards current point.
        Quaternion lookDirection = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookDirection, rotationSpeed * Time.deltaTime);
    }

    private void ExtractPointsFromPath()
    {
        List<Vector3> result = new List<Vector3>();
        foreach (Transform child in path.transform)
        {
            result.Add(child.position);
        }
        pathPoints = result.ToArray();
    }
}
