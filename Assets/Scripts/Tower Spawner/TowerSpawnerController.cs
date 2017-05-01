using ElementalTowerDefenseModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TowerSpawnerController : ModelController<TowerSpawner, TowerSpawnerView> {
    
    private TowerService towerService;
    private GoldManagerController goldManagerController;

    private List<TowerController> controllers = new List<TowerController>();

    public bool IsInSpawnMode
    {
        get
        {
            return Model.IsInSpawnMode;
        }
        set
        {
            Model.IsInSpawnMode = value;
            UpdateView();
        }
    }

    public bool HasGoldForTower
    {
        get
        {
            return goldManagerController.CanSpend(Model.SelectedTowerType);
        }
    }

    public TowerSpawnerController(
        TowerSpawner model, 
        TowerSpawnerView view, 
        GoldManagerController goldManagerController) : base(model, view)
    {
        towerService = new TowerService(goldManagerController.Model, Model);
        this.goldManagerController = goldManagerController;
    }

    public void SpawnTower()
    {
        if (View.TowerIsPlaceable)
        {
            Tower tower = towerService.Buy();
            if (tower != null)
            {
                TowerController controller = TowerFactory.CreateTower(Model.SelectedTowerType, tower, this);
                goldManagerController.UpdateView();
                controllers.Add(controller);
                IsInSpawnMode = false;
            }
        }
    }

    public void SelectTower(TowerType towerType)
    {
        Model.SelectedTowerType = towerType;
        IsInSpawnMode = true;
        UpdateView();
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

    public void HideTowerUIs()
    {
        foreach (TowerController controller in controllers)
            controller.View.ToggleUI(true);
    }

    public override void UpdateView()
    {
        View.UpdateState(
            new TowerSpawnerViewModel
            {
                TowerType = Model.SelectedTowerType,
                IsInSpawnMode = Model.IsInSpawnMode,
                Range = (ElementalTowerDefenseModel.TowerFactory.CreateTower(Model.SelectedTowerType).Range.Points)
            });
    }
}
