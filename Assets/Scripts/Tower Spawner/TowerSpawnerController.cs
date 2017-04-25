using ElementalTowerDefenseModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TowerSpawnerController : Controller<TowerSpawnerView> {

    private TowerSelectorController towerSelectorController;
    private TowerService towerService;
    private GoldManagerController goldManagerController;

    private List<TowerController> controllers = new List<TowerController>();

    public bool HasGoldForTower
    {
        get
        {
            return goldManagerController.CanSpend(towerSelectorController.SelectedTowerType);
        }
    }

    public TowerSpawnerController(TowerSpawnerView View, TowerSelectorController towerSelectorController, GoldManagerController goldManagerController) : base(View)
    {
        this.towerSelectorController = towerSelectorController;
        towerService = new TowerService(goldManagerController.Model, towerSelectorController.Model);
        this.goldManagerController = goldManagerController;
    }

    public void SpawnTower()
    {
        if (View.TowerIsPlaceable)
        {
            Tower tower = towerService.Buy();
            if (tower != null)
            {
                TowerController controller = TowerFactory.CreateTower(towerSelectorController.SelectedTowerType, tower, this);
                goldManagerController.UpdateView();
                controllers.Add(controller);
                View.IsInSpawnMode = false;
            }
        }
    }

    public void ReloadAll()
    {
        foreach (TowerController controller in controllers)
            controller.OnReload();
    }

    public float Sell(TowerController towerController)
    {
        float sellPrice = towerService.Sell(towerController.Model);
        controllers.Remove(towerController);
        return sellPrice;
    }

    public float Reload(TowerController towerController)
    {
        float reloadPrice = towerService.Reload(towerController.Model);
        towerController.UpdateView();
        return reloadPrice;
    }

    public override void UpdateView()
    {

    }
}
