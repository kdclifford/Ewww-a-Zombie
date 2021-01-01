Shader "Custom/StencilRead"
{
	Properties{
		_Color("Tint", Color) = (0, 0, 0, 1)
		_MainTex("Texture", 2D) = "white" {}
		_Smoothness("Smoothness", Range(0, 1)) = 0
		_Metallic("Metalness", Range(0, 1)) = 0
		[HDR] _EmissionColor("Emission", color) = (0,0,0)
			[Toggle(USE_TEXTURE)] _UseTexture("Use Emission", int) = 0

		[IntRange] _StencilRef("Stencil Reference Value", Range(0,255)) = 0
	}
		SubShader{
			Tags{ "RenderType" = "Opaque" "Queue" = "Geometry"}

			//stencil operation
			Stencil{
				Ref[_StencilRef]
				Comp Equal
			}

			CGPROGRAM

			#pragma surface surf Standard fullforwardshadows
			#pragma target 3.0

			sampler2D _MainTex;
			fixed4 _Color;

			half _Smoothness;
			half _Metallic;
			half3 _EmissionColor;
			int _UseTexture;
			struct Input {
				float2 uv_MainTex;
			};

			void surf(Input i, inout SurfaceOutputStandard o) {
				fixed4 col = tex2D(_MainTex, i.uv_MainTex);
				col *= _Color;
				o.Albedo = col.rgb;
				o.Metallic = _Metallic;
				o.Smoothness = _Smoothness;
				if (_UseTexture == 1)
				{

					//o.Emission = _EmissionColor;
				}
				o.Emission = _EmissionColor;

			}
			ENDCG
		}
			FallBack "Standard"
}