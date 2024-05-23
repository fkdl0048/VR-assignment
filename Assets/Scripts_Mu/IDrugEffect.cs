using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
public interface IDrugEffect
{
    public IEnumerator ApplyEffect(Volume volumn);
}
