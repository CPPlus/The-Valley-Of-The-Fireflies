using UnityEngine;

public class FlyingTextSpawner : MonoBehaviour {

    private const float offset = 0.3f;
    public GameObject flyingTextPrefab;
    private static FlyingTextSpawner instance;

    public void Start()
    {
        instance = this;
    }

    public static void Spawn<T>(T text, Color color, Vector3 position)
    {
        GameObject flyingTextObject = Instantiate(instance.flyingTextPrefab, position, Quaternion.identity);
        FlyingText flyingText = flyingTextObject.GetComponent<FlyingText>();
        flyingText.SetText(text);
        flyingText.SetColor(color);
    }

    public static void Spawn<T>(T text, Color color, GameObject gameObject)
    {
        
        Spawn(text, color, Utils.GetTopPoint(gameObject, offset));
    }

    public static void SpawnAmmunitionLeft(int percentage, GameObject gameObject)
    {
        string ammunitionLeftText = string.Format(
                "{0}%",
                percentage);
        Spawn(ammunitionLeftText, new Color(1, 0.5f, 0.3f), gameObject);
    }

    public static void SpawnGoldEarned(float gold, GameObject gameObject)
    {
        string goldEarnedText = string.Format(
                "+{0}g",
                gold);
        Spawn(goldEarnedText, Color.yellow, gameObject);
    }

    public static void SpawnGoldSpent(float gold, GameObject gameObject)
    {
        string goldEarnedText = string.Format(
                "-{0}g",
                gold);
        Spawn(goldEarnedText, Color.yellow, gameObject);
    }
}
