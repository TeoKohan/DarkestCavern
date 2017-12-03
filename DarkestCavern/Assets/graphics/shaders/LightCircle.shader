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
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows alpha:fade

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		float4 _LightPosition;
		fixed4 _Color;
		float _Power;

		struct Input {
			float2 uv_MainTex;
			float3 worldPos;
		};

		// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
		// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
		// #pragma instancing_options assumeuniformscaling
		UNITY_INSTANCING_CBUFFER_START(Props)
			// put more per-instance properties here
		UNITY_INSTANCING_CBUFFER_END

		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			fixed4 c = _Color;
			o.Albedo = c.rgb;
			float2 d = abs(_LightPosition.xy - IN.worldPos.xy);
			o.Alpha = max(pow(sqrt(d.x * d.x + d.y * d.y) / _Power / 10, _Power * 2), 1 - _Power + 0.25);
		}
		ENDCG
	}
	FallBack "Diffuse"
}
