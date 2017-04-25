using ElementalTowerDefenseModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PathFollower))]
[RequireComponent(typeof(PrefabAttacher))]
public class MonsterView : ModelView<MonsterController> {

    private PathFollower follower;
    private PrefabAttacher attacher;
    public Bar healthBar;

	void Start () {
        
	}
	
	void Update () {
		
	}

    public void OnShot(Projectile projectile)
    {
        Controller.OnShot(projectile);
    }

    public void UpdateState(MonsterViewModel viewModel)
    {
        if (follower == null) follower = GetComponent<PathFollower>();
        if (attacher == null)
        {
            attacher = GetComponent<PrefabAttacher>();
            attacher.AttachPrefab();
            healthBar = attacher.GetInstance().GetComponent<Bar>();
            healthBar.pointsImage 
                = attacher.
                GetInstance().
                transform.Find("Health").
                GetComponent<Image>();
        }

        healthBar.UpdateState(viewModel.Health, viewModel.MaxHealth);

        follower.movementSpeed = viewModel.Speed;
        if (viewModel.IsDead)
        {
            FlyingTextSpawner.SpawnGoldEarned(viewModel.KillReward, gameObject);
            Destroy(gameObject);
        }
    }
}
