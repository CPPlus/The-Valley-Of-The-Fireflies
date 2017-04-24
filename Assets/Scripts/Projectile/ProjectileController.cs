using ElementalTowerDefenseModel;
using System.Collections;
using System.Collections.Generic;
using System;

public class ProjectileController : ModelController<Projectile, ProjectileView>
{
    public ProjectileController(Projectile Model, ProjectileView View) : base(Model, View)
    {
    }

    public void OnMonsterHit(MonsterView view)
    {
        view.Controller.Model.GetShot(Model);
        view.Controller.UpdateView();
    }

    public override void UpdateView()
    {
        throw new NotImplementedException();
    }
}
