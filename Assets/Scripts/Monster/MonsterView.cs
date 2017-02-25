using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PathFollower))]
public class MonsterView : MonoBehaviour {

    private PathFollower follower;

	void Start () {
        
	}
	
	void Update () {
		
	}

    public void UpdateState(MonsterViewModel viewModel)
    {
        if (follower == null) follower = GetComponent<PathFollower>();

        follower.movementSpeed = viewModel.Speed;
        if (viewModel.IsDead) Destroy(gameObject);
    }
}
