using System.Collections;
using System.Collections.Generic;
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

    void Update()
    {
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
}
