using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ElementalTowerDefenseModel;

public class MonsterFactory : MonoBehaviour {

    public static MonsterFactory instance;

    public GameObject spawner;
    public GameObject crawlingHorrorPrefab;
    public GameObject runnerPrefab;
    public GameObject soulEaterPrefab;
    public GameObject airHorrorPrefab;
    public GameObject path;

    void Start()
    {
        instance = this;
    }

	public static MonsterController CreateMonster(MonsterType type, Monster model)
    {
        GameObject monsterInstance = null;
        GameObject prefab = null;
        switch (type)
        {
            case MonsterType.CRAWLING_HORROR:
                prefab = instance.crawlingHorrorPrefab;
                break;
            case MonsterType.RUNNER:
                prefab = instance.runnerPrefab;
                break;
            case MonsterType.SOUL_EATER:
                prefab = instance.soulEaterPrefab;
                break;
            case MonsterType.AIR_HORROR:
                prefab = instance.airHorrorPrefab;
                break;
        }
        monsterInstance = Instantiate(prefab, instance.spawner.transform.position, instance.spawner.transform.rotation);
        
        PathFollower pathFollower = monsterInstance.GetComponent<PathFollower>();
        pathFollower.path = instance.path;
        pathFollower.ExtractPointsFromPath(model.Terrain);

        MonsterView view = monsterInstance.GetComponent<MonsterView>();
        MonsterController controller = new MonsterController(model, view);
        view.Controller = controller;
        controller.View = view;
        return controller;
    }
}
