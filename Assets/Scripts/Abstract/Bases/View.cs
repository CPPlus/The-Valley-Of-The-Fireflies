using ElementalTowerDefenseModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class View<T> : MonoBehaviour, IView where T : IController {

    public T Controller { get; set; }
}
