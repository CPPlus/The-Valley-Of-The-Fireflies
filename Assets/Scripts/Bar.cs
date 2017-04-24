using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using ElementalTowerDefenseModel;

public class Bar : MonoBehaviour
{
    public Image pointsImage;

    public void UpdateState(float points, float maxPoints)
    {
        pointsImage.fillAmount = points / maxPoints;
    }

    protected void Update()
    {

    }
}

