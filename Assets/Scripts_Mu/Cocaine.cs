using FronkonGames.SpiceUp.Drunk;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.XR.Interaction.Toolkit;


public class Cocaine : MonoBehaviour, IDrugEffect
{
    private ChromaticAberration chromaticAberration;
    private DepthOfField depthOfField;
    private MotionBlur motionblur;
    private PaniniProjection paniniProjection;
    public Camera mainCamera;
    Drunk.Settings settings;
    [SerializeField]
    private ActionBasedContinuousMoveProvider moveProvider;

    void Start()
    {
        mainCamera = Camera.main;
        if (Drunk.IsInRenderFeatures() == false)
            Drunk.AddRenderFeature();
        if (moveProvider == null)
            moveProvider = GameObject.Find("XR Origin (XR Rig)").GetComponent<ActionBasedContinuousMoveProvider>();

        settings = Drunk.GetSettings();
    }

    public IEnumerator ApplyEffect(Volume volume)
    {
        yield return new WaitForSeconds(0.5f);

        float duration = 60f;
        float startChromaticAberration = 0;
        float targetChromaticAberration = 1f;
        float time = 0f;

        volume.profile.TryGet(out chromaticAberration);
        volume.profile.TryGet(out depthOfField);
        volume.profile.TryGet(out motionblur);
        volume.profile.TryGet(out paniniProjection);
        
        mainCamera.fieldOfView = 80f;
        motionblur.active = true;
        motionblur.intensity.Override(0.8f);
        settings.drunkenness = 0.1f;
        moveProvider.moveSpeed = 2f;

        while (time < duration)
        {
            time += Time.deltaTime;
            float t = time / duration;
            chromaticAberration.intensity.value = Mathf.Lerp(startChromaticAberration, targetChromaticAberration, t*3);
            
            yield return null;
        }
        
    }
}
