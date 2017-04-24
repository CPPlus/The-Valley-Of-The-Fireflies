using ElementalTowerDefenseModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class ModelView<T> : MonoBehaviour, IControllerModelView<T> where T : IController
{
    public T Controller { get; set; }
}
