using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldManagerView : ModelView<GoldManagerController> {

    public Text amount;

    public void UpdateState(float gold)
    {
        amount.text = gold.ToString();
    }
}
