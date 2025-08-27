using UnityEngine;
using UnityEngine.UI;

public class NoteLine : MonoBehaviour
{
    public RectTransform noteLine;
    public float hitThreshold = 3f; // Pixels above/below line that count as hit
    private Note currentNote;

    // Reference to your DraakStateHandler
    public DraakStateHandler draakStateHandler;
    void Start()
    {
        if (draakStateHandler == null)
        {
            draakStateHandler = FindObjectOfType<DraakStateHandler>();
            if (draakStateHandler == null)
                Debug.LogError("No DraakStateHandler found in scene!");
        }
    }

    void Update()
    {
        // Track notes that are close to the line
        Note[] notes = FindObjectsOfType<Note>();
        currentNote = null;
        foreach (var note in notes)
        {
            float distance = Mathf.Abs(note.transform.position.y - noteLine.position.y);
            if (distance <= hitThreshold)
            {
                currentNote = note;
                break;
            }
        }

        // Hit input
        if (Input.GetKeyDown(KeyCode.Space) && currentNote != null)
        {
            Debug.Log("Hit!");

            if (draakStateHandler != null)
            {
                draakStateHandler.AddHitPoints(1);
            }
            else {
                Debug.Log("not working maannn");
            }

                Destroy(currentNote.gameObject);
            currentNote = null;
        }
    }
}
