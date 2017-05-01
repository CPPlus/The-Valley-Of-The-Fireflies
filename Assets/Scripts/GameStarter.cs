using ElementalTowerDefenseModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStarter : MonoBehaviour {

    public MonsterSpawnerView spawnerView;
    private MonsterSpawnerController spawnerController;

    private TowerSpawnerController towerSpawnerController;
    public TowerSpawnerView towerSpawnerView;

    private GoldManagerController goldManagerController;
    public GoldManagerView goldManagerView;

    void Start () {
        GoldManager goldManager = new GoldManager(150, new RegularPriceList());
        goldManagerController = new GoldManagerController(goldManager, goldManagerView);

        WaveService waveService = new WaveService(goldManager);
        spawnerController = new MonsterSpawnerController(spawnerView, goldManagerController, waveService);

        TowerSpawner towerSpawner = new TowerSpawner();
        towerSpawnerController = new TowerSpawnerController(towerSpawner, towerSpawnerView, goldManagerController);
        towerSpawnerView.Controller = towerSpawnerController;
        towerSpawnerController.SelectTower(TowerType.EARTH_TOWER);
    }
	
	void Update () {
        spawnerController.Update(Time.deltaTime);

        UserInput();
	}

    private void UserInput()
    {
        if (Input.GetKeyDown(KeyCode.K)) spawnerController.KillWave();
        if (Input.GetKeyDown(KeyCode.R)) towerSpawnerController.ReloadAll();
        if (Input.GetKeyDown(KeyCode.Alpha1)) towerSpawnerController.SelectTower(TowerType.EARTH_TOWER);
        if (Input.GetKeyDown(KeyCode.Alpha2)) towerSpawnerController.SelectTower(TowerType.FIRE_TOWER);
        if (Input.GetKeyDown(KeyCode.Mouse0)) towerSpawnerController.SpawnTower();
    }
}
