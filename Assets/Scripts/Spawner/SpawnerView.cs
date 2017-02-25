using ElementalTowerDefenseModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerView : MonoBehaviour {

    public GameObject crawlingHorrorPrefab;
    public GameObject runnerPrefab;

	void Start () {
		
	}
	
	void Update () {
		
	}

    public void SpawnMonsterView(MonsterController controller, MonsterType type)
    {
        GameObject prefab = null;
        switch (type)
        {
            case MonsterType.CRAWLING_HORROR:
                prefab = crawlingHorrorPrefab;
                break;
            case MonsterType.RUNNER:
                prefab = runnerPrefab;
                break;
        }

        GameObject monster = Instantiate(prefab, transform.position, transform.rotation);
        MonsterView view = monster.GetComponent<MonsterView>();

        PathFollower follower = view.gameObject.GetComponent<PathFollower>();
        follower.path = GameObject.FindGameObjectWithTag("Path");

        controller.View = view;
    }
}
