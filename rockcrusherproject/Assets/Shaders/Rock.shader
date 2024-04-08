Shader "Unlit/Rock"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _RunEffect("Run Effect", Int) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        Tags {"Queue"="Transparent"}
        LOD 100

        
        Blend SrcAlpha OneMinusSrcAlpha //odpowiada za import przezroczystoœci
        Pass
        {
            CGPROGRAM
        // Upgrade NOTE: excluded shader from DX11; has structs without semantics (struct appdata members runeffect)
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

            sampler2D _MainTex;
            float4 _MainTex_ST;
            int _RunEffect=0;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float4 normalCol = tex2D(_MainTex, i.uv);
                       normalCol.a = tex2D(_MainTex, i.uv).a;
                
                float4 effectCol = float4(1,1,1,1);
                effectCol.a = tex2D(_MainTex, i.uv).a;
           
                if(_RunEffect==0)return normalCol;
                else return effectCol;
            }
            ENDCG
        }
    }
}
