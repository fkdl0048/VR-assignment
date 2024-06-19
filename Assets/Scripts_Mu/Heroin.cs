using FronkonGames.SpiceUp.Drunk;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.XR.Interaction.Toolkit;

public class Heroin : MonoBehaviour, IDrugEffect
{
    [SerializeField]
    private ActionBasedContinuousMoveProvider moveProvider;
    Drunk.Settings settings;

    void Start()
    {
        SoundManager.Instance.PlaySound("drugScene");

        if (Drunk.IsInRenderFeatures() == false)
            Drunk.AddRenderFeature();
        if (moveProvider == null)
            moveProvider = GameObject.Find("XR Origin (XR Rig)").GetComponent<ActionBasedContinuousMoveProvider>();

        settings = Drunk.GetSettings();
    }

    public IEnumerator ApplyEffect(Volume volume)
    {
        SoundManager.Instance.PlaySound("effectStart");

        yield return new WaitForSeconds(2.0f);

        float duration = 60f;
        float time = 0f;
        moveProvider.moveSpeed = 0.6f;

        while (time < duration)
        {
            time += Time.deltaTime;
            float t = time / duration;
            
            settings.drunkenness = Mathf.Lerp(0f, 0.6f, t);
            settings.hue = Mathf.Lerp(0.1f, 0.2f, Mathf.PingPong(time * (30f / duration), 1f));

            if (time != 0 && time % 15f < Time.deltaTime)
            {
                SoundManager.Instance.PlaySound("effect");
            }
            yield return null;
        }
    }
}
