using UnityEngine;
using UnityEngine.Rendering;
using FronkonGames.SpiceUp.Scanner;

/// <summary> Spice Up: Scanner demo. </summary>
/// <remarks>
/// This code is designed for a simple demo, not for production environments.
/// </remarks>
public class ScannerDemo : MonoBehaviour
{
  [Space, SerializeField]
  private RenderPipelineAsset overrideRenderPipelineAsset;

  private RenderPipelineAsset defaultRenderPipelineAsset;
  private Scanner.Settings settings;

  private GUIStyle styleTitle;
  private GUIStyle styleLabel;
  private GUIStyle styleButton;

  private void SetPreset(int preset)
  {
    switch (preset)
    {
      case 0:
        settings.linesCount = 400;
        settings.linesTint = new Color(0f, 1f, 0f);
        settings.linesBlend = ColorBlends.Multiply;
        settings.backgroundTint = new Color(0f, 0f, 0f);
        settings.backgroundBlend = ColorBlends.SoftLight;
        settings.scanlineStrength = 0.5f;
        settings.scanlineTint = new Color(0f, 1f, 0f);
        settings.scanlineBlend = ColorBlends.Overlay;
        settings.scanlineWidth = 1f;
        settings.scanlineSpeed = 0.5f;
        settings.noiseBandStrength = 0.2f;
        settings.noiseBandTint = new Color(0f, 1f, 0f);
        settings.noiseBandBlend = ColorBlends.Additive;
        settings.noiseBandWidth = 0.2f;
        settings.noiseBandSpeed = 0.2f;
        settings.barrelStrength = 0.5f;
        settings.barrelZoom = 0f;
        settings.barrelTint = new Color(0.05f, 0.05f, 0.05f);
        settings.interlace = 0.1f;
        settings.frameNoise = 0f;
        settings.signalNoise = 0.1f;
        settings.badSignalStrength = 0f;
        settings.vignetteStrength = 0.6f;
        settings.vignetteBlink = 0.3f;
        settings.brightness = 0f;
        settings.contrast = 1f;
        settings.gamma = 1f;
        settings.hue = 0f;
        settings.saturation = 1f;
        break;
      case 1:
        settings.linesCount = 664;
        settings.linesTint = new Color(0f, 1f, 0f);
        settings.linesBlend = ColorBlends.Multiply;
        settings.backgroundTint = new Color(0f, 0f, 0f);
        settings.backgroundBlend = ColorBlends.SoftLight;
        settings.scanlineStrength = 0f;
        settings.scanlineTint = new Color(0f, 1f, 0f);
        settings.scanlineBlend = ColorBlends.Overlay;
        settings.scanlineWidth = 1f;
        settings.scanlineSpeed = 0.5f;
        settings.noiseBandStrength = 0f;
        settings.noiseBandTint = new Color(0f, 1f, 0f);
        settings.noiseBandBlend = ColorBlends.Additive;
        settings.noiseBandWidth = 0.2f;
        settings.noiseBandSpeed = 0.2f;
        settings.barrelStrength = 1.37f;
        settings.barrelZoom = 0.072f;
        settings.barrelTint = new Color(0.1415094f, 0.1415094f, 0.1415094f);
        settings.interlace = 0.1f;
        settings.frameNoise = 0f;
        settings.signalNoise = 0f;
        settings.badSignalStrength = 0.181f;
        settings.vignetteStrength = 0.6f;
        settings.vignetteBlink = 0.3f;
        settings.brightness = 0f;
        settings.contrast = 1f;
        settings.gamma = 1f;
        settings.hue = 0f;
        settings.saturation = 0f;
        break;
      case 2:
        settings.linesCount = 934;
        settings.linesTint = new Color(0f, 1f, 0.8679183f);
        settings.linesBlend = ColorBlends.Multiply;
        settings.backgroundTint = new Color(0f, 0f, 0f);
        settings.backgroundBlend = ColorBlends.SoftLight;
        settings.scanlineStrength = 1f;
        settings.scanlineTint = new Color(0f, 1f, 0.8666667f);
        settings.scanlineBlend = ColorBlends.Hue;
        settings.scanlineWidth = 5f;
        settings.scanlineSpeed = -1.01f;
        settings.noiseBandStrength = 0f;
        settings.noiseBandTint = new Color(0f, 1f, 0.8666667f);
        settings.noiseBandBlend = ColorBlends.Additive;
        settings.noiseBandWidth = 0.2f;
        settings.noiseBandSpeed = 0.2f;
        settings.barrelStrength = 0f;
        settings.barrelZoom = 0.071f;
        settings.barrelTint = new Color(0f, 1f, 0.8666667f);
        settings.interlace = 0.144f;
        settings.frameNoise = 0f;
        settings.signalNoise = 0f;
        settings.badSignalStrength = 0f;
        settings.vignetteStrength = 0.256f;
        settings.vignetteBlink = 0.3f;
        settings.brightness = 0f;
        settings.contrast = 1f;
        settings.gamma = 1f;
        settings.hue = 0f;
        settings.saturation = 1f;
        break;
      case 3:
        settings.linesCount = 0;
        settings.linesTint = new Color(1f, 0f, 0.07744408f);
        settings.linesBlend = ColorBlends.Additive;
        settings.backgroundTint = new Color(0f, 0f, 0f);
        settings.backgroundBlend = ColorBlends.Additive;
        settings.scanlineStrength = 0.5f;
        settings.scanlineTint = new Color(1f, 0f, 0.07843138f);
        settings.scanlineBlend = ColorBlends.Additive;
        settings.scanlineWidth = 5f;
        settings.scanlineSpeed = 0.5f;
        settings.noiseBandStrength = 0.2f;
        settings.noiseBandTint = new Color(1f, 0f, 0.07843138f);
        settings.noiseBandBlend = ColorBlends.Additive;
        settings.noiseBandWidth = 5f;
        settings.noiseBandSpeed = 0.2f;
        settings.barrelStrength = 0.83f;
        settings.barrelZoom = 0.033f;
        settings.barrelTint = new Color(0.0471698f, 0f, 0f);
        settings.interlace = 0f;
        settings.frameNoise = 0.279f;
        settings.signalNoise = 0.204f;
        settings.badSignalStrength = 0.022f;
        settings.vignetteStrength = 1f;
        settings.vignetteBlink = 0.3f;
        settings.brightness = 0f;
        settings.contrast = 1f;
        settings.gamma = 1.05f;
        settings.hue = 1f;
        settings.saturation = 2f;
        break;
      case 4:
        settings.linesCount = 265;
        settings.linesTint = new Color(0f, 0.03610992f, 1f);
        settings.linesBlend = ColorBlends.Additive;
        settings.backgroundTint = new Color(1f, 0f, 0f);
        settings.backgroundBlend = ColorBlends.Divide;
        settings.scanlineStrength = 0.5f;
        settings.scanlineTint = new Color(0f, 0.9827287f, 1f);
        settings.scanlineBlend = ColorBlends.Color;
        settings.scanlineWidth = 1f;
        settings.scanlineSpeed = 0.5f;
        settings.noiseBandStrength = 1f;
        settings.noiseBandTint = new Color(1f, 0.940421f, 0f);
        settings.noiseBandBlend = ColorBlends.HardMix;
        settings.noiseBandWidth = 0.12f;
        settings.noiseBandSpeed = -0.18f;
        settings.barrelStrength = 0.5f;
        settings.barrelZoom = 0.038f;
        settings.barrelTint = new Color(0.05f, 0.05f, 0.05f);
        settings.interlace = 0f;
        settings.frameNoise = 0f;
        settings.signalNoise = 0f;
        settings.badSignalStrength = 0f;
        settings.vignetteStrength = 1f;
        settings.vignetteBlink = 0.443f;
        settings.brightness = 0f;
        settings.contrast = 1f;
        settings.gamma = 1f;
        settings.hue = 0f;
        settings.saturation = 2f;
        break;
      case 5:
        settings.linesCount = 328;
        settings.linesTint = new Color(1f, 0.5783419f, 0f);
        settings.linesBlend = ColorBlends.Solid;
        settings.backgroundTint = new Color(1f, 0.5764706f, 0f);
        settings.backgroundBlend = ColorBlends.PinLight;
        settings.scanlineStrength = 1f;
        settings.scanlineTint = new Color(1f, 0.5764706f, 0f);
        settings.scanlineBlend = ColorBlends.Screen;
        settings.scanlineWidth = 5f;
        settings.scanlineSpeed = 2.81f;
        settings.noiseBandStrength = 0.418f;
        settings.noiseBandTint = new Color(1f, 0.5764706f, 0f);
        settings.noiseBandBlend = ColorBlends.Lighten;
        settings.noiseBandWidth = 5f;
        settings.noiseBandSpeed = 0.2f;
        settings.barrelStrength = 2.57f;
        settings.barrelZoom = 0.464f;
        settings.barrelTint = new Color(0.08490568f, 0.0488449f, 0f);
        settings.interlace = 0.06f;
        settings.frameNoise = 0f;
        settings.signalNoise = 0.185f;
        settings.badSignalStrength = 0.115f;
        settings.vignetteStrength = 1f;
        settings.vignetteBlink = 0.355f;
        settings.brightness = 0f;
        settings.contrast = 1f;
        settings.gamma = 0.89f;
        settings.hue = 0f;
        settings.saturation = 1f;
        break;
    }

    settings.strength = 1.0f;
  }

