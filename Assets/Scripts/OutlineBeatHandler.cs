using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineBeatHandler : MonoBehaviour
{
    public bool DebugInput = false;
    
    public float BeatIntensity = 1f;
    public float FadeOutMultiplier = 1f;
    
    public Material OutlineMaterial;
    public Color CorrectColor;
    public Color IncorrectColor;

    private float currentIntensity;

    public void OnBeat(bool isCorrect)
    {
        currentIntensity = BeatIntensity;
        
        var color = isCorrect ? CorrectColor : IncorrectColor;
        OutlineMaterial.SetColor("_Color", color);
        
        StopAllCoroutines();
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        while (currentIntensity > 0)
        {
            currentIntensity -= FadeOutMultiplier * Time.deltaTime;
            yield return null;
        }
        
        currentIntensity = 0;
    }

    void Update()
    {
        OutlineMaterial.SetFloat("_Intensity", currentIntensity);

        if (!DebugInput)
            return;

        if (Input.GetKeyDown(KeyCode.B))
        {
            OnBeat(true);
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            OnBeat(false);
        }
    }
}
