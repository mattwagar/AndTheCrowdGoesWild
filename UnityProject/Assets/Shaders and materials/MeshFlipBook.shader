// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Garden/MeshFlipBook"
{
	Properties
	{
		_Cutoff( "Mask Clip Value", Float ) = 0.5
		_Float1("Float 1", Range( 0 , 5)) = 0.15
		[HDR]_Color0("Color 0", Color) = (0.7830863,0.514151,1,0)
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
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows vertex:vertexDataFunc 
		struct Input
		{
			float4 vertexColor : COLOR;
			float2 uv_texcoord;
		};

		uniform float _Float1;
		uniform float4 _Color0;
		uniform float _Cutoff = 0.5;

		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			float temp_output_4_0 = ( fmod( _Time.y , 1.5 ) * 1.5 );
			float temp_output_10_0 = ( step( v.color.r , temp_output_4_0 ) - step( v.color.r , ( temp_output_4_0 - _Float1 ) ) );
			float3 temp_cast_0 = (temp_output_10_0).xxx;
			v.vertex.xyz += temp_cast_0;
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float temp_output_4_0 = ( fmod( _Time.y , 1.5 ) * 1.5 );
			float temp_output_10_0 = ( step( i.vertexColor.r , temp_output_4_0 ) - step( i.vertexColor.r , ( temp_output_4_0 - _Float1 ) ) );
			float2 uv_TexCoord22 = i.uv_texcoord * float2( 1,1.68 );
			float4 lerpResult17 = lerp( _Color0 , float4(1,1,1,0) , float4( uv_TexCoord22, 0.0 , 0.0 ));
			o.Emission = ( temp_output_10_0 * lerpResult17 ).rgb;
			o.Alpha = 1;
			clip( temp_output_10_0 - _Cutoff );
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=15800
447;220;1272;631;1828.699;932.8254;1.822162;True;False
Node;AmplifyShaderEditor.RangedFloatNode;13;-939.8275,-326.7475;Float;False;Constant;_Float3;Float 3;1;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;2;-812.4532,-239.0936;Float;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;3;-802.4968,-154.9749;Float;False;Constant;_Float0;Float 0;1;0;Create;True;0;0;False;0;1.5;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;12;-815.8275,-37.7475;Float;False;Constant;_Float2;Float 2;1;0;Create;True;0;0;False;0;1.5;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.FmodOpNode;1;-647.8513,-223.4187;Float;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;4;-527.9276,-224.7186;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;6;-633.845,-5.755424;Float;True;Property;_Float1;Float 1;1;0;Create;True;0;0;False;0;0.15;0.18;0;5;0;1;FLOAT;0
Node;AmplifyShaderEditor.VertexColorNode;9;-938.4633,-529.0403;Float;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleSubtractOpNode;5;-288.3142,-100.916;Float;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StepOpNode;7;-368.0985,-412.622;Float;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StepOpNode;8;-367.146,-275.8999;Float;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;22;344.7277,-816.6316;Float;True;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1.68;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;16;-284.4725,-730.8328;Float;False;Constant;_Color1;Color 1;2;0;Create;True;0;0;False;0;1,1,1,0;0,0,0,0;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;15;-285.7723,-895.9332;Float;False;Property;_Color0;Color 0;2;1;[HDR];Create;True;0;0;False;0;0.7830863,0.514151,1,0;0.7830863,0.514151,1,0;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleSubtractOpNode;10;-137.8997,-240.8105;Float;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;17;76.9276,-828.3321;Float;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.WorldPosInputsNode;31;-896.3284,-692.0801;Float;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.NoiseGeneratorNode;30;-690.0999,-750.2924;Float;True;Simplex3D;1;0;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;32;-495.4525,-569.9893;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;23;-116.7724,-455.2335;Float;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;120.6,-390.2997;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;Garden/MeshFlipBook;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Custom;0.5;True;True;0;True;Opaque;;AlphaTest;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;0;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;2;0;13;0
WireConnection;1;0;2;0
WireConnection;1;1;3;0
WireConnection;4;0;1;0
WireConnection;4;1;12;0
WireConnection;5;0;4;0
WireConnection;5;1;6;0
WireConnection;7;0;9;1
WireConnection;7;1;4;0
WireConnection;8;0;9;1
WireConnection;8;1;5;0
WireConnection;10;0;7;0
WireConnection;10;1;8;0
WireConnection;17;0;15;0
WireConnection;17;1;16;0
WireConnection;17;2;22;0
WireConnection;30;0;31;0
WireConnection;32;0;30;0
WireConnection;23;0;10;0
WireConnection;23;1;17;0
WireConnection;0;2;23;0
WireConnection;0;10;10;0
WireConnection;0;11;10;0
ASEEND*/
//CHKSM=7CABA1AFBFF4A3810967C5E6747025D8908A9A2F