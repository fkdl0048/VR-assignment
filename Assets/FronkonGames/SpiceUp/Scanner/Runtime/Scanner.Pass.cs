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
using Unity.Profiling;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace FronkonGames.SpiceUp.Scanner
{
  ///------------------------------------------------------------------------------------------------------------------
  /// <summary> Render Pass. </summary>
  /// <remarks> Only available for Universal Render Pipeline. </remarks>
  ///------------------------------------------------------------------------------------------------------------------
  public sealed partial class Scanner
  {
    private sealed class RenderPass : ScriptableRenderPass
    {
      private readonly Settings settings;

      private RenderTargetIdentifier colorBuffer;
      private RenderTextureDescriptor renderTextureDescriptor;

#if UNITY_2022_1_OR_NEWER
      RTHandle renderTextureHandle0;

      private readonly ProfilingSampler profilingSamples = new(Constants.Asset.AssemblyName);
      private ProfilingScope profilingScope;
#else
      private int renderTextureHandle0;
#endif
      private readonly Material material;

      private static readonly ProfilerMarker ProfilerMarker = new($"{Constants.Asset.AssemblyName}.Pass.Execute");

      private const string CommandBufferName = Constants.Asset.AssemblyName;

      private static class ShaderIDs
      {
        public static readonly int Intensity = Shader.PropertyToID("_Intensity");

        public static readonly int Strength = Shader.PropertyToID("_Strength");
        public static readonly int Count = Shader.PropertyToID("_Count");
        public static readonly int TintLines = Shader.PropertyToID("_TintLines");
        public static readonly int BlendLines = Shader.PropertyToID("_BlendLines");
        public static readonly int TintBackground = Shader.PropertyToID("_TintBackground");
        public static readonly int BlendBackground = Shader.PropertyToID("_BlendBackground");
        public static readonly int ScanLineStrength = Shader.PropertyToID("_ScanlineStrength");
        public static readonly int ScanLineColor = Shader.PropertyToID("_ScanlineTint");
        public static readonly int ScanLineBlend = Shader.PropertyToID("_ScanlineBlend");
        public static readonly int ScanLineWidth = Shader.PropertyToID("_ScanlineWidth");
        public static readonly int ScanLineSpeed = Shader.PropertyToID("_ScanlineSpeed");
        public static readonly int NoiseBandStrength = Shader.PropertyToID("_NoiseBandStrength");
        public static readonly int NoiseBandColor = Shader.PropertyToID("_NoiseBandTint");
        public static readonly int NoiseBandBlend = Shader.PropertyToID("_NoiseBandBlend");
        public static readonly int NoiseBandWidth = Shader.PropertyToID("_NoiseBandWidth");
        public static readonly int NoiseBandSpeed = Shader.PropertyToID("_NoiseBandSpeed");
        public static readonly int BarrelStrength = Shader.PropertyToID("_BarrelStrength");
        public static readonly int BarrelZoom = Shader.PropertyToID("_BarrelZoom");
        public static readonly int BarrelTint = Shader.PropertyToID("_BarrelTint");
        public static readonly int Interlace = Shader.PropertyToID("_Interlace");
        public static readonly int FrameNoise = Shader.PropertyToID("_FrameNoise");
        public static readonly int SignalNoise = Shader.PropertyToID("_SignalNoise");
        public static readonly int BadSignalStrength = Shader.PropertyToID("_BadSignalStrength");
        public static readonly int VignetteStrength = Shader.PropertyToID("_VignetteStrength");
        public static readonly int VignetteBlink = Shader.PropertyToID("_VignetteBlink");

        public static readonly int Brightness = Shader.PropertyToID("_Brightness");
        public static readonly int Contrast = Shader.PropertyToID("_Contrast");
        public static readonly int Gamma = Shader.PropertyToID("_Gamma");
        public static readonly int Hue = Shader.PropertyToID("_Hue");
        public static readonly int Saturation = Shader.PropertyToID("_Saturation");
      }

      /// <summary> Render pass constructor. </summary>
      public RenderPass(Settings settings)
      {
        this.settings = settings;

        string shaderPath = $"Shaders/{Constants.Asset.ShaderName}_URP";
        Shader shader = Resources.Load<Shader>(shaderPath);
        if (shader != null)
        {
          if (shader.isSupported == true)
            material = CoreUtils.CreateEngineMaterial(shader);
          else
            Log.Warning($"'{shaderPath}.shader' not supported");
        }
      }

      /// <inheritdoc/>
      public override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData)
      {
        renderTextureDescriptor = renderingData.cameraData.cameraTargetDescriptor;
        renderTextureDescriptor.depthBufferBits = 0;

#if UNITY_2022_1_OR_NEWER
        colorBuffer = renderingData.cameraData.renderer.cameraColorTargetHandle;

        RenderingUtils.ReAllocateIfNeeded(ref renderTextureHandle0, renderTextureDescriptor, settings.filterMode, TextureWrapMode.Clamp, false, 1, 0, $"_RTHandle0_{Constants.Asset.Name}");
#else
        colorBuffer = renderingData.cameraData.renderer.cameraColorTarget;

        renderTextureHandle0 = Shader.PropertyToID($"_RTHandle0_{Constants.Asset.Name}");
        cmd.GetTemporaryRT(renderTextureHandle0, renderTextureDescriptor.width, renderTextureDescriptor.height, renderTextureDescriptor.depthBufferBits, settings.filterMode, RenderTextureFormat.ARGB32);
#endif
      }

      /// <inheritdoc/>
      public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
      {
        if (material == null ||
            renderingData.postProcessingEnabled == false ||
            settings.intensity == 0.0f ||
            settings.affectSceneView == false && renderingData.cameraData.isSceneViewCamera == true)
          return;

        CommandBuffer cmd = CommandBufferPool.Get(CommandBufferName);

        if (settings.enableProfiling == true)
#if UNITY_2022_1_OR_NEWER
          profilingScope = new ProfilingScope(cmd, profilingSamples);
#else
          ProfilerMarker.Begin();
#endif

        material.shaderKeywords = null;
        material.SetFloat(ShaderIDs.Intensity, settings.intensity);

        material.SetFloat(ShaderIDs.Strength, settings.strength);

        material.SetFloat(ShaderIDs.Count, settings.linesCount);
        material.SetColor(ShaderIDs.TintLines, settings.linesTint);
        material.SetInt(ShaderIDs.BlendLines, (int)settings.linesBlend);
        material.SetColor(ShaderIDs.TintBackground, settings.backgroundTint);
        material.SetInt(ShaderIDs.BlendBackground, (int)settings.backgroundBlend);
        material.SetFloat(ShaderIDs.ScanLineStrength, settings.scanlineStrength);
        material.SetFloat(ShaderIDs.ScanLineWidth, settings.scanlineWidth * 0.01f);
        material.SetFloat(ShaderIDs.ScanLineSpeed, settings.scanlineSpeed);
        material.SetColor(ShaderIDs.ScanLineColor, settings.scanlineTint);
        material.SetInt(ShaderIDs.ScanLineBlend, (int)settings.scanlineBlend);
        material.SetFloat(ShaderIDs.NoiseBandStrength, settings.noiseBandStrength);
        material.SetColor(ShaderIDs.NoiseBandColor, settings.noiseBandTint);
        material.SetInt(ShaderIDs.NoiseBandBlend, (int)settings.noiseBandBlend);
        material.SetFloat(ShaderIDs.NoiseBandWidth, settings.noiseBandWidth);
        material.SetFloat(ShaderIDs.NoiseBandSpeed, settings.noiseBandSpeed);
        material.SetFloat(ShaderIDs.FrameNoise, settings.frameNoise);
        material.SetFloat(ShaderIDs.SignalNoise, settings.signalNoise);
        material.SetFloat(ShaderIDs.BarrelStrength, settings.barrelStrength);
        material.SetFloat(ShaderIDs.BarrelZoom, settings.barrelZoom);
        material.SetColor(ShaderIDs.BarrelTint, settings.barrelTint);
        material.SetFloat(ShaderIDs.Interlace, settings.interlace);
        material.SetFloat(ShaderIDs.BadSignalStrength, settings.badSignalStrength);
        material.SetFloat(ShaderIDs.VignetteStrength, settings.vignetteStrength);
        material.SetFloat(ShaderIDs.VignetteBlink, settings.vignetteBlink);

        material.SetFloat(ShaderIDs.Brightness, settings.brightness);
        material.SetFloat(ShaderIDs.Contrast, settings.contrast);
        material.SetFloat(ShaderIDs.Gamma, 1.0f / settings.gamma);
        material.SetFloat(ShaderIDs.Hue, settings.hue);
        material.SetFloat(ShaderIDs.Saturation, settings.saturation);

#if UNITY_2022_1_OR_NEWER
        cmd.Blit(colorBuffer, renderTextureHandle0, material);
        cmd.Blit(renderTextureHandle0, colorBuffer);
#else
        Blit(cmd, colorBuffer, renderTextureHandle0, material);
        Blit(cmd, renderTextureHandle0, colorBuffer);
#endif

        if (settings.enableProfiling == true)
#if UNITY_2022_1_OR_NEWER
          profilingScope.Dispose();
#else
          ProfilerMarker.End();
#endif

        context.ExecuteCommandBuffer(cmd);
        CommandBufferPool.Release(cmd);
      }
    }
  }
}
