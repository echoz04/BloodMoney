Shader "Custom/ColliderStop"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _UseAlpha ("Use Texture Alpha", Float) = 1
        _EmissionColor ("Emission Color", Color) = (1,1,1,1)
        _EmissionStrength ("Emission Strength", Range(0,10)) = 2
        _VisibleDistance ("Visible Distance", Range(0.1,100)) = 3
    }

    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off
        Cull Back    // ← скрываем обратную сторону

        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _UseAlpha;
            float4 _EmissionColor;
            float _EmissionStrength;
            float _VisibleDistance;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
                float3 worldPos : TEXCOORD1;
            };

            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                return o;
            }

            float4 frag(v2f i) : SV_Target
            {
                float3 camPos = _WorldSpaceCameraPos;
                float dist = distance(i.worldPos, camPos);

                // Прозрачность по расстоянию
                float visibility = saturate(1 - (dist / _VisibleDistance));

                float4 texColor = tex2D(_MainTex, i.uv);

                // Учитываем альфа-канал текстуры
                float alpha = _UseAlpha > 0.5 ? texColor.a : 1.0;

                float3 emission = _EmissionColor.rgb * _EmissionStrength;

                float finalAlpha = visibility * alpha;

                return float4(texColor.rgb * visibility + emission * visibility, finalAlpha);
            }
            ENDHLSL
        }
    }
}