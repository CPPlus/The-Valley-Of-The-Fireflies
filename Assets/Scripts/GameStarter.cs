using ElementalTowerDefenseModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStarter : MonoBehaviour {

    public MonsterSpawnerView spawnerView;
    private MonsterSpawnerController spawnerController;

    private TowerSelectorController towerSelectorController;
    public TowerSelectorView towerSelectorView;

    private TowerSpawnerController towerSpawnerController;
    public TowerSpawnerView towerSpawnerView;

    private GoldManagerController goldManagerController;
    public GoldManagerView goldManagerView;

    void Start () {
        GoldManager goldManager = new GoldManager(150, new RegularPriceList());
        goldManagerController = new GoldManagerController(goldManager, goldManagerView);

        spawnerController = new MonsterSpawnerController(spawnerView, goldManagerController);
        spawnerController.View = spawnerView;

        towerSelectorController = new TowerSelectorController(new TowerSelector(), towerSelectorView);
        towerSelectorView.Controller = towerSelectorController;
        towerSelectorController.SelectTowerType(TowerType.EARTH_TOWER);

        towerSpawnerController = new TowerSpawnerController(towerSpawnerView, towerSelectorController, goldManagerController);
        towerSpawnerView.Controller = towerSpawnerController;
    }
	
	void Update () {
        spawnerController.Update(Time.deltaTime);

        UserInput();
	}

    private void UserInput()
    {
        if (Input.GetKeyDown(KeyCode.K)) spawnerController.KillWave();
        if (Input.GetKeyDown(KeyCode.R)) towerSpawnerController.ReloadAll();
        if (Input.GetKeyDown(KeyCode.Alpha1)) towerSelectorController.SelectTowerType(TowerType.EARTH_TOWER);
        if (Input.GetKeyDown(KeyCode.Alpha2)) towerSelectorController.SelectTowerType(TowerType.FIRE_TOWER);
        if (Input.GetKeyDown(KeyCode.Mouse0)) towerSpawnerController.SpawnTower();
    }
}
