using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    public GameObject notePrefab;      // UI Note prefab (with RectTransform)
    public RectTransform spawnPoint;   // Top of lane in canvas
    public RectTransform noteLine;     // Hit line in canvas
    public float bpm = 120f;
    public int beatsAhead = 2;

    private float secondsPerBeat;
    private float songPosition;
    private float nextBeatTime;

    void Start()
    {
        secondsPerBeat = 60f / bpm;
        songPosition = 0f;
        nextBeatTime = secondsPerBeat;
    }

    void Update()
    {
        songPosition += Time.deltaTime;

        if (songPosition >= nextBeatTime)
        {
            SpawnNote();
            nextBeatTime += secondsPerBeat;
        }
    }

    void SpawnNote()
    {
        GameObject note = Instantiate(notePrefab, spawnPoint.position, Quaternion.identity, spawnPoint.parent);

        // Calculate speed so it reaches the line in "beatsAhead" beats
        float travelTime = secondsPerBeat * beatsAhead;
        float distance = spawnPoint.anchoredPosition.y - noteLine.anchoredPosition.y;
        float speed = distance / travelTime;

        Note n = note.GetComponent<Note>();
        n.speed = speed;
        n.targetLine = noteLine;
    }
}
