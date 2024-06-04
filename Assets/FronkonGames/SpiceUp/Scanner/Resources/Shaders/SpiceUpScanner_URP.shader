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
Shader "Hidden/Fronkon Games/Spice Up/Scanner URP"
{
  Properties
  {
    _MainTex("Main Texture", 2D) = "white" {}
  }

  SubShader
  {
    Tags
    {
      "RenderType" = "Opaque"
      "RenderPipeline" = "UniversalPipeline"
    }
    LOD 100
    ZTest Always ZWrite Off Cull Off

    Pass
    {
      Name "Fronkon Games Spice Up Scanner"

      HLSLPROGRAM
      #include "SpiceUp.hlsl"
      #include "ColorBlend.hlsl"
      #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"

      #pragma vertex SpiceUpVert
      #pragma fragment SpiceUpFrag
      #pragma fragmentoption ARB_precision_hint_fastest
      #pragma exclude_renderers d3d9 d3d11_9x ps3 flash
      #pragma multi_compile _ _USE_DRAW_PROCEDURAL
      #pragma multi_compile _ ENABLE_BARREL

      float _Strength;
      float _Count;
      half4 _TintLines;
      int _BlendLines;
      half4 _TintBackground;
      int _BlendBackground;
      float _ScanlineStrength;
      float _ScanlineWidth;
      float _ScanlineSpeed;
      half4 _ScanlineTint;
      int _ScanlineBlend;
      float _NoiseBandStrength;
      half4 _NoiseBandTint;
      int _NoiseBandBlend;
      float _NoiseBandSpeed;
      float _NoiseBandWidth;
      float _BarrelStrength;
      float _BarrelZoom;
      half4 _BarrelTint;
      float _Interlace;
      float _FrameNoise;
      float _SignalNoise;
      float _BadSignalStrength;
      float _VignetteStrength;
      float _VignetteBlink;

      inline float Noise(float2 p)
      {
        float2 ip = floor(p);
        float2 u = frac(p);
        u = u * u * (3.0 - 2.0 * u);

        float res = lerp(lerp(Rand(ip), Rand(ip+ float2(1.0, 0.0)), u.x),
                         lerp(Rand(ip + float2(0.0, 1.0)), Rand(ip + float2(1.0, 1.0)), u.x), u.y);
        return res * res;
      }

      float2 BrownConradyDistortion(float2 uv, float scalar)
      {
        uv = (uv - 0.5) * 2.0;
        const float K1 = -0.02 * scalar;
        const float K2 = -0.01 * scalar;

        float r2 = dot(uv, uv);
        uv *= 1.0 + K1 * r2 + K2 * r2 * r2;

        return (uv / 2.0) + 0.5;
      }

      float2 Displace(float2 uv, float seed, float seed2)
      {
        float2 shift = (float2)0.0;

        if (Rand(seed) > 0.5)
          shift += 0.1 * (float2)(2.0 * (0.5 - Rand(seed2)));
        
        if (Rand(seed2) > 0.6)
        {
          if (uv.y > 0.5)
            shift.x *= Rand(seed2 * seed);
        }

        return shift;
      }

      float2 Barrel(float2 uv)
      {
        uv = (uv - 0.5) * 2.0;
        uv *= 1.2;
        uv.x *= 1.0 + pow((abs(uv.y) / 5.0), 2.0) * _BarrelStrength * _Strength;
        uv.y *= 1.0 + pow((abs(uv.x) / 4.0), 2.0) * _BarrelStrength * _Strength;
        uv /= 1.2 - (_BarrelZoom * _Strength);
        uv  = (uv / 2.0) + 0.5;
      
        return uv;
      }

      half4 SpiceUpFrag(const VertexOutput input) : SV_Target 
      {
        UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input);
        const float2 uv = UnityStereoTransformScreenSpaceTex(input.uv).xy;

        const half4 color = SAMPLE_MAIN(uv);
        half4 pixel = color;
        
        float2 barrelUV = Barrel(uv);

        float2 rDisplace = (float2)0.0;
        float2 gDisplace = (float2)0.0;
        float2 bDisplace = (float2)0.0;

        if (Rand(_Time.y) > 1.0 - _FrameNoise)
        {
          rDisplace = Displace(barrelUV, _Time.y * 2.0, 2.0 + _Time.y) * _Strength;
          gDisplace = Displace(barrelUV, _Time.y * 3.0, 3.0 + _Time.y) * _Strength;
          bDisplace = Displace(barrelUV, _Time.y * 5.0, 5.0 + _Time.y) * _Strength;
        }

        rDisplace.x += 0.05 * (0.5 - Rand(_Time.y * 37.0 * barrelUV.y)) * _SignalNoise * _Strength;
        gDisplace.x += 0.07 * (0.5 - Rand(_Time.y * 41.0 * barrelUV.y)) * _SignalNoise * _Strength;
        bDisplace.x += 0.01 * (0.5 - Rand(_Time.y * 53.0 * barrelUV.y)) * _SignalNoise * _Strength;

        pixel.r = SAMPLE_MAIN(barrelUV + rDisplace).r;
        pixel.g = SAMPLE_MAIN(barrelUV + gDisplace).g;
        pixel.b = SAMPLE_MAIN(barrelUV + bDisplace).b;

        pixel.rgb = lerp(pixel.rgb * (((sin(_Time.y * 4.0) * _Interlace * _Strength) + 0.75) +
                                     (Rand(_Time.y) * _Interlace * _Strength)), pixel.rgb, uint(barrelUV.y) % 3);

        if (_Count > 0)
        {
          float lines = clamp(sin(barrelUV.y * _Count), 0.0, 1.0);
          half3 linesColor = lerp(0.0, ColorBlend(_BlendLines, pixel.rgb, _TintLines.rgb * _TintLines.a), lines) +
                            lerp(0.0, ColorBlend(_BlendBackground, pixel.rgb, _TintBackground.rgb * _TintBackground.a), 1.0 - lines);
          pixel.rgb = lerp(pixel.rgb, linesColor, _Strength);
        }

        float y = frac(_Time.y * _ScanlineSpeed);
        float scanline = (smoothstep(y - _ScanlineWidth, y, barrelUV.y) - smoothstep(y, y + _ScanlineWidth, barrelUV.y)) * abs(sin((barrelUV.y + _Time.y))) * _ScanlineStrength;

        pixel.rgb = lerp(pixel.rgb, ColorBlend(_ScanlineBlend, pixel.rgb, _ScanlineTint.rgb * _ScanlineTint.a), scanline * _Strength);

        float p = frac(-_Time.y * _NoiseBandSpeed);
        float d = smoothstep(p - _NoiseBandWidth, p, barrelUV.y) - smoothstep(p, p + _NoiseBandWidth, barrelUV.y);
        float noise = Noise((barrelUV * 1000.0 + _Time.y * 10.0) * 5.0) * d * _NoiseBandStrength * _Strength;

        pixel.rgb = lerp(pixel.rgb, ColorBlend(_NoiseBandBlend, pixel.rgb, _NoiseBandTint.rgb * _NoiseBandTint.a), noise * _Strength);

        float badSignal = cos(barrelUV.y * _ScreenParams.y) * 2.0;
        badSignal += tan(sin(100.0 + _Time.y * cos(100.0 + _Time.y) * 2.0) * 14.0 * barrelUV.y) * 0.05;
        pixel.rgb += badSignal * 0.01 * _BadSignalStrength * _Strength;

        float vignette = 50.0 * barrelUV.x * (1.0 - barrelUV.x) * barrelUV.y * (1.0 - barrelUV.y) * _Strength;
        vignette *= lerp(1.0 - _VignetteBlink, 1.0, Rand(_Time.y * 0.5));
        pixel.rgb = lerp(pixel.rgb, pixel.rgb * vignette, _VignetteStrength * _Strength);

        if (barrelUV.x < 0.0 || barrelUV.x > 1.0 || barrelUV.y < 0.0 || barrelUV.y > 1.0)
		      pixel.rgb = lerp(pixel.rgb, _BarrelTint.rgb, _BarrelTint.a);

        // Color adjust.
        pixel.rgb = ColorAdjust(pixel.rgb);

        return lerp(color, pixel, _Intensity);
      }

      ENDHLSL
    }
  }
  
  FallBack "Diffuse"
}
