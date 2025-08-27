using UnityEngine;
using UnityEngine.UI;

public class BeatController : MonoBehaviour
{
    public float bpm = 120f;
    public float tolerance = 0.25f;         // timing window around the beat
    public Character attacker1;
    public Character attacker2;
    public Image beatIndicator;             // shows anticipation & beat pulses
    public Image checkIndicator;            // shows red/green result
    public float anticipationTime = 0.3f;   // how early beat indicator appears
    public float visibleDuration = 0.1f;    // how long flashes last

    private float secondsPerBeat;
    private float songPosition;
    private float nextBeatTime;
    private float beatHideTime;
    private float checkHideTime;
    private bool hasAttemptedThisBeat;

    void Start()
    {
        secondsPerBeat = 60f / bpm;
        nextBeatTime = secondsPerBeat;
        songPosition = 0f;

        if (beatIndicator != null) SetAlpha(beatIndicator, 0f);
        if (checkIndicator != null) SetAlpha(checkIndicator, 0f);
    }

    void Update()
    {
        songPosition += Time.deltaTime;

        // Hide indicators when time is up
        if (beatIndicator != null && Time.time >= beatHideTime) SetAlpha(beatIndicator, 0f);
        if (checkIndicator != null && Time.time >= checkHideTime) SetAlpha(checkIndicator, 0f);

        // Anticipation (slight early warning)
        if (songPosition >= nextBeatTime - anticipationTime && !hasAttemptedThisBeat)
        {
            ShowIndicator(beatIndicator, Color.gray, ref beatHideTime);
        }

        // Player input (only once per beat)
        if (Input.GetKeyDown(KeyCode.Space) && !hasAttemptedThisBeat)
        {
            hasAttemptedThisBeat = true;

            float distanceToBeat = Mathf.Abs(songPosition - nextBeatTime);
            if (distanceToBeat <= tolerance)
            {
                if (Random.value < 0.5f) attacker1.Attack();
                else attacker2.Attack();

                ShowIndicator(checkIndicator, Color.green, ref checkHideTime);
            }
            else
            {
                ShowIndicator(checkIndicator, Color.red, ref checkHideTime);
                Debug.Log("Missed the beat!");
            }
        }

        // Advance to the next beat
        if (songPosition >= nextBeatTime)
        {
            nextBeatTime += secondsPerBeat;
            hasAttemptedThisBeat = false;

            // Beat pulse (neutral white)
            ShowIndicator(beatIndicator, Color.white, ref beatHideTime);
        }
    }

    private void ShowIndicator(Image img, Color color, ref float hideTime)
    {
        if (img == null) return;

        color.a = 1f;
        img.color = color;
        hideTime = Time.time + visibleDuration;
    }

    private void SetAlpha(Image img, float a)
    {
        if (img == null) return;

        Color c = img.color;
        c.a = a;
        img.color = c;
    }
}
