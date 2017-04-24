using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils : MonoBehaviour {

	public static Vector3 GetTopPoint(GameObject gameObject, float offset = 0)
    {
        MeshFilter[] filters = gameObject.GetComponentsInChildren<MeshFilter>();
        MeshFilter highestMeshFilter = null;
        foreach (MeshFilter filter in filters)
        {
            if (highestMeshFilter == null)
            {
                highestMeshFilter = filter;
                continue;
            }
            else
            {
                if (highestMeshFilter.transform.position.y < filter.transform.position.y)
                    highestMeshFilter = filter;
            }
        }

        Vector3 topPoint = new Vector3(
            gameObject.transform.position.x,
            highestMeshFilter.transform.position.y + highestMeshFilter.mesh.bounds.size.y / 2 + offset,
            gameObject.transform.position.z);

        return topPoint;
    }
}
