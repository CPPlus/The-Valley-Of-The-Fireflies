using ElementalTowerDefenseModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpawnerView : ModelView<TowerSpawnerController> {

    public bool TowerIsPlaceable {
        get
        {
            return isOnPlaceableGround &&
                !isColliding &&
                Controller.HasGoldForTower &&
                IsInSpawnMode; // &&
                // UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject();
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

    private new BoxCollider collider;
    private Vector3 initialColliderSize;

    public TowerSpawnerView spawnerView;
    private GameObject towerProjection;
    public GameObject shootingRangeSphere;

    void Start () {
        collisionCooldown = new Cooldown(0.1f);
	}
    
	void Update () {
        towerProjection.transform.position = transform.position;

        collisionCooldown.Update(Time.deltaTime);
        if (collisionCooldown.IsOver) isColliding = false;
        else isColliding = true;

        RaycastForProjectionState();
        VisualizePlaceability();
	}

    public void SetProjectionVisibility(bool isVisible)
    {
        towerProjection.SetActive(isVisible);
        shootingRangeSphere.SetActive(isVisible);
    }

    public void UpdateState(TowerSpawnerViewModel data)
    {
        if (towerProjection != null) Destroy(towerProjection);
        Tower model = ElementalTowerDefenseModel.TowerFactory.CreateTower(data.TowerType);
        TowerController controller
            = TowerFactory.CreateTower(
                data.TowerType,
                model,
                null,
                out towerProjection);
        towerProjection.GetComponent<Rigidbody>().detectCollisions = false;
        towerProjection.GetComponent<TowerView>().enabled = false;
        towerProjection.transform.parent = gameObject.transform;
        transform.GetChild(0).transform.localScale = new Vector3(
            model.Range.Points * 2,
            model.Range.Points * 2,
            model.Range.Points * 2);
        spawnerView.IsInSpawnMode = data.IsInSpawnMode;

        if (!IsInSpawnMode) SetProjectionVisibility(false);
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
            SetProjectionVisibility(false);
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
            else if (hit.collider.gameObject.tag == collisionObstacleTag) SetProjectionVisibility(false);
        } else
        {
            SetProjectionVisibility(false);
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
            SetProjectionVisibility(false);
            collider.size = new Vector3();
        }
        else
        {
            SetProjectionVisibility(true);
            collider.size = initialColliderSize;
        }  
    }
}
