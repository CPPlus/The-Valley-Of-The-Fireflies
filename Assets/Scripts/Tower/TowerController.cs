using ElementalTowerDefenseModel;
using System.Collections;
using System.Collections.Generic;

public class TowerController : ModelController<Tower, TowerView> {

    private Cooldown fireCooldownCounter;
    private System.Random random = new System.Random();
    private TowerSpawnerController spawnerController;
    private float sellPrice = 0;
    private bool isSold = false;
    private float reloadPrice = 0;
    
    public TowerController(Tower model, TowerView view, TowerSpawnerController spawnerController) : base(model, view)
    {
        fireCooldownCounter = new Cooldown(Model.Projectile.AttackComp.Speed.Points);
        this.spawnerController = spawnerController;
    }

    public override void UpdateView()
    {
        TowerViewModel viewModel = new TowerViewModel();
        viewModel.Range = Model.Range.Points;
        viewModel.MaxAmmo = Model.Ammo.MaxPoints;
        viewModel.Ammo = Model.Ammo.Points;
        viewModel.IsSold = isSold;
        viewModel.SellPrice = sellPrice;
        viewModel.ReloadPrice = reloadPrice;
        View.UpdateState(viewModel);
    }

    public void Update(float deltaTime)
    {
        fireCooldownCounter.Update(deltaTime);

        if (fireCooldownCounter.IsOver)
        {
            MonsterView[] nearbyMonsters = View.GetMonstersInRange();
            StartShootingAtMonster(nearbyMonsters);
            UpdateView();
        }
    }

    public void OnReload()
    {
        reloadPrice = spawnerController.Reload(this);
        UpdateView();
        reloadPrice = 0;
    }

    public void OnSell()
    {
        sellPrice = spawnerController.Sell(this);
        isSold = true;
        UpdateView();
    }

    public void StartShootingAtMonster(MonsterView[] monsters)
    {
        List<MonsterController> controllers = new List<MonsterController>();
        foreach (MonsterView view in monsters)
        {
            if (!(view.Controller.Model).WillDie)
                controllers.Add(view.Controller);
        }

        if (controllers.Count == 0) return;

        int randomIndex = random.Next(controllers.Count);
        MonsterController monsterController = controllers[randomIndex];

        Projectile projectileModel = Model.Shoot(monsterController.Model);
        fireCooldownCounter = new Cooldown(Model.AttackSpeed);
        if (projectileModel != null)
        {
            ProjectileController projectileController = ProjectileFactory.CreateProjectile(this, projectileModel, projectileModel.Type);
            View.Shoot(monsterController.View, projectileController);
            fireCooldownCounter.Reset();
        }
    }
}
