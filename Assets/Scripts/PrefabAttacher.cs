using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabAttacher : MonoBehaviour {

    public GameObject prefab;
    public Vector3 offset;

    private GameObject instance;

    public void AttachPrefab()
    {
        prefab.transform.position = new Vector3();
        instance = Instantiate(prefab, transform, false);
        instance.transform.position += offset;
    }

    public GameObject GetInstance()
    {
        return instance;
    }
}
