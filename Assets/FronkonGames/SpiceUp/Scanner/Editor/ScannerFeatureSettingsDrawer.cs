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
using static FronkonGames.SpiceUp.Scanner.Inspector;

namespace FronkonGames.SpiceUp.Scanner.Editor
{
  /// <summary> Spice Up Scanner inspector. </summary>
  [CustomPropertyDrawer(typeof(Scanner.Settings))]
  public class ScannerFeatureSettingsDrawer : Drawer
  {
    private Scanner.Settings settings;

    protected override void InspectorGUI()
    {
      settings ??= GetSettings<Scanner.Settings>();

      /////////////////////////////////////////////////
      // Common.
      /////////////////////////////////////////////////
      settings.intensity = Slider("Intensity", "Controls the intensity of the effect [0, 1]. Default 1.", settings.intensity, 0.0f, 1.0f, 1.0f);

      /////////////////////////////////////////////////
      // Scanner.
      /////////////////////////////////////////////////
      Separator();

      settings.strength = Slider("Strength", "Lines strength [0, 1]. Default 0.", settings.strength, 0.0f, 1.0f, 0.0f);

      settings.linesCount = Slider("Lines", "Lines count [0, 1000]. Default 500.", settings.linesCount, 0, 1000, 500);
      IndentLevel++;
      settings.linesTint = ColorField("Tint", "Lines tint.", settings.linesTint, Color.green);
      settings.linesBlend = (ColorBlends)EnumPopup("Blend", "Lines blend.", settings.linesBlend, ColorBlends.Multiply);
      IndentLevel--;

      settings.backgroundTint = ColorField("Background", "Background tint.", settings.backgroundTint, Color.black);
      IndentLevel++;
      settings.backgroundBlend = (ColorBlends)EnumPopup("Blend", "Background blend.", settings.backgroundBlend, ColorBlends.SoftLight);
      IndentLevel--;

      settings.scanlineStrength = Slider("Scanline", "Scan line strength [0, 1]. Default 0.5.", settings.scanlineStrength, 0.0f, 1.0f, 0.5f);
      IndentLevel++;
      settings.scanlineTint = ColorField("Tint", "Scanline tint.", settings.scanlineTint, Color.green);
      settings.scanlineBlend = (ColorBlends)EnumPopup("Blend", "Scanline blend.", settings.scanlineBlend, ColorBlends.Additive);
      settings.scanlineWidth = Slider("Width", "Scan line wigth [0, 5]. Default 0.2.", settings.scanlineWidth, 0.0f, 5.0f, 0.2f);
      settings.scanlineSpeed = Slider("Speed", "Scan line speed [-5, 5]. Default 0.5.", settings.scanlineSpeed, -5.0f, 5.0f, 0.5f);
      IndentLevel--;

      settings.noiseBandStrength = Slider("Band noise", "Noise band strength [0, 1]. Default 0.2.", settings.noiseBandStrength, 0.0f, 1.0f, 0.2f);
      IndentLevel++;
      settings.noiseBandTint = ColorField("Tint", "Noise tint.", settings.noiseBandTint, Color.green);
      settings.noiseBandBlend = (ColorBlends)EnumPopup("Blend", "Noise blend.", settings.noiseBandBlend, ColorBlends.Additive);
      settings.noiseBandWidth = Slider("Width", "Noise band width [0, 5]. Default 0.2.", settings.noiseBandWidth, 0.0f, 5.0f, 0.2f);
      settings.noiseBandSpeed = Slider("Speed", "Noise band speed [-5, 5]. Default 0.2.", settings.noiseBandSpeed, -5.0f, 5.0f, 0.2f);
      IndentLevel--;
      settings.frameNoise = Slider("Frame noise", "Image frame noise [0, 1]. Default 0.05.", settings.frameNoise, 0.0f, 1.0f, 0.05f);
      settings.signalNoise = Slider("Signal noise", "Signal noise [0, 1]. Default 0.1.", settings.signalNoise, 0.0f, 1.0f, 0.1f);
      settings.interlace = Slider("Interlace noise", "Interlace noise [0, 1]. Default 0.1.", settings.interlace, 0.0f, 1.0f, 0.1f);
      settings.badSignalStrength = Slider("Bad signal", "Bad signal strength [0, 1]. Default 0.", settings.badSignalStrength, 0.0f, 1.0f, 0.0f);

      settings.barrelStrength = Slider("Barrel", "Barrel strength [0, 1]. Default 0.5.", settings.barrelStrength, 0.0f, 10.0f, 0.5f);
      IndentLevel++;
      settings.barrelTint = ColorField("Tint", "Exterior color.", settings.barrelTint, Scanner.Settings.DefaultBarrelColor);
      settings.barrelZoom = Slider("Zoom", "Barrel zoom [0, 1]. Default 1.", settings.barrelZoom, 0.0f, 1.0f, 0.0f);
      IndentLevel--;

      settings.vignetteStrength = Slider("Vignette", "Vignette strength [0, 1]. Default 0.6.", settings.vignetteStrength, 0.0f, 1.0f, 0.6f);
      IndentLevel++;
      settings.vignetteBlink = Slider("Blink", "Vignette blink effect [0, 1]. Default 0.3.", settings.vignetteBlink, 0.0f, 1.0f, 0.3f);
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

    protected override void OnCopy()
    {
      GUIUtility.systemCopyBuffer = $"settings.intensity = {ToString(settings.intensity)};\n" +
        $"settings.strength = {ToString(settings.strength)};\n" +
        $"settings.linesCount = {settings.linesCount};\n" +
        $"settings.linesTint = {ToString(settings.linesTint)};\n" +
        $"settings.linesBlend = ColorBlends.{settings.linesBlend};\n" +
        $"settings.backgroundTint = {ToString(settings.backgroundTint)};\n" +
        $"settings.backgroundBlend = ColorBlends.{settings.backgroundBlend};\n" +
        $"settings.scanlineStrength = {ToString(settings.scanlineStrength)};\n" +
        $"settings.scanlineTint = {ToString(settings.scanlineTint)};\n" +
        $"settings.scanlineBlend = ColorBlends.{settings.scanlineBlend};\n" +
        $"settings.scanlineWidth = {ToString(settings.scanlineWidth)};\n" +
        $"settings.scanlineSpeed = {ToString(settings.scanlineSpeed)};\n" +
        $"settings.noiseBandStrength = {ToString(settings.noiseBandStrength)};\n" +
        $"settings.noiseBandTint = {ToString(settings.noiseBandTint)};\n" +
        $"settings.noiseBandBlend = ColorBlends.{settings.noiseBandBlend};\n" +
        $"settings.noiseBandWidth = {ToString(settings.noiseBandWidth)};\n" +
        $"settings.noiseBandSpeed = {ToString(settings.noiseBandSpeed)};\n" +
        $"settings.barrelStrength = {ToString(settings.barrelStrength)};\n" +
        $"settings.barrelZoom = {ToString(settings.barrelZoom)};\n" +
        $"settings.barrelTint = {ToString(settings.barrelTint)};\n" +
        $"settings.interlace = {ToString(settings.interlace)};\n" +
        $"settings.frameNoise = {ToString(settings.frameNoise)};\n" +
        $"settings.signalNoise = {ToString(settings.signalNoise)};\n" +
        $"settings.badSignalStrength = {ToString(settings.badSignalStrength)};\n" +
        $"settings.vignetteStrength = {ToString(settings.vignetteStrength)};\n" +
        $"settings.vignetteBlink = {ToString(settings.vignetteBlink)};\n" +
        $"settings.brightness = {ToString(settings.brightness)};\n" +
        $"settings.contrast = {ToString(settings.contrast)};\n" +
        $"settings.gamma = {ToString(settings.gamma)};\n" +
        $"settings.hue = {ToString(settings.hue)};\n" +
        $"settings.saturation = {ToString(settings.saturation)};";
    }
  }
}
