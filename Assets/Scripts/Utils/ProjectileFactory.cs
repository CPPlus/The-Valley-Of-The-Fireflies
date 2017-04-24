using ElementalTowerDefenseModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileFactory : MonoBehaviour {

    private static ProjectileFactory instance;

    public GameObject rockProjectilePrefab;
    public GameObject fireballProjectilePrefab;

    void Start()
    {
        instance = this;
    }

    public static ProjectileController CreateProjectile(TowerController towerController, Projectile model, ProjectileType type)
    {
        GameObject prefab = null;
        switch (type)
        {
            case ProjectileType.ROCK:
                prefab = instance.rockProjectilePrefab;
                break;
            case ProjectileType.FIREBALL:
                prefab = instance.fireballProjectilePrefab;
                break;
        }

        GameObject projectile = Instantiate(
            prefab, 
            towerController.View.transform.position, 
            towerController.View.transform.rotation);
        ProjectileView view = projectile.GetComponent<ProjectileView>();
        ProjectileController controller = new ProjectileController(model, view);
        view.Controller = controller;

        return controller;
    }
}
