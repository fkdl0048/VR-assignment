using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
using FronkonGames.SpiceUp.Scanner;

public class LSD : MonoBehaviour, IDrugEffect
{
    Scanner.Settings settings;
    public void Start()
    {
        SoundManager.Instance.PlaySound("drugScene");

        if (Scanner.IsInRenderFeatures() == false)
            Scanner.AddRenderFeature();

        settings = Scanner.GetSettings();
    }

    public IEnumerator ApplyEffect(Volume volume)
    {
        SoundManager.Instance.PlaySound("effectStart");

        yield return new WaitForSeconds(2.0f);

        float duration = 60f;
        float time = 0f;
        float signalNoiseElapsedTime = 0f;
        float signalNoiseDuration = 2f;
        float signalNoiseOnDuration = 0.5f;
        float hueElapsedTime = 0f;
        float hueDuration = 2f;

        settings.frameNoise = 0.1f;
        settings.linesCount = 0;
        settings.scanlineStrength = 0f;
        settings.interlace = 0.1f;
        settings.barrelStrength = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            float t = time / duration;

            settings.strength = Mathf.Lerp(0f, 1f, t);

            signalNoiseElapsedTime += Time.deltaTime;
            if (signalNoiseElapsedTime >= signalNoiseDuration)
                signalNoiseElapsedTime = 0f;
            if (signalNoiseElapsedTime < signalNoiseOnDuration)
                settings.signalNoise = 0.3f;
            else
                settings.signalNoise = 0f;

            hueElapsedTime += Time.deltaTime;
            if (hueElapsedTime >= hueDuration)
            {
                hueElapsedTime = 0f;
                settings.hue = Random.Range(0f, 1f);
            }

            if (time != 0 && time % 15f < Time.deltaTime)
            {
                SoundManager.Instance.PlaySound("effect");
            }

            yield return null;
        }
    }

}
