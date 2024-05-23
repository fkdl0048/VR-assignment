using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;


public class Cocaine : MonoBehaviour, IDrugEffect
{
    private ChromaticAberration chromaticAberration;
    private DepthOfField depthOfField;
    private MotionBlur motionblur;
    private PaniniProjection paniniProjection;


    public IEnumerator ApplyEffect(Volume volume)
    {
        float duration = 30f;
        float startChromaticAberration = 0;
        float targetChromaticAberration = 1f;
        float startPaniniProjection = 0;
        float targetPaniniProjection = 0.3f;
        float startMotionBlur = 0;
        float endMotionBlur = 0.5f;
        float time = 0f;

        volume.profile.TryGet(out chromaticAberration);
        volume.profile.TryGet(out depthOfField);
        volume.profile.TryGet(out motionblur);
        volume.profile.TryGet(out paniniProjection);

        while (time < duration)
        {
            time += Time.deltaTime;
            float t = time / duration;
            chromaticAberration.intensity.value = Mathf.Lerp(startChromaticAberration, targetChromaticAberration, t);
            paniniProjection.distance.value = Mathf.Lerp(startPaniniProjection, targetPaniniProjection, t);
            motionblur.intensity.value = Mathf.Lerp(startMotionBlur, endMotionBlur, t);


            float cycleDuration = duration / 5;
            float halfCycle = cycleDuration / 2;
            float cycleTime = time % cycleDuration;
            if (cycleTime < halfCycle)
                depthOfField.gaussianStart.value = Mathf.Lerp(0f, 20f, cycleTime / halfCycle);
            else
                depthOfField.gaussianStart.value = Mathf.Lerp(20f, 0f, (cycleTime - halfCycle) / halfCycle);
            
            yield return null;
        }
        
    }
}
