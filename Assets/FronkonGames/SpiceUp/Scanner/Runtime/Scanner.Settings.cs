////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Copyright (c) Martin Bustos @FronkonGames <fronkongames@gmail.com>. All rights reserved.
//
// THIS FILE CAN NOT BE HOSTED IN PUBLIC REPOSITORIES.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
// COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR
// OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace FronkonGames.SpiceUp.Scanner
{
  ///------------------------------------------------------------------------------------------------------------------
  /// <summary> Settings. </summary>
  /// <remarks> Only available for Universal Render Pipeline. </remarks>
  ///------------------------------------------------------------------------------------------------------------------
  public sealed partial class Scanner
  {
    /// <summary> Settings. </summary>
    [Serializable]
    public sealed class Settings
    {
      public Settings() => ResetDefaultValues();

      /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
      #region Common settings.

      /// <summary> Controls the intensity of the effect [0, 1]. Default 1. </summary>
      /// <remarks> An effect with Intensity equal to 0 will not be executed. </remarks>
      public float intensity = 1.0f;
      #endregion
      /////////////////////////////////////////////////////////////////////////////////////////////////////////////////

      /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
      #region Scanner settings.

      /// <summary> Lines strength [0, 1]. Default 0. </summary>
      public float strength = 0.0f;

      /// <summary> Lines count [0, 1000]. Default 500. </summary>
      public int linesCount = 500;

      /// <summary> Tint. </summary>
      public Color linesTint = Color.green;

      /// <summary> Color blend. Default Multiply. </summary>
      public ColorBlends linesBlend = ColorBlends.Multiply;

      /// <summary> Tint. </summary>
      public Color backgroundTint = Color.black;

      /// <summary> Color blend. Default SoftLight. </summary>
      public ColorBlends backgroundBlend = ColorBlends.SoftLight;

      /// <summary> Scanline strength [0, 1]. Default 0.5. </summary>
      public float scanlineStrength = 0.5f;

      /// <summary> Scanline tint color. </summary>
      public Color scanlineTint = Color.green;

      /// <summary> Scanline color blend. Default Overlay. </summary>
      public ColorBlends scanlineBlend = ColorBlends.Overlay;

      /// <summary> Scanline wigth [0, 5]. Default 1. </summary>
      public float scanlineWidth = 1.0f;

      /// <summary> Scanline speed [-5, 5]. Default 0.5. </summary>
      public float scanlineSpeed = 0.5f;

      /// <summary> Noise band strength [0, 1]. Default 0.2. </summary>
      public float noiseBandStrength = 0.2f;

      /// <summary> Noise band color tint. </summary>
      public Color noiseBandTint = Color.green;

      /// <summary> Noise band color blend. Default Additive. </summary>
      public ColorBlends noiseBandBlend = ColorBlends.Additive;

      /// <summary> Noise band width [0, 5]. Default 0.2. </summary>
      public float noiseBandWidth = 0.2f;

      /// <summary> Noise band speed [-5, 5]. Default 0.2. </summary>
      public float noiseBandSpeed = 0.2f;

      /// <summary> Barrel strength [0, 1]. Default 0.5. </summary>
      public float barrelStrength = 0.5f;

      /// <summary> Barrel zoom [0, 1]. Default 1. </summary>
      public float barrelZoom = 1.0f;

      /// <summary> Exterior color. </summary>
      public Color barrelTint = DefaultBarrelColor;

      /// <summary> Interlace noise [0, 1]. Default 0.1. </summary>
      public float interlace = 0.1f;

      /// <summary> Image frame noise [0, 1]. Default 0.05. </summary>
      public float frameNoise = 0.05f;

      /// <summary> Signal noise [0, 1]. Default 0.1. </summary>
      public float signalNoise = 0.1f;

      /// <summary> Bad signal strength [0, 1]. Default 0. </summary>
      public float badSignalStrength = 0.0f;

      /// <summary> Vignette strength [0, 1]. Default 0.6. </summary>
      public float vignetteStrength = 0.6f;

      /// <summary> Vignette blink effect [0, 1]. Default 0.3. </summary>
      public float vignetteBlink = 0.3f;
      #endregion
      /////////////////////////////////////////////////////////////////////////////////////////////////////////////////

      /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
      #region Color settings.

      /// <summary> Brightness [-1.0, 1.0]. Default 0. </summary>
      public float brightness = 0.0f;

      /// <summary> Contrast [0.0, 10.0]. Default 1. </summary>
      public float contrast = 1.0f;

      /// <summary>Gamma [0.1, 10.0]. Default 1. </summary>
      public float gamma = 1.0f;

      /// <summary> The color wheel [0.0, 1.0]. Default 0. </summary>
      public float hue = 0.0f;

      /// <summary> Intensity of a colors [0.0, 2.0]. Default 1. </summary>
      public float saturation = 1.0f;
      #endregion
      /////////////////////////////////////////////////////////////////////////////////////////////////////////////////

      /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
      #region Advanced settings.

      /// <summary> Does it affect the Scene View? </summary>
      public bool affectSceneView = false;

      /// <summary> Filter mode. Default Bilinear. </summary>
      public FilterMode filterMode = FilterMode.Bilinear;

      /// <summary> Render pass injection. Default BeforeRenderingPostProcessing. </summary>
      public RenderPassEvent whenToInsert = RenderPassEvent.BeforeRenderingPostProcessing;

      /// <summary> Enable render pass profiling. </summary>
      public bool enableProfiling = false;
      #endregion
      /////////////////////////////////////////////////////////////////////////////////////////////////////////////////

      public static Color DefaultBarrelColor = new(0.05f, 0.05f, 0.05f, 1.0f);

      /// <summary> Reset to default values. </summary>
      public void ResetDefaultValues()
      {
        intensity = 1.0f;

        strength = 0.0f;
        linesCount = 500;
        linesTint = Color.green;
        linesBlend = ColorBlends.Multiply;
        backgroundTint = Color.black;
        backgroundBlend = ColorBlends.SoftLight;
        scanlineStrength = 0.5f;
        scanlineTint = Color.green;
        scanlineBlend = ColorBlends.Overlay;
        scanlineWidth = 1.0f;
        scanlineSpeed = 0.5f;
        noiseBandStrength = 0.2f;
        noiseBandTint = Color.green;
        noiseBandBlend = ColorBlends.Additive;
        noiseBandWidth = 0.2f;
        noiseBandSpeed = 0.2f;
        barrelStrength = 0.5f;
        barrelZoom = 0.0f;
        barrelTint = DefaultBarrelColor;
        interlace = 0.1f;
        frameNoise = 0.05f;
        signalNoise = 0.1f;
        badSignalStrength = 0.0f;
        vignetteStrength = 0.6f;
        vignetteBlink = 0.3f;

        brightness = 0.0f;
        contrast = 1.0f;
        gamma = 1.0f;
        hue = 0.0f;
        saturation = 1.0f;

        affectSceneView = false;
        filterMode = FilterMode.Bilinear;
        whenToInsert = RenderPassEvent.BeforeRenderingPostProcessing;
        enableProfiling = false;
      }
    }
  }
}
