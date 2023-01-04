Shader "Unlit/VertexMovement"
{
    Properties
    {
        _HorizontalOffset("Horizonetal Offset", Float) = 0
        _VerticalOffset("Vertical Offset", Float) = 0
        _Color("Color", Color) = (1, 1, 1, 1)
        _MainTex("Texture2D", 2D) = "white" {}
        _SecTex("Secondary Texture2D",2D) = "white"{}
        _MoveSpeed("MoveSpeed", Float) = 30
        _BlendSpeed("BlendSpeed", Float) = 1
        _MoveAmplitude("MoveAmplitude", Float) = 0.5
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            float _HorizontalOffset;
            float _VerticalOffset;
            half4 _Color;
            sampler2D _MainTex;
            float4 _MainTex_ST;
            sampler2D _SecTex;
            float4 _SecTex_ST;
            float _MoveSpeed;
            float _BlendSpeed;
            float _MoveAmplitude;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.vertex.x += _HorizontalOffset;
                //o.vertex.y += _VerticalOffset;
                o.vertex.y += sin(_MoveSpeed * _Time.y) * _MoveAmplitude;
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 col = lerp(tex2D(_MainTex, i.uv), tex2D(_SecTex, i.uv), abs(sin(_BlendSpeed * _Time.y)));
                col *= _Color;
                return col;
            }
            ENDCG
        }
    }
}
