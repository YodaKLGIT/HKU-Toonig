using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DraakStateHandler : MonoBehaviour
{
    public enum DraakState
    {
        Normal,
        Damaged,
        Dead,
        Victory
    }

    public DraakState CurrentState;
    public Image Image;

    public Sprite NormalSprite;
    public Sprite DamagedSprite;
    public Sprite DeadSprite;
    public Sprite VictorySprite;

    [Header("Health Settings")]
    public int hitPoints = 0; // Points gained by hitting notes
    public int damagePoints = 0; // Points gained by missed notes
    public int damagedPlayerThreshold = 20;

    public int damagedThreshold = 20;
    public int deadThreshold = 30;

    private bool sceneLoaded = false; // Ensure scene loads only once

    void Update()
    {
        // Update state based on hit points
        if (hitPoints >= deadThreshold)
            CurrentState = DraakState.Dead;
        else if (hitPoints >= damagedThreshold)
            CurrentState = DraakState.Damaged;
        else
            CurrentState = DraakState.Normal;

        // Dragon victory if player damage threshold is reached
        if (damagePoints >= damagedPlayerThreshold)
            CurrentState = DraakState.Victory;

        // Update sprite
        switch (CurrentState)
        {
            case DraakState.Normal:
                Image.sprite = NormalSprite;
                break;
            case DraakState.Damaged:
                Image.sprite = DamagedSprite;
                break;
            case DraakState.Dead:
                Image.sprite = DeadSprite;
                break;
            case DraakState.Victory:
                Image.sprite = VictorySprite;
                break;
        }

        // Load ending scene if dead or victory
        if (!sceneLoaded && (CurrentState == DraakState.Dead))
        {
            sceneLoaded = true;
            SceneManager.LoadScene("Ending scene");
        }

        // Load ending scene if dead or victory
        if (!sceneLoaded && (CurrentState == DraakState.Victory))
        {
            sceneLoaded = true;
            SceneManager.LoadScene("defeat scene");
        }
    }

    public void AddHitPoints(int points)
    {
        hitPoints += points;
    }

    public void AddDamagePoints(int points)
    {
        damagePoints += points;
    }
}
