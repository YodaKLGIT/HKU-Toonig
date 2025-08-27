using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterStateHandler : MonoBehaviour
{
    public enum CharacterState
    {
        Normal,
        Active
    }
    
    public CharacterState CurrentState;
    public Image Image;
    
    public Sprite NormalSprite;
    public Sprite ActiveSprite;

    void Update()
    {
        switch (CurrentState)
        {
            case CharacterState.Normal:
                Image.sprite = NormalSprite;
                break;
            
            case CharacterState.Active:
                Image.sprite = ActiveSprite;
                break;
        }
    }
}
