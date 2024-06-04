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
Shader "Hidden/Fronkon Games/Spice Up/Double Vision URP"
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
      Name "Fronkon Games Spice Up Double Vision"

      HLSLPROGRAM
      #include "SpiceUp.hlsl"
      #include "ColorBlend.hlsl"
      #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"

      #pragma vertex SpiceUpVert
      #pragma fragment SpiceUpFrag
      #pragma fragmentoption ARB_precision_hint_fastest
      #pragma exclude_renderers d3d9 d3d11_9x ps3 flash
      #pragma multi_compile _ _USE_DRAW_PROCEDURAL

      float3 _Strength;
      float3 _Speed;
      float3 _ColorOffset;
      float _BlendStrength;
      int _ColorBlend;
      
      half4 SpiceUpFrag(const VertexOutput input) : SV_Target 
      {
        UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input);
        const float2 uv = UnityStereoTransformScreenSpaceTex(input.uv).xy;

        const half4 color = SAMPLE_MAIN(uv);

        _Strength.xy *= _MainTex_TexelSize.xy * _Strength.z;
        _Speed.xy *= _Speed.z;
        _ColorOffset *= _MainTex_TexelSize.xyy * _Strength.z;

        half4 pixel = color;
        pixel.r = SAMPLE_MAIN(uv + float2(sin(_Time.y * _Speed.x) * _Strength.x + _ColorOffset.x + cos(_Time.y * _ColorOffset.x) * _Strength.x,
                                          cos(_Time.y * _Speed.y) * _Strength.y + _ColorOffset.x + sin(_Time.y * _ColorOffset.x) * _Strength.y)).r;
        pixel.g = SAMPLE_MAIN(uv + float2(sin(_Time.y * _Speed.x) * _Strength.x + _ColorOffset.y + cos(_Time.y * _ColorOffset.y) * _Strength.x,
                                          cos(_Time.y * _Speed.y) * _Strength.y + _ColorOffset.y + sin(_Time.y * _ColorOffset.y) * _Strength.y)).g;
        pixel.b = SAMPLE_MAIN(uv + float2(sin(_Time.y * _Speed.x) * _Strength.x + _ColorOffset.z + cos(_Time.y * _ColorOffset.z) * _Strength.x,
                                          cos(_Time.y * _Speed.y) * _Strength.y + _ColorOffset.z + sin(_Time.y * _ColorOffset.z) * _Strength.y)).b;

        pixel.rgb = lerp(pixel.rgb, ColorBlend(_ColorBlend, color.rgb, pixel.rgb), _BlendStrength);
        
        // Color adjust.
        pixel.rgb = ColorAdjust(pixel.rgb);

        return lerp(color, pixel, _Intensity);
      }

      ENDHLSL
    }
  }
  
  FallBack "Diffuse"
}
