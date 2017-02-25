using ElementalTowerDefenseModel;
using System.Collections;
using System.Collections.Generic;

public class MonsterController {

    public Monster Model { get; set; }
    public MonsterView View { get; set; }

    public void UpdateView()
    {
        MonsterViewModel viewModel = new MonsterViewModel();
        viewModel.Speed = Model.MovementComp.Speed.Points;
        viewModel.IsDead = Model.HealthComp.IsDead;
        View.UpdateState(viewModel);
    }
}
