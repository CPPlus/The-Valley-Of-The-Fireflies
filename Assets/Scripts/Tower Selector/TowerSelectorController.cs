using ElementalTowerDefenseModel;
using System.Collections;
using System.Collections.Generic;
using System;

public class TowerSelectorController : ModelController<TowerSelector, TowerSelectorView>, IController
{
    public TowerSelectorController(TowerSelector Model, TowerSelectorView View) : base(Model, View)
    {

    }

    public void SelectTowerType(TowerType towerType)
    {
        Model.Select(towerType);
        UpdateView();
    }

    public override void UpdateView()
    {
        View.UpdateState(Model.SelectedTowerType);
    }
}
