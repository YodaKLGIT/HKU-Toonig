using UnityEngine;

public class NoteLine : MonoBehaviour
{
    public RectTransform noteLine;
    public float hitThreshold = 3f;
    private Note currentNote;

    public DraakStateHandler draakStateHandler;

    [Header("Characters that react to hits")]
    public CharacterStateHandler[] characterHandlers;

    [Header("Note cleanup")]
    public float missYThreshold = -2000f; // y-position at which notes are destroyed

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
            if (note.transform.position.y < noteLine.position.y - hitThreshold && !note.wasMissed)
            {
                if (draakStateHandler != null)
                    draakStateHandler.AddDamagePoints(1);

                Debug.Log("damage!");
                note.wasMissed = true; // flag so damage only applied once
            }

            // Cleanup once note is far enough down
            if (note.transform.position.y < missYThreshold)
            {
                Destroy(note.gameObject);
            }
        }

        // Hit input
        if (Input.GetKeyDown(KeyCode.Space) && currentNote != null)
        {
            Debug.Log("Hit!");

            if (draakStateHandler != null)
                draakStateHandler.AddHitPoints(1);

            if (characterHandlers.Length > 0)
            {
                int randomIndex = Random.Range(0, characterHandlers.Length);
                CharacterStateHandler chosen = characterHandlers[randomIndex];

                if (chosen != null)
                {
                    chosen.ToggleActive();
                    CancelInvoke(nameof(ResetCharacters));
                    Invoke(nameof(ResetCharacters), 0.2f);
                }
            }

            Destroy(currentNote.gameObject);
            currentNote = null;
        }
    }

    void ResetCharacters()
    {
        foreach (var handler in characterHandlers)
        {
            if (handler != null)
                handler.ResetToNormal();
        }
    }
}
