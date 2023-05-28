Shader "Custom/LightBeamDoubleSidedTexture" 
{
    Properties 
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex("Albedo (RGB)", 2D) = "white" {}
        _FadeDist("Fade Distance", Float) = 12
        _TimeXInc("Time movement x", Float) = 0.01
        _TimeYInc("Time movement y", Float) = 0.02
        _LerpStart("Lerp start", Float) = -0.5
        _LerpEnd("Lerp end", Float) = 2.5
        _Power("Fade Power",Float) = 2
        _NormalPower("Normal Power", Float) = 1
    }

    SubShader 
    {
        Tags { "RenderType"="Transparent" "RenderPipeline"="UniversalRenderPipeline" }
        LOD 100
        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off
        Cull Off

        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile _ _MAIN_LIGHT_SHADOWS
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION;
                float3 normalOS : NORMAL;
                float2 uv : TEXCOORD0;
            };

            struct Varyings
            {
                float4 positionCS : SV_POSITION;
                float3 positionWS : TEXCOORD0;
                float3 modelPos : TEXCOORD1;
                float3 normal : TEXCOORD2;
                float2 uv : TEXCOORD3;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            float4 _Color;
            sampler2D _MainTex;
            float _FadeDist;
            float _TimeXInc;
            float _TimeYInc;
            float _LerpStart;
            float _LerpEnd;
            float _Power;
            float _NormalPower;

            Varyings vert(Attributes input)
            {
                Varyings output;
                output.positionCS = TransformObjectToHClip(input.positionOS.xyz);
                output.positionWS = TransformObjectToWorld(input.positionOS.xyz);
                output.modelPos = TransformObjectToWorld(float3(0, 0, 0));
                output.normal = mul((float3x3)unity_ObjectToWorld, input.normalOS);
                output.uv = input.uv;
                UNITY_SETUP_INSTANCE_ID(input);
                UNITY_TRANSFER_INSTANCE_ID(input, output);
                return output;
            }

            half4 frag(Varyings input, half face : VFACE) : SV_Target
            {
                float2 uv = input.uv;
                uv.x += _Time.y * _TimeXInc;
                uv.y -= _Time.y * _TimeYInc;

                half4 col = tex2D(_MainTex, uv) * _Color;

                float fadeStart = 0;
                float fadeEnd = _FadeDist;

                float3 dir2pos = input.positionWS - input.modelPos;
                float d = length(dir2pos);
                float fade = 1 - saturate((d - fadeStart) / (fadeEnd - fadeStart));

                fade = pow(fade, _Power);
                
                float3 dir2Cam = normalize(_WorldSpaceCameraPos - input.positionWS);
                float3 normal = normalize(input.normal) * sign(face);

                float dotVal = max(0.0, dot(normal, dir2Cam));
                float val = pow(dotVal, _NormalPower);
                fade *= max(0.0f, lerp(_LerpStart, _LerpEnd, val));

                return half4(_Color.rgb + col.rgb, _Color.a * fade);
            }
            ENDHLSL
        }
    }
    Fallback "Universal Render Pipeline/Transparent"
}
