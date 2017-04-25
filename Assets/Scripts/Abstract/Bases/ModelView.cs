using ElementalTowerDefenseModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ModelView<T> : MonoBehaviour, IModelView where T : IController
{
    public T Controller { get; set; }
}