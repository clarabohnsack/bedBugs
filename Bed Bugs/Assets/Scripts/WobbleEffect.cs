using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WobbleEffect : MonoBehaviour
{
    public Material wobbleEffectMaterial;
    private bool wobbleActive = false;
    private float frequency = 4f;
    private float shift = 0f;
    private float amplitude = 0.05f;
    private float shiftSpeed = 5f;


    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, wobbleEffectMaterial);
    }

    private void SetFrequency(float frequency)
    {
        wobbleEffectMaterial.SetFloat("_frequency", frequency);
    }

    private void SetShift(float shift)
    {
        wobbleEffectMaterial.SetFloat("_frequency", frequency);
    }

    private void SetAmplitude(float amplitude)
    {
        wobbleEffectMaterial.SetFloat("_frequency", frequency);
    }

    public void StartWobble()
    {
        wobbleActive = true;
        StartCoroutine(WobbleCoroutine());
    }

    public void StopWobble()
    {
        wobbleActive = false;
    }

    private IEnumerator WobbleCoroutine()
    {
        SetFrequency(frequency);
        SetShift(shift);
        SetAmplitude(amplitude);

        while(wobbleActive)
        {
            shift += shiftSpeed * Time.deltaTime;
            SetShift(shift);
            yield return null;
        }

        shift = 0f;
        enabled = false;
    }
}
