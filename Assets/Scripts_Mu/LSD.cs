using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;


public class LSD : MonoBehaviour, IDrugEffect
{
    
    public void Start()
    {
        
    }

    public IEnumerator ApplyEffect(Volume volume)
    {
        float duration = 60f;
        float time = 0f;


        while (time < duration)
        {
            time += Time.deltaTime;
            float t = time / duration;
            
            yield return null;
        }

    }
}
