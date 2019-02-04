// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Unlit/NewUnlitShader"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
	}

SubShader {
   Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
   LOD 200
   ZWrite Off
   Blend SrcAlpha OneMinusSrcAlpha
   CGPROGRAM
   // define finalcolor and vertex programs:
   #pragma surface surf Lambert finalcolor:mycolor vertex:myvert
   struct Input {
       float2 uv_MainTex;
       half alpha;
   };
   sampler2D _MainTex;
   half _LoY;
   half _HiY;
   void myvert (inout appdata_full v, out Input data) {
       // convert the vertex to world space: 
       float4 worldV = mul (unity_ObjectToWorld, v.vertex);
       // calculate alpha according to the world Y coordinate:
       data.alpha = 1 - saturate((worldV.y - _LoY) / (_HiY - _LoY));
   }
   
   void mycolor (Input IN, SurfaceOutput o, inout fixed4 color) {
       // set the vertex color alpha to the value calculated: 
       color.a = IN.alpha;
   }
   
   void surf (Input IN, inout SurfaceOutput o) {
       // simply copy the corresponding texture element color:
       o.Albedo = tex2D (_MainTex, IN.uv_MainTex).rgb;
   }
   
   ENDCG
 } 
 Fallback "Diffuse"
 
 }
