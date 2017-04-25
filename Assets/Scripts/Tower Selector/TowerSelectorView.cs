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

    public void UpdateState(TowerSelectorViewModel data)
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
        spawnerView.IsInSpawnMode = true;
    }


}
