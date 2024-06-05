using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.XR.Interaction.Toolkit.Transformers;
using FronkonGames.SpiceUp.Drunk;

public class SimulationManager : MonoBehaviour
{
    public XRRayInteractor left;
    public XRRayInteractor right;
    private Volume volume;
    Drunk.Settings settings;

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
            volume = globalVolume.GetComponent<Volume>();

        if (Drunk.IsInRenderFeatures() == false)
            Drunk.AddRenderFeature();
        settings = Drunk.GetSettings();
        settings.ResetDefaultValues();
        


        //---------Test----------
        GameObject[] drugs = GameObject.FindGameObjectsWithTag("Drug");
        foreach (GameObject drug in drugs)
        {
            IDrugEffect drugEffect = drug.GetComponent<IDrugEffect>();
            StartCoroutine(BrightnessEffect());
            StartCoroutine(drugEffect.ApplyEffect(volume));
        }
    }



    private void SelectedDrug(SelectEnterEventArgs args)
    {
        GameObject interactableObject = args.interactableObject.transform.gameObject;
        if (interactableObject.CompareTag("Drug"))
        {
            StartCoroutine(BrightnessEffect());
            IDrugEffect drugEffect = interactableObject.GetComponent<IDrugEffect>();
            StartCoroutine(drugEffect.ApplyEffect(volume));
            Destroy(interactableObject.GetComponent<XRGrabInteractable>());
            Destroy(interactableObject.GetComponent<XRGeneralGrabTransformer>());
        }
    }

    public IEnumerator BrightnessEffect()
    {
        float duration = 2f;
        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            settings.brightness = Mathf.PingPong(time * (0.8f / (duration / 2)), 0.8f);

            yield return null;
        }
        settings.brightness = 0f;
    }

    private void OnDisable()
    {
        settings.ResetDefaultValues();
    }

}
