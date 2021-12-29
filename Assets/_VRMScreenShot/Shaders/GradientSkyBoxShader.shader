Shader "Custom/GradientSkyBoxShader"
{
    Properties
    {
        _ColorStart ("ColorStart",Color) = (1,1,1,1)
        _ColorEnd ("ColorEnd",Color) = (1,1,1,1)
        _Angle ("Angle", Float) = 0
        _Intensity ("Intensity", Float) = 1
        _Exponent ("Exponent", Float) = 1
    }
    SubShader
    {
        Tags 
        { 
            "RenderType"="BackGround"
            "Queue"="Background"
            "PreviewType"="SkyBox"
        }

        Pass
        {
            ZWrite Off
            Cull Off
            
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

            half4 _ColorStart;
            half4 _ColorEnd;
            half _Angle;
            half _Intensity;
            half _Exponent;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                half2 pivot = half2(0.5,0.5);
                half cosAngle = cos(_Angle);
                half sinAngle = sin(_Angle);
                half2x2 rot = half2x2(cosAngle,-sinAngle,sinAngle,cosAngle);
                half2 uv = v.uv.xy - pivot;
                o.uv = mul(rot,uv);
                o.uv += pivot;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float p = pow(min(1.0f,max(0,1.0f-i.uv.x)),_Exponent);
                return min(1.0,lerp(_ColorStart,_ColorEnd, p) * _Intensity);
            }
            ENDCG
        }
    }
}
