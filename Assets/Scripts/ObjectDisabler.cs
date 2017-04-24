using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDisabler : MonoBehaviour {

    public GameObject toBeDisabled;

    public bool IsEnabled { get { return toBeDisabled.activeInHierarchy; } }

    public void Disable()
    {
        toBeDisabled.SetActive(false);
    }

    public void Enable()
    {
        toBeDisabled.SetActive(true);
    }
}
