using System;
using UnityEngine;
using UnityEngine.Rendering;
using FronkonGames.SpiceUp.DoubleVision;

/// <summary> Spice Up: Double Vision demo. </summary>
/// <remarks>
/// This code is designed for a simple demo, not for production environments.
/// </remarks>
public class DoubleVisionDemo : MonoBehaviour
{
  [Space, SerializeField]
  private RenderPipelineAsset overrideRenderPipelineAsset;

  private RenderPipelineAsset defaultRenderPipelineAsset;
  private DoubleVision.Settings settings;

  private GUIStyle styleTitle;
  private GUIStyle styleLabel;
  private GUIStyle styleButton;

  private void Start()
  {
    defaultRenderPipelineAsset = GraphicsSettings.currentRenderPipeline;

    GraphicsSettings.defaultRenderPipeline = overrideRenderPipelineAsset;
    QualitySettings.renderPipeline = overrideRenderPipelineAsset;

    if (DoubleVision.IsInRenderFeatures() == false)
      DoubleVision.AddRenderFeature();

    settings = DoubleVision.GetSettings();
    ResetDemo();
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

    GUILayout.BeginHorizontal("box", GUILayout.Width(450.0f), GUILayout.Height(Screen.height));
    {
      const float space = 20.0f;

      GUILayout.BeginVertical(GUILayout.Width(space));
      GUILayout.FlexibleSpace();
      GUILayout.EndVertical();

      GUILayout.BeginVertical();
      {
        GUILayout.Space(space);

        GUILayout.Label("DOUBLE VISION DEMO", styleTitle);

        GUILayout.Space(space * 2.0f);

        settings.intensity = Slider("Intensity", settings.intensity);

        GUILayout.Space(space);

        settings.strength.z = Slider("Strength", settings.strength.z, 0.0f, 10.0f);
        settings.strength.x = Slider("      X", settings.strength.x, -20.0f, 20.0f);
        settings.strength.y = Slider("      Y", settings.strength.y, -20.0f, 20.0f);

        settings.speed.z = Slider("Speed", settings.speed.z, 0.0f, 10.0f);
        settings.speed.x = Slider("      X", settings.speed.x, -20.0f, 20.0f);
        settings.speed.y = Slider("      Y", settings.speed.y, -20.0f, 20.0f);

        settings.colorOffset = Vector3("Color Offset", settings.colorOffset, "R", "G", "B", -100.0f, 100.0f);

        settings.blendStrength = Slider("Blend", settings.blendStrength);

        settings.colorBlend = Enum("Color Blend", settings.colorBlend);

        GUILayout.FlexibleSpace();

        if (GUILayout.Button("RESET", styleButton) == true)
          ResetDemo();

        GUILayout.Space(space);
      }
      GUILayout.EndVertical();

      GUILayout.BeginVertical(GUILayout.Width(space));
      GUILayout.FlexibleSpace();
      GUILayout.EndVertical();
    }
    GUILayout.EndHorizontal();
  }

  private void ResetDemo()
  {
    settings.ResetDefaultValues();

    settings.strength.z = 2.0f;
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

  private Vector3 Vector3(string label, Vector3 value, string x = "X", string y = "Y", string z = "Z", float min = 0.0f, float max = 1.0f)
  {
    GUILayout.Label(label, styleLabel);

    value.x = Slider($"   {x}", value.x, min, max);
    value.y = Slider($"   {y}", value.y, min, max);
    value.z = Slider($"   {z}", value.z, min, max);

    return value;
  }

  private T Enum<T>(string label, T value) where T : Enum
  {
    string[] names = System.Enum.GetNames(typeof(T));
    Array values = System.Enum.GetValues(typeof(T));
    int index = Array.IndexOf(values, value);

    GUILayout.BeginHorizontal();
    {
      GUILayout.Label(label, styleLabel);

      if (GUILayout.Button("<", styleButton) == true)
        index = index > 0 ? index - 1 : values.Length - 1;

      GUILayout.Label(names[index], styleLabel);

      if (GUILayout.Button(">", styleButton) == true)
        index = index < values.Length - 1 ? index + 1 : 0;
    }
    GUILayout.EndHorizontal();

    return (T)(object)index;
  }
}
