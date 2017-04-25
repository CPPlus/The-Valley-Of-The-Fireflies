using ElementalTowerDefenseModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GoldManagerController : ModelController<GoldManager, GoldManagerView>
{
    public GoldManagerController(GoldManager Model, GoldManagerView View) : base(Model, View)
    {
    }

    public bool CanSpend(TowerType towerType)
    {
        return Model.CanSpend(
                Model.PriceList.GetPrice(
                    towerType));
    }

    public override void UpdateView()
    {
        View.UpdateState(Model.Gold);
    }
}
