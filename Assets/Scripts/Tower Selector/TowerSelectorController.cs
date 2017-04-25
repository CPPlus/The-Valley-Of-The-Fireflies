using ElementalTowerDefenseModel;
using System.Collections;
using System.Collections.Generic;
using System;

public class TowerSelectorController : ModelController<TowerSelector, TowerSelectorView>, IController
{
    public TowerSelectorController(TowerSelector Model, TowerSelectorView View) : base(Model, View)
    {

    }

    public TowerType SelectedTowerType
    {
        get
        {
            return Model.SelectedTowerType;
        }

        set
        {
            Model.Select(value);
            UpdateView();
        }
    }

    public override void UpdateView()
    {
        View.UpdateState(
            new TowerSelectorViewModel
            {
                TowerType = Model.SelectedTowerType
            });
    }
}
