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
    public float fadeTime = 0.3f; // how fast it fades out

    private float secondsPerBeat;
    private float songPosition;
    private float nextBeatTime;

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
            }
            else
            {
                Debug.Log("Missed the beat!");
            }
        }

        // Beat reached
        if (songPosition >= nextBeatTime)
        {
            nextBeatTime += secondsPerBeat;
            if (beatIndicator != null) StartCoroutine(BeatFlash());
        }
    }

    IEnumerator BeatFlash()
    {
        // flash to visible
        Color c = beatIndicator.color;
        c.a = 1f;
        beatIndicator.color = c;

        // fade back out
        float t = 0f;
        while (t < fadeTime)
        {
            t += Time.deltaTime;
            c.a = Mathf.Lerp(1f, 0f, t / fadeTime);
            beatIndicator.color = c;
            yield return null;
        }

        // ensure fully transparent
        c.a = 0f;
        beatIndicator.color = c;
    }
}
