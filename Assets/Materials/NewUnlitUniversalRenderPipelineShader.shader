Shader "Custom/URPCubePixelPerfectBorder"
{
    Properties
    {
        [HDR] _FillColor ("Fill Color", Color) = (1, 1, 1, 1)
        [HDR] _BorderColor ("Border Color", Color) = (0, 0, 0, 1)
        _BorderWidthPixels ("Border Width (Pixels)", Range(0, 10)) = 2
    }

    SubShader
    {
        Tags 
        { 
            "RenderType" = "Opaque" 
            "Queue" = "Geometry" 
            "RenderPipeline" = "UniversalPipeline" 
        }

        Pass
        {
            Name "PixelPerfectOutline"
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes
            {
                float4 positionOS   : POSITION;
                float2 uv           : TEXCOORD0;
            };

            struct Varyings
            {
                float4 positionCS   : SV_POSITION;
                float2 uv           : TEXCOORD0;
            };

            float4 _FillColor;
            float4 _BorderColor;
            float _BorderWidthPixels;

            Varyings vert(Attributes input)
            {
                Varyings output;
                output.positionCS = TransformObjectToHClip(input.positionOS.xyz);
                output.uv = input.uv;
                return output;
            }

            half4 frag(Varyings input) : SV_Target
            {
                // DDX and DDY measure how much the UV changes from one screen pixel to the next.
                // This gives us the exact UV-to-pixel ratio dynamically.
                float2 uvPerPixelX = ddx(input.uv);
                float2 uvPerPixelY = ddy(input.uv);

                // Calculate the length of the UV step across screen space pixels
                float2 pixelStepUV = float2(length(uvPerPixelX), length(uvPerPixelY));

                // Convert your desired pixel width into the local UV coordinate threshold
                float2 borderUV = pixelStepUV * _BorderWidthPixels;

                // Procedurally mask the borders out using the dynamically computed pixel thresholds
                float edgeX = step(borderUV.x, input.uv.x) * step(borderUV.x, 1.0 - input.uv.x);
                float edgeY = step(borderUV.y, input.uv.y) * step(borderUV.y, 1.0 - input.uv.y);

                float isCenter = edgeX * edgeY;

                // Smoothly blend if needed, or straight lerp for a razor sharp line
                return lerp(_BorderColor, _FillColor, isCenter);
            }
            ENDHLSL
        }
    }
}