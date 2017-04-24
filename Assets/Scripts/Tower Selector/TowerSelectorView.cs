using ElementalTowerDefenseModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSelectorView : ModelView<TowerSelectorController> , IModelView{

    public TowerSpawnerView spawnerView;
    private GameObject towerProjection;
    public GameObject shootingRangeSphere;

    void Start () {

	}
	
	void Update () {
        towerProjection.transform.position = transform.position;
    }

    public void SetProjectionVisibility(bool isVisible)
    {
        towerProjection.SetActive(isVisible);
        shootingRangeSphere.SetActive(isVisible);   
    }

    public void UpdateState(TowerType type)
    {
        if (towerProjection != null) Destroy(towerProjection);
        TowerController controller = TowerFactory.CreateTower(type, ElementalTowerDefenseModel.TowerFactory.CreateTower(type), null);
        towerProjection = controller.View.gameObject;
        towerProjection.GetComponent<Rigidbody>().detectCollisions = false;
        towerProjection.GetComponent<TowerView>().enabled = false;
        towerProjection.transform.parent = gameObject.transform;
        transform.GetChild(0).transform.localScale = new Vector3(
            controller.Model.Range.Points * 2,
            controller.Model.Range.Points * 2,
            controller.Model.Range.Points * 2);
        spawnerView.IsInSpawnMode = true;
    }


}
