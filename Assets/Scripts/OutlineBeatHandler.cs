using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineBeatHandler : MonoBehaviour
{
    public bool DebugInput = false;
    
    public float BeatIntensity = 1f;
    public float FadeOutMultiplier = .1f;
    
    public Material OutlineMaterial;

    private float currentIntensity;

    public void OnBeat()
    {
        currentIntensity = BeatIntensity;
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
            OnBeat();
        }
    }
}
