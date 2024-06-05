using FronkonGames.SpiceUp.Drunk;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Heroin : MonoBehaviour, IDrugEffect
{
    Drunk.Settings settings;
    void Start()
    {
        if (Drunk.IsInRenderFeatures() == false)
            Drunk.AddRenderFeature();

        settings = Drunk.GetSettings();
    }

    public IEnumerator ApplyEffect(Volume volume)
    {
        float duration = 60f;
        float time = 0f;


        while (time < duration)
        {
            time += Time.deltaTime;
            float t = time / duration;
            
            settings.drunkenness = Mathf.Lerp(0f, 0.6f, t);
            settings.hue = Mathf.Lerp(0.1f, 0.7f, Mathf.PingPong(time * (30f / duration), 1f));
            yield return null;
        }
    }
}
