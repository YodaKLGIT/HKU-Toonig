using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BeatController : MonoBehaviour
{
    public float bpm = 120f;
    public float tolerance = 0.25f;
    public Character attacker1;
    public Character attacker2;
    public Image beatIndicator; // UI Image reference
    public float fadeTime = 0.3f;

    private float secondsPerBeat;
    private float songPosition;
    private float nextBeatTime;
    private Coroutine beatFlashRoutine;

    void Start()
    {
        secondsPerBeat = 60f / bpm;
        nextBeatTime = secondsPerBeat;
        songPosition = 0f;

        if (beatIndicator != null)
        {
            Color c = beatIndicator.color;
            c.a = 0f;
            beatIndicator.color = c;
        }
    }

    void Update()
    {
        songPosition += Time.deltaTime;

        // Input check
        if (Input.GetKeyDown(KeyCode.Space))
        {
            float distanceToBeat = Mathf.Abs(songPosition - nextBeatTime);
            if (distanceToBeat <= tolerance)
            {
                if (Random.value < 0.5f) attacker1.Attack();
                else attacker2.Attack();

                // flash green for success
                if (beatIndicator != null)
                {
                    if (beatFlashRoutine != null) StopCoroutine(beatFlashRoutine);
                    beatFlashRoutine = StartCoroutine(BeatFlash(Color.green));
                }
            }
            else
            {
                Debug.Log("Missed the beat!");

                // flash red for miss
                if (beatIndicator != null)
                {
                    if (beatFlashRoutine != null) StopCoroutine(beatFlashRoutine);
                    beatFlashRoutine = StartCoroutine(BeatFlash(Color.red));
                }
            }
        }

        // Beat reached (neutral pulse)
        if (songPosition >= nextBeatTime)
        {
            nextBeatTime += secondsPerBeat;

            if (beatIndicator != null)
            {
                if (beatFlashRoutine != null) StopCoroutine(beatFlashRoutine);
                beatFlashRoutine = StartCoroutine(BeatFlash(Color.white));
            }
        }
    }

    IEnumerator BeatFlash(Color flashColor)
    {
        Color c = flashColor;
        c.a = 1f;
        beatIndicator.color = c;

        float t = 0f;
        while (t < fadeTime)
        {
            t += Time.deltaTime;
            c.a = Mathf.Lerp(1f, 0f, t / fadeTime);
            beatIndicator.color = c;
            yield return null;
        }

        c.a = 0f;
        beatIndicator.color = c;
    }
}
