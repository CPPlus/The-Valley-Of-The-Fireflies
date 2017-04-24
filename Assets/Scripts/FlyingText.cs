using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlyingText : MonoBehaviour {

    public Text text;
    public float distanceToFly;
    public float flySpeed;

    private float distanceFlown = 0;

    public void Update()
    {
        if (distanceFlown < distanceToFly)
        {
            float distanceToFlyDelta = flySpeed * Time.deltaTime;
            transform.Translate(new Vector3(0, distanceToFlyDelta, 0));
            distanceFlown += distanceToFlyDelta;
        } else
        {
            Destroy(gameObject);
        }
    }

	public void SetColor(Color color)
    {
        text.color = color;
    }

    public void SetText<T>(T text)
    {
        this.text.text = text.ToString();
    }
}
