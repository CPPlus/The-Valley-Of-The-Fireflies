using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils : MonoBehaviour {

	public static Vector3 GetTopPoint(GameObject gameObject, float offset = 0)
    {
        MeshFilter highestMeshFilter = GetHighestMeshFilter(gameObject);

        Vector3 topPoint = new Vector3(
            gameObject.transform.position.x,
            highestMeshFilter.transform.position.y + highestMeshFilter.mesh.bounds.size.y / 2 + offset,
            gameObject.transform.position.z);

        return topPoint;
    }

    public static Vector3 GetMiddlePoint(GameObject gameObject, float offset = 0)
    {
        MeshFilter highestMeshFilter = GetHighestMeshFilter(gameObject);
        MeshFilter lowestMeshFilter = GetLowestMeshFilter(gameObject);

        Vector3 middlePoint = new Vector3(
            gameObject.transform.position.x,
            (lowestMeshFilter.transform.position.y + highestMeshFilter.transform.position.y) / 2 + offset,
            gameObject.transform.position.z);

        return middlePoint;
    }

    private static MeshFilter GetHighestMeshFilter(GameObject gameObject)
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

        return highestMeshFilter;
    }

    private static MeshFilter GetLowestMeshFilter(GameObject gameObject)
    {
        MeshFilter[] filters = gameObject.GetComponentsInChildren<MeshFilter>();
        MeshFilter lowestMeshFilter = null;
        foreach (MeshFilter filter in filters)
        {
            if (lowestMeshFilter == null)
            {
                lowestMeshFilter = filter;
                continue;
            }
            else
            {
                if (lowestMeshFilter.transform.position.y > filter.transform.position.y)
                    lowestMeshFilter = filter;
            }
        }

        return lowestMeshFilter;
    }
}
