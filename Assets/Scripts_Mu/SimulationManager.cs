using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class SimulationManager : MonoBehaviour
{
    public XRRayInteractor left;
    public XRRayInteractor right;
    private Volume volume;
    private Bloom bloom;
    private ChromaticAberration chromaticAberration;
    private LensDistortion lensDistortion;
    
    void Start()
    {
        GameObject leftController = GameObject.Find("Left Controller");
        if (leftController != null)
            left = leftController.GetComponent<XRRayInteractor>();
        GameObject rightController = GameObject.Find("Right Controller");
        if (rightController != null)
            right = rightController.GetComponent<XRRayInteractor>();

        if (left != null)
            left.selectEntered.AddListener(SelectedDrug);
        if (right != null)
            right.selectEntered.AddListener(SelectedDrug);

        GameObject globalVolume = GameObject.Find("Global Volume");
        if (globalVolume != null)
        {
            volume = globalVolume.GetComponent<Volume>();
            volume.profile.TryGet(out bloom);
            volume.profile.TryGet(out chromaticAberration);
            volume.profile.TryGet(out lensDistortion);
        }
        
    }

    private void SelectedDrug(SelectEnterEventArgs args)
    {
        GameObject interactableObject = args.interactableObject.transform.gameObject;
        if (interactableObject.CompareTag("Drug"))
        {
            StartCoroutine(CocaineEffect());
            Destroy(interactableObject);
        }
    }

    
    private IEnumerator CocaineEffect()
    {
        float duration = 10f;
        float startBloomIntensity = 0;
        float targetBloomIntensity = 2f; 
        float startChromaticAberration = 0;
        float targetChromaticAberration = 1f;
        float startLensDistortion = 0;
        float targetLensDistortion = 0.8f;
        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            float t = time / duration;
            bloom.intensity.value = Mathf.Lerp(startBloomIntensity, targetBloomIntensity, t);
            chromaticAberration.intensity.value = Mathf.Lerp(startChromaticAberration, targetChromaticAberration, t);
            lensDistortion.intensity.value = Mathf.Lerp(startLensDistortion, targetLensDistortion, t);
            yield return null;
        }
        bloom.intensity.value = targetBloomIntensity;
        chromaticAberration.intensity.value = targetChromaticAberration;
        lensDistortion.intensity.value = targetLensDistortion;
    }
    
}
