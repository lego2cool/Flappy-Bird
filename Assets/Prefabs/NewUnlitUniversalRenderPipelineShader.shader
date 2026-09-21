Shader "Custom/URPCubeProceduralBorder"
{
    Properties
    {
        [HDR] _FillColor ("Fill Color", Color) = (1, 1, 1, 1)
        [HDR] _BorderColor ("Border Color", Color) = (0, 0, 0, 1)
        _BorderSize ("Border Size", Range(0.0, 0.5)) = 0.05
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
            Name "ProceduralCubeOutline"
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
            float _BorderSize;

            Varyings vert(Attributes input)
            {
                Varyings output;
                output.positionCS = TransformObjectToHClip(input.positionOS.xyz);
                output.uv = input.uv;
                return output;
            }

            half4 frag(Varyings input) : SV_Target
            {
                // Determine how far the current pixel is from any given edge of the face (0.0 to 1.0)
                // step() returns 1.0 if the UV is inside the border boundary, 0.0 if it's right on the edge
                float edgeX = step(_BorderSize, input.uv.x) * step(_BorderSize, 1.0 - input.uv.x);
                float edgeY = step(_BorderSize, input.uv.y) * step(_BorderSize, 1.0 - input.uv.y);

                // Combine X and Y axes. If either axis falls in the border size threshold, value becomes 0.0
                float isCenter = edgeX * edgeY;

                // Linearly interpolate between the border color (0.0) and the fill color (1.0)
                half4 finalColor = lerp(_BorderColor, _FillColor, isCenter);

                return finalColor;
            }
            ENDHLSL
        }
    }
}
