Shader "Custom/LightBeamColor" 
{
    Properties 
    {
        [HideInInspector]
        _Color ("Color", Color) = (1,1,1,1)
        _FadeDist("Fade Distance", Float ) = 12
        _LerpStart("Lerp start", Float) = -0.5
        _LerpEnd("Lerp end", Float ) = 2.5
        _Power("Fade Power",Float) = 2
        _NormalPower("Normal Power", Float) = 1
    }

    SubShader 
    {
        Tags { "RenderType"="Transparent" "RenderPipeline"="UniversalRenderPipeline" }
        LOD 100
        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off

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
            };

            struct Varyings
            {
                float4 positionCS : SV_POSITION;
                float3 positionWS : TEXCOORD0;
                float3 normalWS : NORMAL;
                float3 modelPos : TEXCOORD1;
            };

            float4 _Color;
            float _FadeDist;
            float _LerpStart;
            float _LerpEnd;
            float _Power;
            float _NormalPower;

            Varyings vert(Attributes input)
            {
                Varyings output;
                output.positionCS = TransformObjectToHClip(input.positionOS.xyz);
                output.positionWS = TransformObjectToWorld(input.positionOS.xyz);
                output.normalWS = TransformObjectToWorldNormal(input.normalOS);
                output.modelPos = TransformObjectToWorld(float3(0, 0, 0)); 
                return output;
            }

            half4 frag(Varyings input) : SV_Target
            {
                float fadeStart = 0;
                float fadeEnd = _FadeDist;

                float3 dir2pos = input.positionWS - input.modelPos;
                float d = length(dir2pos);
                float fade = 1 - saturate((d - fadeStart) / (fadeEnd - fadeStart));

                fade = pow(fade, _Power);
                
                float3 dir2Cam = normalize(_WorldSpaceCameraPos - input.positionWS);
                float3 normal = normalize(input.normalWS);

                float dotVal = max(0.0, dot(normal, dir2Cam));
                float val = pow(dotVal, _NormalPower);
                fade *= max(0.0f, lerp(_LerpStart, _LerpEnd, val));

                return half4(_Color.rgb, _Color.a * fade);
            }
            ENDHLSL
        }
    }
    Fallback "Universal Render Pipeline/Transparent"
}
