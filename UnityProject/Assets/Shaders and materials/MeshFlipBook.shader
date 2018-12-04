// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Garden/MeshFlipBook"
{
	Properties
	{
		_Cutoff( "Mask Clip Value", Float ) = 0.5
		_FrameCountPercent("FrameCountPercent", Range( 0.3 , 1)) = 1.5
		_TimeScale("TimeScale", Range( 0.8 , 1.5)) = 1
		_Color1("Color 1", Color) = (1,1,1,0)
		_Stepping("Stepping", Range( 0.7 , 2)) = 1.5
		[HDR]_Color2("Color2", Color) = (0.7830863,0.514151,1,0)
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "AlphaTest+0" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows exclude_path:deferred vertex:vertexDataFunc 
		struct Input
		{
			float4 vertexColor : COLOR;
			float2 uv_texcoord;
		};

		uniform float _TimeScale;
		uniform float _FrameCountPercent;
		uniform float _Stepping;
		uniform float4 _Color2;
		uniform float4 _Color1;
		uniform float _Cutoff = 0.5;

		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			float mulTime2 = _Time.y * _TimeScale;
			float temp_output_4_0 = ( fmod( mulTime2 , _FrameCountPercent ) * _Stepping );
			float4 temp_cast_0 = (temp_output_4_0).xxxx;
			float4 temp_cast_1 = (( temp_output_4_0 - 0.1076471 )).xxxx;
			float4 temp_output_10_0 = ( step( v.color , temp_cast_0 ) - step( v.color , temp_cast_1 ) );
			v.vertex.xyz += temp_output_10_0.rgb;
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float mulTime2 = _Time.y * _TimeScale;
			float temp_output_4_0 = ( fmod( mulTime2 , _FrameCountPercent ) * _Stepping );
			float4 temp_cast_0 = (temp_output_4_0).xxxx;
			float4 temp_cast_1 = (( temp_output_4_0 - 0.1076471 )).xxxx;
			float4 temp_output_10_0 = ( step( i.vertexColor , temp_cast_0 ) - step( i.vertexColor , temp_cast_1 ) );
			float2 uv_TexCoord22 = i.uv_texcoord * float2( 1,1.68 );
			float4 lerpResult17 = lerp( _Color2 , _Color1 , float4( uv_TexCoord22, 0.0 , 0.0 ));
			o.Emission = ( temp_output_10_0 * lerpResult17 ).rgb;
			o.Alpha = 1;
			clip( temp_output_10_0.r - _Cutoff );
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=15800
481;201;1272;649;1669.164;1024.497;1.931298;True;False
Node;AmplifyShaderEditor.RangedFloatNode;13;-1103.628,-272.1476;Float;False;Property;_TimeScale;TimeScale;2;0;Create;True;0;0;False;0;1;0.8189508;0.8;1.5;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;3;-1086.397,-168.494;Float;False;Property;_FrameCountPercent;FrameCountPercent;1;0;Create;True;0;0;False;0;1.5;0.967;0.3;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;2;-812.4532,-239.0936;Float;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;12;-811.9649,-109.2055;Float;False;Property;_Stepping;Stepping;4;0;Create;True;0;0;False;0;1.5;1.30818;0.7;2;0;1;FLOAT;0
Node;AmplifyShaderEditor.FmodOpNode;1;-633.5515,-280.6186;Float;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;6;-642.3109,-28.07603;Float;False;Constant;_LightningSpeeds;Lightning Speeds;1;0;Create;True;0;0;False;0;0.1076471;0.08411765;0;0.15;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;4;-512.3278,-274.1186;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.VertexColorNode;9;-938.4633,-529.0403;Float;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleSubtractOpNode;5;-288.3142,-100.916;Float;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;16;-284.4725,-730.8328;Float;False;Property;_Color1;Color 1;3;0;Create;True;0;0;False;0;1,1,1,0;1,1,1,0;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;15;-285.7723,-895.9332;Float;False;Property;_Color2;Color2;5;1;[HDR];Create;True;0;0;False;0;0.7830863,0.514151,1,0;0.5137255,0.7714087,1,0;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;22;344.7277,-816.6316;Float;True;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1.68;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StepOpNode;8;-367.146,-275.8999;Float;False;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.StepOpNode;7;-368.0985,-412.622;Float;False;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;17;76.9276,-828.3321;Float;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;10;-137.8997,-240.8105;Float;False;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;23;-116.7724,-455.2335;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;120.6,-390.2997;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;Garden/MeshFlipBook;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Custom;0.5;True;True;0;True;Opaque;;AlphaTest;ForwardOnly;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;0;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;2;0;13;0
WireConnection;1;0;2;0
WireConnection;1;1;3;0
WireConnection;4;0;1;0
WireConnection;4;1;12;0
WireConnection;5;0;4;0
WireConnection;5;1;6;0
WireConnection;8;0;9;0
WireConnection;8;1;5;0
WireConnection;7;0;9;0
WireConnection;7;1;4;0
WireConnection;17;0;15;0
WireConnection;17;1;16;0
WireConnection;17;2;22;0
WireConnection;10;0;7;0
WireConnection;10;1;8;0
WireConnection;23;0;10;0
WireConnection;23;1;17;0
WireConnection;0;2;23;0
WireConnection;0;10;10;0
WireConnection;0;11;10;0
ASEEND*/
//CHKSM=18C943CC027FFA08E0A40FEF2C5E37ACDF9E5B10