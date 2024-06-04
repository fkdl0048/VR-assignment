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
using UnityEngine;
using UnityEditor;
using static FronkonGames.SpiceUp.DoubleVision.Editor.Inspector;

namespace FronkonGames.SpiceUp.DoubleVision.Editor
{
  /// <summary> Spice up Double Vision inspector. </summary>
  [CustomPropertyDrawer(typeof(DoubleVision.Settings))]
  public class BlurryFeatureSettingsDrawer : Drawer
  {
    private DoubleVision.Settings settings;

    protected override void InspectorGUI()
    {
      if (settings == null)
        settings = GetSettings<DoubleVision.Settings>();

      /////////////////////////////////////////////////
      // Common.
      /////////////////////////////////////////////////
      settings.intensity = Slider("Intensity", "Controls the intensity of the effect [0, 1]. Default 1.", settings.intensity, 0.0f, 1.0f, 1.0f);

      /////////////////////////////////////////////////
      // Double Vision.
      /////////////////////////////////////////////////
      Separator();

      settings.strength.z = Slider("Strength", "Strength and direction of effect [0, 1]. Default 0.", settings.strength.z, 0.0f, 1.0f, DoubleVision.Settings.DefaultStrength.z);
      IndentLevel++;
      settings.strength.x = Slider("X", string.Empty, settings.strength.x, 0.0f, 10.0f, DoubleVision.Settings.DefaultStrength.x);
      settings.strength.y = Slider("Y", string.Empty, settings.strength.y, 0.0f, 10.0f, DoubleVision.Settings.DefaultStrength.y);
      IndentLevel--;

      settings.speed.z = Slider("Speed", "Oscillation speed on each axis [0, 1]. Default 0.", settings.speed.z, 0.0f, 1.0f, DoubleVision.Settings.DefaultSpeed.z);
      IndentLevel++;
      settings.speed.x = Slider("X", string.Empty, settings.speed.x, -10.0f, 10.0f, DoubleVision.Settings.DefaultSpeed.x);
      settings.speed.y = Slider("Y", string.Empty, settings.speed.y, -10.0f, 10.0f, DoubleVision.Settings.DefaultSpeed.y);
      IndentLevel--;

      settings.colorOffset.w = Slider("Color offset", "Color channels offset [0, 1]. Default 0.", settings.colorOffset.w, 0.0f, 1.0f, 0.0f);
      IndentLevel++;
      settings.colorOffset.x = Slider("Red", string.Empty, settings.colorOffset.x, -10.0f, 10.0f, 0.0f);
      settings.colorOffset.y = Slider("Green", string.Empty, settings.colorOffset.y, -10.0f, 10.0f, 0.0f);
      settings.colorOffset.z = Slider("Blue", string.Empty, settings.colorOffset.z, -10.0f, 10.0f, 0.0f);
      IndentLevel--;

      settings.blendStrength = Slider("Blend strength", "Result of image blending [0, 1]. Default 0.5.", settings.blendStrength, 0.0f, 1.0f, 0.5f);
      IndentLevel++;
      settings.colorBlend = (ColorBlends)EnumPopup("Mode", "How to mix the original image and the effect image. Default Solid.", settings.colorBlend, ColorBlends.Solid);
      IndentLevel--;

      /////////////////////////////////////////////////
      // Color.
      /////////////////////////////////////////////////
      Separator();

      if (Foldout("Color") == true)
      {
        IndentLevel++;

        settings.brightness = Slider("Brightness", "Brightness [-1.0, 1.0]. Default 0.", settings.brightness, -1.0f, 1.0f, 0.0f);
        settings.contrast = Slider("Contrast", "Contrast [0.0, 10.0]. Default 1.", settings.contrast, 0.0f, 10.0f, 1.0f);
        settings.gamma = Slider("Gamma", "Gamma [0.1, 10.0]. Default 1.", settings.gamma, 0.01f, 10.0f, 1.0f);
        settings.hue = Slider("Hue", "The color wheel [0.0, 1.0]. Default 0.", settings.hue, 0.0f, 1.0f, 0.0f);
        settings.saturation = Slider("Saturation", "Intensity of a colors [0.0, 2.0]. Default 1.", settings.saturation, 0.0f, 2.0f, 1.0f);

        IndentLevel--;
      }

      /////////////////////////////////////////////////
      // Advanced.
      /////////////////////////////////////////////////
      Separator();

      if (Foldout("Advanced") == true)
      {
        IndentLevel++;

        settings.affectSceneView = Toggle("Affect the Scene View?", "Does it affect the Scene View?", settings.affectSceneView);
        settings.filterMode = (FilterMode)EnumPopup("Filter mode", "Filter mode. Default Bilinear.", settings.filterMode, FilterMode.Bilinear);
        settings.whenToInsert = (UnityEngine.Rendering.Universal.RenderPassEvent)EnumPopup("RenderPass event",
          "Render pass injection. Default BeforeRenderingPostProcessing.",
          settings.whenToInsert,
          UnityEngine.Rendering.Universal.RenderPassEvent.BeforeRenderingPostProcessing);
        settings.enableProfiling = Toggle("Enable profiling", "Enable render pass profiling", settings.enableProfiling);

        IndentLevel--;
      }

      /////////////////////////////////////////////////
      // Misc.
      /////////////////////////////////////////////////
      Separator();

      BeginHorizontal();
      {
        if (MiniButton("documentation", "Online documentation") == true)
          Application.OpenURL(Constants.Support.Documentation);

        if (MiniButton("support", "Do you have any problem or suggestion?") == true)
          SupportWindow.ShowWindow();

        if (EditorPrefs.GetBool($"{Constants.Asset.AssemblyName}.Review") == false)
        {
          Separator();

          if (MiniButton("write a review <color=#800000>❤️</color>", "Write a review, thanks!") == true)
          {
            Application.OpenURL(Constants.Support.Store);

            EditorPrefs.SetBool($"{Constants.Asset.AssemblyName}.Review", true);
          }
        }

        FlexibleSpace();

        if (Button("Reset") == true)
          settings.ResetDefaultValues();
      }
      EndHorizontal();
    }
  }
}
