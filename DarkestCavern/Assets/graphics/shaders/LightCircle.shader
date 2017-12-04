Shader "Custom/LightCircle" {
	Properties {
		_LightPosition ("LightPosition", Vector) = (0,0,0,0)
		_Color ("Color", Color) = (1,1,1,1)
		_Power ("Power", Range(0,1)) = 0
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		#pragma surface surf Standard alpha:fade

		#pragma target 3.0

		float4 _LightPosition;
		fixed4 _Color;
		float _Power;

		struct Input {
			float2 uv_MainTex;
			float3 worldPos;
		};

		UNITY_INSTANCING_CBUFFER_START(Props)
		UNITY_INSTANCING_CBUFFER_END

		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			fixed4 c = _Color;
			o.Albedo = c.rgb;
			float2 d = abs(_LightPosition.xy - IN.worldPos.xy);
			o.Alpha = min(pow(sqrt(d.x * d.x + d.y * d.y) / max(_Power, 0.025) / 5, 2), 1);
		}
		ENDCG
	}
	FallBack "Diffuse"
}
