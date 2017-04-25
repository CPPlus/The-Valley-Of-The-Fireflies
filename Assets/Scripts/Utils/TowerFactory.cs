using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ElementalTowerDefenseModel;

public class TowerFactory : MonoBehaviour
{
    public static TowerFactory instance;

    public GameObject spawner;
    public GameObject earthTowerPrefab;
    public GameObject fireTowerPrefab;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
       
    }

    public static TowerController CreateTower(TowerType type, Tower model, TowerSpawnerController spawnerController)
    {
        GameObject dummy;
        return CreateTower(type, model, spawnerController, out dummy);
    }

    public static TowerController CreateTower(TowerType type, Tower model, TowerSpawnerController spawnerController, out GameObject gameObject)
    {
        GameObject towerInstance = null;
        GameObject prefab = null;
        switch (type)
        {
            case TowerType.EARTH_TOWER:
                prefab = instance.earthTowerPrefab;
                break;
            case TowerType.FIRE_TOWER:
                prefab = instance.fireTowerPrefab;
                break;
        }
        towerInstance = Instantiate(prefab, instance.spawner.transform.position, instance.spawner.transform.rotation);

        TowerView view = towerInstance.GetComponent<TowerView>();
        TowerController controller = new TowerController(model, view, spawnerController);
        view.Controller = controller;
        controller.UpdateView();

        gameObject = towerInstance;

        return controller;
    }
}
