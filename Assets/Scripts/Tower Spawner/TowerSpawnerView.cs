using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TowerSelectorView))]
public class TowerSpawnerView : View<TowerSpawnerController> {

    public bool TowerIsPlaceable {
        get
        {
            return isOnPlaceableGround && 
                !isColliding && 
                Controller.HasGoldForTower &&
                !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject();
        }
    }

    public Material placeableMaterial;
    public Material notPlaceableMaterial;

    public string placeableTag;
    public string notPlaceableTag;
    public string collisionObstacleTag;
    public string shootingRadiusTag;

    public string layerMaskToProjectOn;
    public new GameObject camera;

    public bool IsInSpawnMode = false;
    
    private bool isColliding = false;
    private bool isOnPlaceableGround = false;

    private Cooldown collisionCooldown;
    private TowerSelectorView towerSelectorView;

    private new BoxCollider collider;
    private Vector3 initialColliderSize;

    void Start () {
        collisionCooldown = new Cooldown(0.1f);
        towerSelectorView = GetComponent<TowerSelectorView>();
	}
    
	void Update () {
        collisionCooldown.Update(Time.deltaTime);
        if (collisionCooldown.IsOver) isColliding = false;
        else isColliding = true;

        RaycastForProjectionState();
        VisualizePlaceability();
	}

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == collisionObstacleTag || collision.gameObject.tag == notPlaceableTag)
        {
            collisionCooldown.Reset();
        }
    }

    private void RaycastForProjectionState()
    {
        if (!IsInSpawnMode)
        {
            towerSelectorView.SetProjectionVisibility(false);
            return;
        }

        RaycastHit hit;
        Ray ray = camera.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << LayerMask.NameToLayer(layerMaskToProjectOn)))
        {
            HideProjectionIfMouseIsOverUI();

            transform.position = hit.point;

            if (hit.collider.gameObject.tag == placeableTag) isOnPlaceableGround = true;
            else if (hit.collider.gameObject.tag == notPlaceableTag) isOnPlaceableGround = false;
            else if (hit.collider.gameObject.tag == collisionObstacleTag) towerSelectorView.SetProjectionVisibility(false);
        } else
        {
            towerSelectorView.SetProjectionVisibility(false);
        }
    }

    private void VisualizePlaceability()
    {
        Material material = TowerIsPlaceable ? placeableMaterial : notPlaceableMaterial;

        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in renderers)
        {
            if (renderer.tag != shootingRadiusTag)
                renderer.material = material;
        }
    }

    private void HideProjectionIfMouseIsOverUI()
    {
        if (collider == null)
        {
            collider = GetComponent<BoxCollider>();
            initialColliderSize = collider.size;
        }

        if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            towerSelectorView.SetProjectionVisibility(false);
            collider.size = new Vector3();
        }
        else
        {
            towerSelectorView.SetProjectionVisibility(true);
            collider.size = initialColliderSize;
        }  
    }
}
