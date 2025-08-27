using UnityEngine;

public class Note : MonoBehaviour
{
    [HideInInspector] public float speed;
    [HideInInspector] public RectTransform targetLine;
    private RectTransform rect;
    public bool wasMissed = false;

    public float destroyY = -10f; // Y position at which note is destroyed

    void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    void Update()
    {
        if (targetLine == null) return;

        // Move downward continuously
        rect.anchoredPosition -= new Vector2(0f, speed * Time.deltaTime);

        // Destroy if it goes far below the canvas
        if (rect.anchoredPosition.y <= destroyY)
        {
            Destroy(gameObject);
        }
    }


}
