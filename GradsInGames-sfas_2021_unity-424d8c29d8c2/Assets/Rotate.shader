Shader "Custom/RotateUVs" {
    Properties{
        _MainTex("Base (RGB)", 2D) = "white" {}
        _RotationSpeed("Rotation Speed", Float) = 2.0
            _RotationSpeed2("Rotation Speed", Float) = 2.0
            _RotationSpeed3("Rotation Speed", Float) = 2.0
    }
        SubShader{
            Tags { "RenderType" = "Opaque" }
            LOD 200

            CGPROGRAM
            #pragma surface surf Lambert vertex:vert

            sampler2D _MainTex;

            struct Input {
                float2 uv_MainTex;
            };

            float _RotationSpeed;
            float _RotationSpeed2;
            float _RotationSpeed3;
            void vert(inout appdata_full v) {
                float sinX = sin(_RotationSpeed);
                float cosX = cos(_RotationSpeed2);
                float sinY = sin(_RotationSpeed3);
                float2x2 rotationMatrix = float2x2(cosX, -sinX, sinY, cosX);
                v.texcoord.xy = mul(v.texcoord.xy, rotationMatrix);
            }

            void surf(Input IN, inout SurfaceOutput o) {
                half4 c = tex2D(_MainTex, IN.uv_MainTex);
                o.Albedo = c.rgb;
                o.Alpha = c.a;
            }
            ENDCG
        }
            FallBack "Diffuse"
}
