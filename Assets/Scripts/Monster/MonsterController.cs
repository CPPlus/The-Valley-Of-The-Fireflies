using ElementalTowerDefenseModel;
using System;
using System.Collections;
using System.Collections.Generic;

public class MonsterController : ModelController<Monster, MonsterView> {

    public MonsterController(Monster Model, MonsterView View) : base(Model, View)
    {

    }

    public void OnShot(Projectile projectile)
    {
        Model.GetShot(projectile);
        UpdateView();
    }

    public override void UpdateView()
    {
        MonsterViewModel viewModel = new MonsterViewModel();
        viewModel.Speed = Model.MovementComp.Speed.Points;
        viewModel.IsDead = Model.HealthComp.IsDead;
        viewModel.MaxHealth = Model.HealthComp.Health.MaxPoints;
        viewModel.Health = Model.HealthComp.Health.Points;
        viewModel.KillReward = (new RegularPriceList()).GetPrice(Model.Type);

        if (View != null) 
            View.UpdateState(viewModel);
    }
}
