using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.XR.Interaction.Toolkit.Transformers;

public class SimulationManager : MonoBehaviour
{
    public XRRayInteractor left;
    public XRRayInteractor right;
    private Volume volume;

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

    }

    private void SelectedDrug(SelectEnterEventArgs args)
    {
        GameObject interactableObject = args.interactableObject.transform.gameObject;
        if (interactableObject.CompareTag("Drug"))
        {
            IDrugEffect drugEffect = interactableObject.GetComponent<IDrugEffect>();
            StartCoroutine(drugEffect.ApplyEffect(volume));
            Destroy(interactableObject.GetComponent<XRGrabInteractable>());
            Destroy(interactableObject.GetComponent<XRGeneralGrabTransformer>());
        }
    }

    
    
    
}
