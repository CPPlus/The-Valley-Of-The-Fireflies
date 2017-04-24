using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ElementalTowerDefenseModel;

public class TargetFollower : MonoBehaviour { 

    public GameObject Target { get; set; }
    public float speed;

	void Start () {
	
	}
	
	void Update () {
        FollowTarget();
	}

    private void FollowTarget()
    {
        if (!Target) return;

        Vector3 direction = GetDirectionToTarget();
        transform.position += direction * speed * Time.deltaTime;
    }

    private Vector3 GetDirectionToTarget()
    {
        Vector3 direction = Target.transform.position - transform.position;
        direction.Normalize();
        return direction;
    }
}
