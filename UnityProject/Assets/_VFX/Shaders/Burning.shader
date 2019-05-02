// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "PolyTools/Burning"
{
	Properties
	{
		_Diffuse("Diffuse", 2D) = "white" {}
		_Mask("Mask", 2D) = "white" {}
		_DistortionMap("Distortion Map", 2D) = "bump" {}
		_Hot("Hot", Color) = (0,0,0,0)
		_Warm("Warm", Color) = (0,0,0,0)
		_DistortionAmount("Distortion Amount", Range( 0 , 1)) = 0
		_ScrollSpeed("Scroll Speed", Range( 0 , 1)) = 0.43
		_Burn("Burn", Range( 0 , 2)) = 0.184143
		_HeatWave("Heat Wave", Range( 0 , 1)) = 0
		_DissolveAmount("Dissolve Amount", Range( 0 , 2)) = 0
		_WiggleAmount("Wiggle Amount", Float) = 0
		_Zoom("Zoom", Float) = 2
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Back
		Blend SrcAlpha OneMinusSrcAlpha
		
		AlphaToMask On
		CGINCLUDE
		#include "UnityShaderVariables.cginc"
		#include "UnityPBSLighting.cginc"
		#include "Lighting.cginc"
		#pragma target 3.0
		struct Input
		{
			float3 worldPos;
			float2 uv_texcoord;
		};

		uniform sampler2D _DistortionMap;
		uniform float _ScrollSpeed;
		uniform sampler2D _Mask;
		uniform float _HeatWave;
		uniform float _Zoom;
		uniform float _Burn;
		uniform float _WiggleAmount;
		uniform sampler2D _Diffuse;
		uniform float4 _Diffuse_ST;
		uniform float4 _Warm;
		uniform float4 _Hot;
		uniform float4 _DistortionMap_ST;
		uniform float _DistortionAmount;
		uniform float _DissolveAmount;

		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			float3 ase_worldPos = mul( unity_ObjectToWorld, v.vertex );
			float3 ase_vertex3Pos = v.vertex.xyz;
			float temp_output_11_0 = ( _Time.y * _ScrollSpeed );
			float2 panner31 = ( temp_output_11_0 * float2( 0,-1 ) + v.texcoord.xy);
			float4 tex2DNode30 = tex2Dlod( _DistortionMap, float4( panner31, 0, 0.0) );
			float4 tex2DNode20 = tex2Dlod( _Mask, float4( ( ( ( (tex2DNode30).rga * _HeatWave ) + float3( v.texcoord.xy ,  0.0 ) ) * _Zoom ).xy, 0, 0.0) );
			float temp_output_21_0 = step( tex2DNode20.r , _Burn );
			v.vertex.xyz += ( ( float4( ( ase_worldPos * ase_vertex3Pos ) , 0.0 ) * tex2DNode30 * temp_output_21_0 * 0.1 ) * _WiggleAmount ).rgb;
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_Diffuse = i.uv_texcoord * _Diffuse_ST.xy + _Diffuse_ST.zw;
			o.Albedo = tex2D( _Diffuse, uv_Diffuse ).rgb;
			float2 uv_DistortionMap = i.uv_texcoord * _DistortionMap_ST.xy + _DistortionMap_ST.zw;
			float temp_output_11_0 = ( _Time.y * _ScrollSpeed );
			float2 panner10 = ( temp_output_11_0 * float2( 0,-1 ) + float2( 0,0 ));
			float2 uv_TexCoord9 = i.uv_texcoord + panner10;
			float4 lerpResult16 = lerp( _Warm , _Hot , tex2D( _Mask, ( ( (UnpackNormal( tex2D( _DistortionMap, uv_DistortionMap ) )).xy * _DistortionAmount ) + uv_TexCoord9 ) ).r);
			float4 temp_cast_1 = (2.0).xxxx;
			float2 panner31 = ( temp_output_11_0 * float2( 0,-1 ) + i.uv_texcoord);
			float4 tex2DNode30 = tex2D( _DistortionMap, panner31 );
			float4 tex2DNode20 = tex2D( _Mask, ( ( ( (tex2DNode30).rga * _HeatWave ) + float3( i.uv_texcoord ,  0.0 ) ) * _Zoom ).xy );
			float temp_output_21_0 = step( tex2DNode20.r , _Burn );
			float temp_output_42_0 = ( step( tex2DNode20.r , ( 1.0 - ( _DissolveAmount / 2.0 ) ) ) - step( tex2DNode20.r , ( 1.0 - _DissolveAmount ) ) );
			float4 temp_cast_4 = (temp_output_42_0).xxxx;
			o.Emission = ( ( ( pow( lerpResult16 , temp_cast_1 ) * 2.0 ) * ( temp_output_21_0 + ( temp_output_21_0 - step( tex2DNode20.r , ( _Burn / 2.0 ) ) ) ) ) - temp_cast_4 ).rgb;
			o.Alpha = 1;
		}

		ENDCG
		CGPROGRAM
		#pragma surface surf Standard keepalpha fullforwardshadows vertex:vertexDataFunc 

		ENDCG
		Pass
		{
			Name "ShadowCaster"
			Tags{ "LightMode" = "ShadowCaster" }
			ZWrite On
			AlphaToMask Off
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0
			#pragma multi_compile_shadowcaster
			#pragma multi_compile UNITY_PASS_SHADOWCASTER
			#pragma skip_variants FOG_LINEAR FOG_EXP FOG_EXP2
			#include "HLSLSupport.cginc"
			#if ( SHADER_API_D3D11 || SHADER_API_GLCORE || SHADER_API_GLES3 || SHADER_API_METAL || SHADER_API_VULKAN )
				#define CAN_SKIP_VPOS
			#endif
			#include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "UnityPBSLighting.cginc"
			struct v2f
			{
				V2F_SHADOW_CASTER;
				float2 customPack1 : TEXCOORD1;
				float3 worldPos : TEXCOORD2;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};
			v2f vert( appdata_full v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID( v );
				UNITY_INITIALIZE_OUTPUT( v2f, o );
				UNITY_TRANSFER_INSTANCE_ID( v, o );
				Input customInputData;
				vertexDataFunc( v, customInputData );
				float3 worldPos = mul( unity_ObjectToWorld, v.vertex ).xyz;
				half3 worldNormal = UnityObjectToWorldNormal( v.normal );
				o.customPack1.xy = customInputData.uv_texcoord;
				o.customPack1.xy = v.texcoord;
				o.worldPos = worldPos;
				TRANSFER_SHADOW_CASTER_NORMALOFFSET( o )
				return o;
			}
			half4 frag( v2f IN
			#if !defined( CAN_SKIP_VPOS )
			, UNITY_VPOS_TYPE vpos : VPOS
			#endif
			) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID( IN );
				Input surfIN;
				UNITY_INITIALIZE_OUTPUT( Input, surfIN );
				surfIN.uv_texcoord = IN.customPack1.xy;
				float3 worldPos = IN.worldPos;
				half3 worldViewDir = normalize( UnityWorldSpaceViewDir( worldPos ) );
				surfIN.worldPos = worldPos;
				SurfaceOutputStandard o;
				UNITY_INITIALIZE_OUTPUT( SurfaceOutputStandard, o )
				surf( surfIN, o );
				#if defined( CAN_SKIP_VPOS )
				float2 vpos = IN.pos;
				#endif
				SHADOW_CASTER_FRAGMENT( IN )
			}
			ENDCG
		}
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=15800
104;42;1185;530;1934.837;516.8288;2.966499;True;False
Node;AmplifyShaderEditor.SimpleTimeNode;13;-4245.409,28.4486;Float;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;12;-4253.417,265.9816;Float;False;Property;_ScrollSpeed;Scroll Speed;7;0;Create;True;0;0;False;0;0.43;0.381;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;11;-3920.771,157.3418;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;32;-3988.854,600.6381;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TexturePropertyNode;3;-3605.672,-726.145;Float;True;Property;_DistortionMap;Distortion Map;3;0;Create;True;0;0;False;0;None;226e46221f80a154c90ee734a4bc3df8;False;bump;Auto;Texture2D;0;1;SAMPLER2D;0
Node;AmplifyShaderEditor.PannerNode;31;-3659.96,569.3362;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,-1;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;30;-3320.79,601.6003;Float;True;Property;_TextureSample3;Texture Sample 3;8;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ComponentMaskNode;33;-2958.75,659.5057;Float;False;True;True;False;True;1;0;COLOR;0,0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;35;-3001.997,889.2488;Float;False;Property;_HeatWave;Heat Wave;9;0;Create;True;0;0;False;0;0;0.02;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;4;-3256.928,-726.0511;Float;True;Property;_TextureSample1;Texture Sample 1;3;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;36;-2779.509,1196.676;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;34;-2656.03,759.5115;Float;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;6;-2890.13,-504.9799;Float;False;Property;_DistortionAmount;Distortion Amount;6;0;Create;True;0;0;False;0;0;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.ComponentMaskNode;5;-2879.805,-736.8623;Float;False;True;True;False;True;1;0;FLOAT3;0,0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PannerNode;10;-3594.688,144.8509;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,-1;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;7;-2541.173,-612.5751;Float;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;60;-2053.45,828.8956;Float;False;Property;_Zoom;Zoom;12;0;Create;True;0;0;False;0;2;2;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;9;-3279.675,137.257;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;37;-2323.07,712.8345;Float;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT2;0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleAddOpNode;8;-2325.443,41.07862;Float;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;22;-1455.56,707.4687;Float;False;Property;_Burn;Burn;8;0;Create;True;0;0;False;0;0.184143;0.68;0;2;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;27;-1470.831,962.4241;Float;False;Constant;_DivideAmount;Divide Amount;8;0;Create;True;0;0;False;0;2;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TexturePropertyNode;1;-2117.867,-202.1553;Float;True;Property;_Mask;Mask;2;0;Create;True;0;0;False;0;None;5f4b49e2e5f38fe45be7687c93e50f56;False;white;Auto;Texture2D;0;1;SAMPLER2D;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;59;-1874.931,646.4453;Float;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;26;-1128.489,862.3337;Float;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;20;-1455.236,488.9879;Float;True;Property;_TextureSample2;Texture Sample 2;6;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;14;-1389.937,-503.2838;Float;False;Property;_Warm;Warm;5;0;Create;True;0;0;False;0;0,0,0,0;0.363999,0.8807263,1,0;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;15;-1387.029,-314.2647;Float;False;Property;_Hot;Hot;4;0;Create;True;0;0;False;0;0,0,0,0;0.85562,0.9559999,0.9559999,0;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;2;-1453.579,-28.1884;Float;True;Property;_TextureSample0;Texture Sample 0;2;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;50;-1480.345,1250.917;Float;False;Property;_DissolveAmount;Dissolve Amount;10;0;Create;True;0;0;False;0;0;0.82;0;2;0;1;FLOAT;0
Node;AmplifyShaderEditor.StepOpNode;21;-1050.773,459.3381;Float;False;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;17;-1006.084,-168.8661;Float;False;Constant;_Float0;Float 0;4;0;Create;True;0;0;False;0;2;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;51;-1131.83,1149.273;Float;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StepOpNode;25;-854.2927,703.34;Float;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;16;-1070.059,-416.0442;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.PowerNode;18;-787.9852,-320.0807;Float;False;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.PosVertexDataNode;52;-176.174,-125.7573;Float;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.WorldPosInputsNode;54;-174.1693,-339.9898;Float;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.OneMinusNode;39;-826.3328,1005.499;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;38;-827.4613,1261.035;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;28;-610.303,642.6478;Float;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StepOpNode;41;-576.5441,1269.907;Float;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;58;102.0615,26.9197;Float;False;Constant;_VertexCompensator;Vertex Compensator;11;0;Create;True;0;0;False;0;0.1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.StepOpNode;40;-584.5525,1002.965;Float;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;19;-497.1876,-325.8966;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;29;-370.3116,400.8646;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;55;96.43317,-249.0311;Float;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;56;400.6968,166.0885;Float;False;Property;_WiggleAmount;Wiggle Amount;11;0;Create;True;0;0;False;0;0;0.2;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;53;365.9462,-73.35265;Float;False;4;4;0;FLOAT3;0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;23;-139.1343,330.4678;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;42;-318.1427,1134.417;Float;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;57;630.816,101.1602;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;24;600.157,-621.5811;Float;True;Property;_Diffuse;Diffuse;0;0;Create;True;0;0;False;0;None;f6df8509372384f48bb3fa1a762ba9b3;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleSubtractOpNode;48;105.3889,542.2571;Float;False;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;49;348.8914,873.2366;Float;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;1026.511,153.2107;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;PolyTools/Burning;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Custom;0.5;True;True;0;True;Transparent;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;2;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;1;-1;-1;-1;0;True;0;0;False;-1;-1;0;False;-1;0;0;0;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;11;0;13;0
WireConnection;11;1;12;0
WireConnection;31;0;32;0
WireConnection;31;1;11;0
WireConnection;30;0;3;0
WireConnection;30;1;31;0
WireConnection;33;0;30;0
WireConnection;4;0;3;0
WireConnection;34;0;33;0
WireConnection;34;1;35;0
WireConnection;5;0;4;0
WireConnection;10;1;11;0
WireConnection;7;0;5;0
WireConnection;7;1;6;0
WireConnection;9;1;10;0
WireConnection;37;0;34;0
WireConnection;37;1;36;0
WireConnection;8;0;7;0
WireConnection;8;1;9;0
WireConnection;59;0;37;0
WireConnection;59;1;60;0
WireConnection;26;0;22;0
WireConnection;26;1;27;0
WireConnection;20;0;1;0
WireConnection;20;1;59;0
WireConnection;2;0;1;0
WireConnection;2;1;8;0
WireConnection;21;0;20;1
WireConnection;21;1;22;0
WireConnection;51;0;50;0
WireConnection;51;1;27;0
WireConnection;25;0;20;1
WireConnection;25;1;26;0
WireConnection;16;0;14;0
WireConnection;16;1;15;0
WireConnection;16;2;2;1
WireConnection;18;0;16;0
WireConnection;18;1;17;0
WireConnection;39;0;51;0
WireConnection;38;0;50;0
WireConnection;28;0;21;0
WireConnection;28;1;25;0
WireConnection;41;0;20;1
WireConnection;41;1;38;0
WireConnection;40;0;20;1
WireConnection;40;1;39;0
WireConnection;19;0;18;0
WireConnection;19;1;17;0
WireConnection;29;0;21;0
WireConnection;29;1;28;0
WireConnection;55;0;54;0
WireConnection;55;1;52;0
WireConnection;53;0;55;0
WireConnection;53;1;30;0
WireConnection;53;2;21;0
WireConnection;53;3;58;0
WireConnection;23;0;19;0
WireConnection;23;1;29;0
WireConnection;42;0;40;0
WireConnection;42;1;41;0
WireConnection;57;0;53;0
WireConnection;57;1;56;0
WireConnection;48;0;23;0
WireConnection;48;1;42;0
WireConnection;49;1;42;0
WireConnection;0;0;24;0
WireConnection;0;2;48;0
WireConnection;0;11;57;0
ASEEND*/
//CHKSM=F59DDA30504D0558F1C28107ABAE8E0C4ED480C8