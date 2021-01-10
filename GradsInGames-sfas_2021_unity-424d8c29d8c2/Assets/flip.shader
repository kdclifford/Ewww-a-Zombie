Shader "Custom/flip"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
            _ImageHeightY("Image Height", Float) = 0.0
              _ImageHeightX("Image Width", Float) = 0.0
            [Toggle(X_Flip)] _XFlip("XFlip", int) = 0
            [Toggle(Y_Flip)] _YFlip("YFlip", int) = 0
            _Zoom("Image Zoom", Float) = 0.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;
        fixed2 flip;
        half _ImageHeightY;
        half _ImageHeightX;
        half _XFlip;
        half _YFlip;
        half _Zoom;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            flip = IN.uv_MainTex;
            if (_XFlip == 1)
            {
                flip.x = 1 - flip.x;
            }

            if (_YFlip == 1)
            {
                flip.y = 1 - flip.y;
            }
            //flip.y += (flip.y / flip.y);

            
                flip.y += _ImageHeightY;           
                flip.x += _ImageHeightX;
            
            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D (_MainTex, flip * _Zoom) * _Color;
            o.Albedo = c.rgb;
            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
