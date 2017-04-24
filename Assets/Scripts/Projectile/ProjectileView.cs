using ElementalTowerDefenseModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileView : ModelView<ProjectileController> {

    private TargetFollower follower;

    void Update()
    {
        if (follower != null)
            if (follower.Target == null)
                Destroy(gameObject);
    }

    void OnCollisionStay(Collision collision)
    {
        if (follower == null) follower = GetComponent<TargetFollower>();

        if (follower.Target == collision.gameObject)
        {
            Controller.OnMonsterHit(collision.gameObject.GetComponent<MonsterView>());
            Destroy(gameObject);
        }
    }
}
