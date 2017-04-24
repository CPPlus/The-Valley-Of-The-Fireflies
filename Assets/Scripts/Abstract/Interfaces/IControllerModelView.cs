using ElementalTowerDefenseModel;
using System.Collections;
using System.Collections.Generic;

public interface IControllerModelView<T> : IModelView where T : IController
{
    T Controller { get; set; }
}