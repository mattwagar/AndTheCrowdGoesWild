Shader "Custom/Hologram" {
    Properties {
      _RimColor ("Rim Color", Color) = (0,0.5,0.5,0.0)
      _RimPower ("Rim Power", Range(0.5,8.0)) = 3.0
  		_Speed("Speed", Range(0,5)) = 1.0
  		_Alpha("Alpha", Range(0,1)) = 1.0
      _MainTex ("MainTex", 2D) = "white" {}
    }
    SubShader {
      Tags{"Queue" = "Transparent"}

      Pass {
        ZWrite On
        ColorMask 0
        Cull off
      }

      


      CGPROGRAM
      
      #pragma surface surf Lambert alpha:fade

    	uniform float _Speed;
    	uniform float _Alpha;
      sampler2D _MainTex;
      struct Input {
          float3 viewDir;
          float2 uv_MainTex;
      };

      float4 _RimColor;
      float _RimPower;
      
      void surf (Input IN, inout SurfaceOutput o) {
          IN.uv_MainTex -= float2(0, _Time.y * _Speed);
          fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
          half rim = 1 - saturate(dot (normalize(IN.viewDir), o.Normal));
          o.Emission = _RimColor.rgb;
          o.Alpha = (c*_Alpha)+pow (rim, _RimPower);
      }
      ENDCG
    } 
    Fallback "Diffuse"
  }