  private void Start()
  {
    defaultRenderPipelineAsset = GraphicsSettings.currentRenderPipeline;

    GraphicsSettings.defaultRenderPipeline = overrideRenderPipelineAsset;
    QualitySettings.renderPipeline = overrideRenderPipelineAsset;

    if (Scanner.IsInRenderFeatures() == false)
      Scanner.AddRenderFeature();

    settings = Scanner.GetSettings();
    settings.ResetDefaultValues();
  }

  private void Update()
  {
    if (Input.GetKeyDown(KeyCode.Alpha1) == true)
      SetPreset(0);
    else if (Input.GetKeyDown(KeyCode.Alpha2) == true)
      SetPreset(1);
    else if (Input.GetKeyDown(KeyCode.Alpha3) == true)
      SetPreset(2);
    else if (Input.GetKeyDown(KeyCode.Alpha4) == true)
      SetPreset(3);
    else if (Input.GetKeyDown(KeyCode.Alpha4) == true)
      SetPreset(4);
    else if (Input.GetKeyDown(KeyCode.Alpha5) == true)
      SetPreset(5);
    else if (Input.GetKeyDown(KeyCode.Alpha0) == true)
      settings.ResetDefaultValues();
  }

  private void OnGUI()
  {
    styleTitle = new GUIStyle(GUI.skin.label)
    {
      alignment = TextAnchor.LowerCenter,
      fontSize = 32,
      fontStyle = FontStyle.Bold
    };

    styleLabel = new GUIStyle(GUI.skin.label)
    {
      alignment = TextAnchor.UpperLeft,
      fontSize = 24
    };

    styleButton = new GUIStyle(GUI.skin.button)
    {
      fontSize = 24
    };

    GUILayout.BeginHorizontal();
    {
      GUILayout.BeginVertical("box", GUILayout.Width(300.0f), GUILayout.Height(Screen.height));
      {
        const float space = 10.0f;

        GUILayout.Space(space);

        GUILayout.Label("SCANNER DEMO", styleTitle);

        GUILayout.Space(space * 2.0f);

        settings.strength = settings.intensity = Slider("Intensity", settings.intensity);
        settings.linesCount = Slider("Lines", settings.linesCount, 0, 1000);

        GUILayout.Space(space);

        settings.scanlineStrength = Slider("Scanline", settings.scanlineStrength);
        settings.scanlineWidth = Slider("    Width", settings.scanlineWidth, 0.0f, 5.0f);
        settings.scanlineSpeed = Slider("    Speed", settings.scanlineSpeed, -5.0f, 5.0f);

        GUILayout.Space(space);

        settings.noiseBandStrength = Slider("Noise Band", settings.noiseBandStrength);
        settings.noiseBandWidth = Slider("    Width", settings.noiseBandWidth, 0.0f, 5.0f);
        settings.noiseBandSpeed = Slider("    Speed", settings.noiseBandSpeed, -5.0f, 5.0f);

        GUILayout.Space(space);

        settings.frameNoise = Slider("Frame Noise", settings.frameNoise);
        settings.signalNoise = Slider("Signal Noise", settings.signalNoise);
        settings.interlace = Slider("Interlace Noise", settings.interlace);
        settings.badSignalStrength = Slider("Bad Signal", settings.badSignalStrength);

        GUILayout.Space(space);

        settings.barrelStrength = Slider("Barrel", settings.barrelStrength, 0.0f, 10.0f);
        settings.barrelZoom = Slider("    Zoom", settings.barrelZoom);

        GUILayout.Space(space);

        settings.vignetteStrength = Slider("Vignette", settings.vignetteStrength);
        settings.vignetteBlink = Slider("    Blink", settings.vignetteBlink);

        GUILayout.FlexibleSpace();

        GUILayout.BeginHorizontal();
        {
          GUILayout.Label("Presets", styleLabel);

          if (GUILayout.Button("1", styleButton) == true)
            SetPreset(0);

          if (GUILayout.Button("2", styleButton) == true)
            SetPreset(1);

          if (GUILayout.Button("3", styleButton) == true)
            SetPreset(2);

          if (GUILayout.Button("4", styleButton) == true)
            SetPreset(3);

          if (GUILayout.Button("5", styleButton) == true)
            SetPreset(4);

          if (GUILayout.Button("6", styleButton) == true)
            SetPreset(5);
        }
        GUILayout.EndHorizontal();

        if (GUILayout.Button("RESET", styleButton) == true)
          settings.ResetDefaultValues();

        GUILayout.Space(space);
      }
      GUILayout.EndVertical();

      GUILayout.FlexibleSpace();
    }
    GUILayout.EndHorizontal();
  }

  private void OnDestroy()
  {
    settings.ResetDefaultValues();

    GraphicsSettings.defaultRenderPipeline = defaultRenderPipelineAsset;
    QualitySettings.renderPipeline = defaultRenderPipelineAsset;
  }

  private float Slider(string label, float value, float min = 0.0f, float max = 1.0f)
  {
    GUILayout.BeginHorizontal();
    {
      GUILayout.Label(label, styleLabel);

      value = GUILayout.HorizontalSlider(value, min, max);
    }
    GUILayout.EndHorizontal();

    return value;
  }

  private int Slider(string label, int value, int min, int max)
  {
    GUILayout.BeginHorizontal();
    {
      GUILayout.Label(label, styleLabel);

      value = (int)GUILayout.HorizontalSlider(value, min, max);
    }
    GUILayout.EndHorizontal();

    return value;
  }
}
