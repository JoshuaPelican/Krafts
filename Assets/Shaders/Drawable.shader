Shader "JoshShaders/Drawable"
{
    Properties
    {
        [HideInInspector] _MainTex ("Texture", 2D) = "white" {}
        [HideInInspector] _Color("Tint", Color) = (1,1,1,1)
        [HideInInspector] _DrawMap("Draw Map", 2D) = "white" {}
    }
    SubShader
    {
        Tags{"Queue" = "Transparent"}
        //ZTest Off
        Cull Off
        Blend One OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag
            #pragma shader_feature ACTIVE

            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex   : POSITION;
                float4 color    : COLOR;
                float2 texcoord : TEXCOORD0;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                fixed4 color : COLOR;
                float2 uv : TEXCOORD0;
            };

            sampler2D _MainTex;
            sampler2D _DrawMap;
            fixed4 _Color;

            v2f vert(appdata_t v)
            {
                v2f o;

                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.texcoord;
                o.color = v.color * _Color;

                return o;
            }

            float4 _MainTex_TexelSize;

            fixed4 frag(v2f i) : COLOR
            {
                fixed4 col = tex2D(_MainTex, i.uv) * i.color;
                fixed4 drawCol = tex2D(_DrawMap, i.uv);
                col.rgb *= col.a;
                
                fixed4 mainCol = lerp(col, drawCol, round(drawCol.a));

                return mainCol;
            }

            ENDCG
        }
    }
}
