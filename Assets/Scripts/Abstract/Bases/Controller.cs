using ElementalTowerDefenseModel;
using System.Collections;
using System.Collections.Generic;
using System;

public abstract class Controller<T> : IController where T : IView {
    
    public T View { get; set; }

    public Controller(T View)
    {
        this.View = View;
    }

    public abstract void UpdateView();
}
