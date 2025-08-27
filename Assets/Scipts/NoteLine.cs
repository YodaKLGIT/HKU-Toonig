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
        Note[] notes = FindObjectsOfType<Note>();
        currentNote = null;

        foreach (var note in notes)
        {
            float distance = Mathf.Abs(note.transform.position.y - noteLine.position.y);

            // Check for hit
            if (distance <= hitThreshold)
            {
                currentNote = note;
                break;
            }

            // Check if note passed the line (missed)
            if (note.transform.position.y < noteLine.position.y + 1 - hitThreshold)
            {
                // Apply damage points for miss
                if (draakStateHandler != null)
                {

                    draakStateHandler.AddDamagePoints(1); // Add 1 damage for missed note
                }

                Debug.Log("damage!");
                Destroy(note.gameObject);
            }
        }

        // Hit input
        if (Input.GetKeyDown(KeyCode.Space) && currentNote != null)
        {
            Debug.Log("Hit!");

            if (draakStateHandler != null)
            {
                draakStateHandler.AddHitPoints(1); // Add 1 hit point for successful note
            }

            Destroy(currentNote.gameObject);
            currentNote = null;
        }
    }
}
