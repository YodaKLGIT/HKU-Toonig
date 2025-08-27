using UnityEngine;
using UnityEngine.UI;

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
    public int hitPoints = 0;// Points gained by hitting notes
    public int damagePoints= 0;
    public int damagedPlayerThreshold = 20;


    public int damagedThreshold = 20;
    public int deadThreshold = 30;

    void Update()
    {
        // Update state based on hit points
        if (hitPoints >= deadThreshold)
            CurrentState = DraakState.Dead;
        else if (hitPoints >= damagedThreshold)
            CurrentState = DraakState.Damaged;
        else
            CurrentState = DraakState.Normal;
        // dragon win
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
    }

    public void AddHitPoints(int points)
    {
        hitPoints += points;
        Debug.Log("Added hit points. Total: " + hitPoints);
    }

    public void AddDamagePoints(int points)
    {
        damagePoints += points;
        Debug.Log("Missed! Added damage points. Total: " + damagePoints);
    }
}
