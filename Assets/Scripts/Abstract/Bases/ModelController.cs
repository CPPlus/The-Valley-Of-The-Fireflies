using ElementalTowerDefenseModel;
using System;
using System.Collections;
using System.Collections.Generic;

public abstract class ModelController<T, U> : IController where T : IModel where U : IModelView
{
    public T Model { get; set; }
    public U View { get; set; }

    public ModelController(T Model, U View)
    {
        this.Model = Model;
        this.View = View;
    }

    public abstract void UpdateView();
}
