Shader "Hidden/Color Correction Effect" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_RampTex ("Base (RGB)", 2D) = "grayscaleRamp" {}
	}
	SubShader {
		Pass {
			ZClip Off
			ZTest Always
			ZWrite Off
			Cull Off
			GpuProgramID 12324
			Program "vp" {
				SubProgram "d3d9 " {
					"!!vs_3_0
					
					//
					// Generated by Microsoft (R) HLSL Shader Compiler 10.1
					//
					// Parameters:
					//
					//   row_major float4x4 unity_MatrixVP;
					//   row_major float4x4 unity_ObjectToWorld;
					//
					//
					// Registers:
					//
					//   Name                Reg   Size
					//   ------------------- ----- ----
					//   unity_ObjectToWorld c0       4
					//   unity_MatrixVP      c4       4
					//
					
					    vs_3_0
					    def c8, 1, 0, 0, 0
					    dcl_position v0
					    dcl_texcoord v1
					    dcl_position o0
					    dcl_texcoord o1.xy
					    mad r0, v0.xyzx, c8.xxxy, c8.yyyx
					    dp4 r1.x, c0, r0
					    dp4 r1.y, c1, r0
					    dp4 r1.z, c2, r0
					    dp4 r1.w, c3, r0
					    dp4 r2.x, c4, r1
					    dp4 r2.y, c5, r1
					    dp4 r2.z, c6, r1
					    dp4 r2.w, c7, r1
					    mov o1.xy, v1
					    mad o0.xy, r2.w, c255, r2
					    mov o0.zw, r2
					
					// approximately 12 instruction slots used"
				}
				SubProgram "d3d11 " {
					"!!vs_4_0
					
					#version 330
					#extension GL_ARB_explicit_attrib_location : require
					#extension GL_ARB_explicit_uniform_location : require
					
					layout(std140) uniform UnityPerDraw {
						mat4x4 unity_ObjectToWorld;
						vec4 unused_0_1[6];
					};
					layout(std140) uniform UnityPerFrame {
						vec4 unused_1_0[17];
						mat4x4 unity_MatrixVP;
						vec4 unused_1_2[3];
					};
					in  vec4 in_POSITION0;
					in  vec2 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					void main()
					{
					    u_xlat0 = in_POSITION0.yyyy * unity_ObjectToWorld[1];
					    u_xlat0 = unity_ObjectToWorld[0] * in_POSITION0.xxxx + u_xlat0;
					    u_xlat0 = unity_ObjectToWorld[2] * in_POSITION0.zzzz + u_xlat0;
					    u_xlat0 = u_xlat0 + unity_ObjectToWorld[3];
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat1 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = unity_MatrixVP[3] * u_xlat0.wwww + u_xlat1;
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy;
					    return;
					}"
				}
			}
			Program "fp" {
				SubProgram "d3d9 " {
					"!!ps_3_0
					
					//
					// Generated by Microsoft (R) HLSL Shader Compiler 10.1
					//
					// Parameters:
					//
					//   sampler2D _MainTex;
					//   sampler2D _RampTex;
					//
					//
					// Registers:
					//
					//   Name         Reg   Size
					//   ------------ ----- ----
					//   _MainTex     s0       1
					//   _RampTex     s1       1
					//
					
					    ps_3_0
					    dcl_texcoord_pp v0.xy
					    dcl_2d s0
					    dcl_2d s1
					    texld_pp r0, v0, s0
					    texld_pp r1, r0.x, s1
					    mov_pp oC0.x, r1.x
					    texld_pp r1, r0.y, s1
					    mov_pp oC0.y, r1.y
					    texld_pp r1, r0.z, s1
					    mov_pp oC0.w, r0.w
					    mov_pp oC0.z, r1.z
					
					// approximately 8 instruction slots used (4 texture, 4 arithmetic)"
				}
				SubProgram "d3d11 " {
					"!!ps_4_0
					
					#version 330
					#extension GL_ARB_explicit_attrib_location : require
					#extension GL_ARB_explicit_uniform_location : require
					
					uniform  sampler2D _MainTex;
					uniform  sampler2D _RampTex;
					in  vec2 vs_TEXCOORD0;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat10_0;
					vec4 u_xlat10_1;
					void main()
					{
					    u_xlat10_0 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat10_1 = texture(_RampTex, u_xlat10_0.xx);
					    SV_Target0.x = u_xlat10_1.x;
					    u_xlat10_1 = texture(_RampTex, u_xlat10_0.yy);
					    SV_Target0.y = u_xlat10_1.y;
					    u_xlat10_1 = texture(_RampTex, u_xlat10_0.zz);
					    SV_Target0.w = u_xlat10_0.w;
					    SV_Target0.z = u_xlat10_1.z;
					    return;
					}"
				}
			}
		}
	}
}