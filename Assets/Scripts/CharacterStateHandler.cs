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

    [Header("Sprites")]
    public Sprite NormalSprite;
    public Sprite ActiveSprite1;
    public Sprite ActiveSprite2;

    private bool useFirstActive = true;

    void Update()
    {
        switch (CurrentState)
        {
            case CharacterState.Normal:
                Image.sprite = NormalSprite;
                break;

            case CharacterState.Active:
                Image.sprite = useFirstActive ? ActiveSprite1 : ActiveSprite2;
                break;
        }
    }

    public void ToggleActive()
    {
        CurrentState = CharacterState.Active;
        useFirstActive = !useFirstActive;
    }

    public void ResetToNormal()
    {
        CurrentState = CharacterState.Normal;
    }
}
