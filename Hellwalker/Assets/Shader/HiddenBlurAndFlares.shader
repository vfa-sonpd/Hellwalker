Shader "Hidden/BlurAndFlares" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "" {}
		_NonBlurredTex ("Base (RGB)", 2D) = "" {}
	}
	SubShader {
		Pass {
			ZClip Off
			ZTest Always
			ZWrite Off
			Cull Off
			GpuProgramID 5053
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
					//
					//
					// Registers:
					//
					//   Name         Reg   Size
					//   ------------ ----- ----
					//   _MainTex     s0       1
					//
					
					    ps_3_0
					    def c0, 0.219999999, 0.707000017, 0.0710000023, 1.5
					    dcl_texcoord_pp v0.xy
					    dcl_2d s0
					    texld_pp r0, v0, s0
					    dp3_pp r1.x, r0, c0
					    add_pp r1.x, r1.x, c0.w
					    rcp r1.x, r1.x
					    mul_pp oC0, r0, r1.x
					
					// approximately 5 instruction slots used (1 texture, 4 arithmetic)"
				}
				SubProgram "d3d11 " {
					"!!ps_4_0
					
					#version 330
					#extension GL_ARB_explicit_attrib_location : require
					#extension GL_ARB_explicit_uniform_location : require
					
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat10_0;
					float u_xlat16_1;
					void main()
					{
					    u_xlat10_0 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat16_1 = dot(u_xlat10_0.xyz, vec3(0.219999999, 0.707000017, 0.0710000023));
					    u_xlat16_1 = u_xlat16_1 + 1.5;
					    SV_Target0 = u_xlat10_0 / vec4(u_xlat16_1);
					    return;
					}"
				}
			}
		}
		Pass {
			ZClip Off
			ZTest Always
			ZWrite Off
			Cull Off
			GpuProgramID 97131
			Program "vp" {
				SubProgram "d3d9 " {
					"!!vs_3_0
					
					//
					// Generated by Microsoft (R) HLSL Shader Compiler 10.1
					//
					// Parameters:
					//
					//   float4 _Offsets;
					//   float _StretchWidth;
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
					//   _Offsets            c8       1
					//   _StretchWidth       c9       1
					//
					
					    vs_3_0
					    def c10, 1, 0, 4, 6
					    dcl_position v0
					    dcl_texcoord v1
					    dcl_position o0
					    dcl_texcoord o1.xy
					    dcl_texcoord1 o2.xy
					    dcl_texcoord2 o3.xy
					    dcl_texcoord3 o4.xy
					    dcl_texcoord4 o5.xy
					    dcl_texcoord5 o6.xy
					    dcl_texcoord6 o7.xy
					    mad r0, v0.xyzx, c10.xxxy, c10.yyyx
					    dp4 r1.x, c0, r0
					    dp4 r1.y, c1, r0
					    dp4 r1.z, c2, r0
					    dp4 r1.w, c3, r0
					    dp4 r2.x, c4, r1
					    dp4 r2.y, c5, r1
					    dp4 r2.z, c6, r1
					    dp4 r2.w, c7, r1
					    add r0.x, c9.x, c9.x
					    mad o2.xy, r0.x, c8, v1
					    mad o3.xy, r0.x, -c8, v1
					    mov r0.zw, c10
					    mul r0, r0.zzww, c9.x
					    mad o4.xy, r0, c8, v1
					    mad o5.xy, r0, -c8, v1
					    mad o6.xy, r0.zwzw, c8, v1
					    mad o7.xy, r0.zwzw, -c8, v1
					    mov o1.xy, v1
					    mad o0.xy, r2.w, c255, r2
					    mov o0.zw, r2
					
					// approximately 21 instruction slots used"
				}
				SubProgram "d3d11 " {
					"!!vs_4_0
					
					#version 330
					#extension GL_ARB_explicit_attrib_location : require
					#extension GL_ARB_explicit_uniform_location : require
					
					layout(std140) uniform VGlobals {
						vec4 unused_0_0[2];
						vec4 _Offsets;
						vec4 unused_0_2;
						float _StretchWidth;
						vec4 unused_0_4;
					};
					layout(std140) uniform UnityPerDraw {
						mat4x4 unity_ObjectToWorld;
						vec4 unused_1_1[6];
					};
					layout(std140) uniform UnityPerFrame {
						vec4 unused_2_0[17];
						mat4x4 unity_MatrixVP;
						vec4 unused_2_2[3];
					};
					in  vec4 in_POSITION0;
					in  vec2 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec2 vs_TEXCOORD2;
					out vec2 vs_TEXCOORD3;
					out vec2 vs_TEXCOORD4;
					out vec2 vs_TEXCOORD5;
					out vec2 vs_TEXCOORD6;
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
					    u_xlat0.x = _StretchWidth + _StretchWidth;
					    vs_TEXCOORD1.xy = u_xlat0.xx * _Offsets.xy + in_TEXCOORD0.xy;
					    vs_TEXCOORD2.xy = (-u_xlat0.xx) * _Offsets.xy + in_TEXCOORD0.xy;
					    u_xlat0 = vec4(_StretchWidth) * vec4(4.0, 4.0, 6.0, 6.0);
					    vs_TEXCOORD3.xy = u_xlat0.xy * _Offsets.xy + in_TEXCOORD0.xy;
					    vs_TEXCOORD4.xy = (-u_xlat0.xy) * _Offsets.xy + in_TEXCOORD0.xy;
					    vs_TEXCOORD5.xy = u_xlat0.zw * _Offsets.xy + in_TEXCOORD0.xy;
					    vs_TEXCOORD6.xy = (-u_xlat0.zw) * _Offsets.xy + in_TEXCOORD0.xy;
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
					//
					//
					// Registers:
					//
					//   Name         Reg   Size
					//   ------------ ----- ----
					//   _MainTex     s0       1
					//
					
					    ps_3_0
					    dcl_texcoord_pp v0.xy
					    dcl_texcoord1_pp v1.xy
					    dcl_texcoord2_pp v2.xy
					    dcl_texcoord3_pp v3.xy
					    dcl_texcoord4_pp v4.xy
					    dcl_texcoord5_pp v5.xy
					    dcl_texcoord6_pp v6.xy
					    dcl_2d s0
					    texld_pp r0, v0, s0
					    texld_pp r1, v1, s0
					    max_pp r2, r0, r1
					    texld_pp r0, v2, s0
					    max_pp r1, r2, r0
					    texld_pp r0, v3, s0
					    max_pp r2, r1, r0
					    texld_pp r0, v4, s0
					    max_pp r1, r2, r0
					    texld_pp r0, v5, s0
					    max_pp r2, r1, r0
					    texld_pp r0, v6, s0
					    max_pp oC0, r2, r0
					
					// approximately 13 instruction slots used (7 texture, 6 arithmetic)"
				}
				SubProgram "d3d11 " {
					"!!ps_4_0
					
					#version 330
					#extension GL_ARB_explicit_attrib_location : require
					#extension GL_ARB_explicit_uniform_location : require
					
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec2 vs_TEXCOORD2;
					in  vec2 vs_TEXCOORD3;
					in  vec2 vs_TEXCOORD4;
					in  vec2 vs_TEXCOORD5;
					in  vec2 vs_TEXCOORD6;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat16_0;
					vec4 u_xlat10_0;
					vec4 u_xlat10_1;
					void main()
					{
					    u_xlat10_0 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat10_1 = texture(_MainTex, vs_TEXCOORD1.xy);
					    u_xlat16_0 = max(u_xlat10_0, u_xlat10_1);
					    u_xlat10_1 = texture(_MainTex, vs_TEXCOORD2.xy);
					    u_xlat16_0 = max(u_xlat16_0, u_xlat10_1);
					    u_xlat10_1 = texture(_MainTex, vs_TEXCOORD3.xy);
					    u_xlat16_0 = max(u_xlat16_0, u_xlat10_1);
					    u_xlat10_1 = texture(_MainTex, vs_TEXCOORD4.xy);
					    u_xlat16_0 = max(u_xlat16_0, u_xlat10_1);
					    u_xlat10_1 = texture(_MainTex, vs_TEXCOORD5.xy);
					    u_xlat16_0 = max(u_xlat16_0, u_xlat10_1);
					    u_xlat10_1 = texture(_MainTex, vs_TEXCOORD6.xy);
					    SV_Target0 = max(u_xlat16_0, u_xlat10_1);
					    return;
					}"
				}
			}
		}
		Pass {
			ZClip Off
			ZTest Always
			ZWrite Off
			Cull Off
			GpuProgramID 156922
			Program "vp" {
				SubProgram "d3d9 " {
					"!!vs_3_0
					
					//
					// Generated by Microsoft (R) HLSL Shader Compiler 10.1
					//
					// Parameters:
					//
					//   float4 _MainTex_TexelSize;
					//   float4 _Offsets;
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
					//   _Offsets            c8       1
					//   _MainTex_TexelSize  c9       1
					//
					
					    vs_3_0
					    def c10, 1, 0, 0.5, 1.5
					    def c11, 2.5, 0, 0, 0
					    dcl_position v0
					    dcl_texcoord v1
					    dcl_position o0
					    dcl_texcoord o1.xy
					    dcl_texcoord1 o2.xy
					    dcl_texcoord2 o3.xy
					    dcl_texcoord3 o4.xy
					    dcl_texcoord4 o5.xy
					    dcl_texcoord5 o6.xy
					    dcl_texcoord6 o7.xy
					    mad r0, v0.xyzx, c10.xxxy, c10.yyyx
					    dp4 r1.x, c0, r0
					    dp4 r1.y, c1, r0
					    dp4 r1.z, c2, r0
					    dp4 r1.w, c3, r0
					    dp4 r2.x, c4, r1
					    dp4 r2.y, c5, r1
					    dp4 r2.z, c6, r1
					    dp4 r2.w, c7, r1
					    mov r0.xy, c8
					    mul r0.xy, r0, c9
					    mad o2.xy, r0, c10.z, v1
					    mad o3.xy, r0, -c10.z, v1
					    mad o4.xy, r0, c10.w, v1
					    mad o5.xy, r0, -c10.w, v1
					    mad o6.xy, r0, c11.x, v1
					    mad o7.xy, r0, -c11.x, v1
					    mov o1.xy, v1
					    mad o0.xy, r2.w, c255, r2
					    mov o0.zw, r2
					
					// approximately 20 instruction slots used"
				}
				SubProgram "d3d11 " {
					"!!vs_4_0
					
					#version 330
					#extension GL_ARB_explicit_attrib_location : require
					#extension GL_ARB_explicit_uniform_location : require
					
					layout(std140) uniform VGlobals {
						vec4 unused_0_0[2];
						vec4 _Offsets;
						vec4 unused_0_2[2];
						vec4 _MainTex_TexelSize;
					};
					layout(std140) uniform UnityPerDraw {
						mat4x4 unity_ObjectToWorld;
						vec4 unused_1_1[6];
					};
					layout(std140) uniform UnityPerFrame {
						vec4 unused_2_0[17];
						mat4x4 unity_MatrixVP;
						vec4 unused_2_2[3];
					};
					in  vec4 in_POSITION0;
					in  vec2 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec2 vs_TEXCOORD2;
					out vec2 vs_TEXCOORD3;
					out vec2 vs_TEXCOORD4;
					out vec2 vs_TEXCOORD5;
					out vec2 vs_TEXCOORD6;
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
					    u_xlat0.xy = _Offsets.xy * _MainTex_TexelSize.xy;
					    vs_TEXCOORD1.xy = u_xlat0.xy * vec2(0.5, 0.5) + in_TEXCOORD0.xy;
					    vs_TEXCOORD2.xy = (-u_xlat0.xy) * vec2(0.5, 0.5) + in_TEXCOORD0.xy;
					    vs_TEXCOORD3.xy = u_xlat0.xy * vec2(1.5, 1.5) + in_TEXCOORD0.xy;
					    vs_TEXCOORD4.xy = (-u_xlat0.xy) * vec2(1.5, 1.5) + in_TEXCOORD0.xy;
					    vs_TEXCOORD5.xy = u_xlat0.xy * vec2(2.5, 2.5) + in_TEXCOORD0.xy;
					    vs_TEXCOORD6.xy = (-u_xlat0.xy) * vec2(2.5, 2.5) + in_TEXCOORD0.xy;
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
					//   float _Saturation;
					//   float2 _Threshhold;
					//   float4 _TintColor;
					//
					//
					// Registers:
					//
					//   Name         Reg   Size
					//   ------------ ----- ----
					//   _TintColor   c0       1
					//   _Threshhold  c1       1
					//   _Saturation  c2       1
					//   _MainTex     s0       1
					//
					
					    ps_3_0
					    def c3, 0.142857149, 0, 0, 0
					    def c4, 0.219999999, 0.707000017, 0.0710000023, 0
					    dcl_texcoord_pp v0.xy
					    dcl_texcoord1_pp v1.xy
					    dcl_texcoord2_pp v2.xy
					    dcl_texcoord3_pp v3.xy
					    dcl_texcoord4_pp v4.xy
					    dcl_texcoord5_pp v5.xy
					    dcl_texcoord6_pp v6.xy
					    dcl_2d s0
					    texld_pp r0, v0, s0
					    texld r1, v1, s0
					    add_pp r0, r0, r1
					    texld r1, v2, s0
					    add_pp r0, r0, r1
					    texld r1, v3, s0
					    add_pp r0, r0, r1
					    texld r1, v4, s0
					    add_pp r0, r0, r1
					    texld r1, v5, s0
					    add_pp r0, r0, r1
					    texld r1, v6, s0
					    add_pp r0, r0, r1
					    mov r1.x, c3.x
					    mad_pp r0, r0, r1.x, -c1.x
					    max_pp r1, r0, c3.y
					    dp3_pp r0.x, r1, c4
					    lrp_pp r2.xyz, c2.x, r1, r0.x
					    mov_pp oC0.w, r1.w
					    mul_pp oC0.xyz, r2, c0
					
					// approximately 20 instruction slots used (7 texture, 13 arithmetic)"
				}
				SubProgram "d3d11 " {
					"!!ps_4_0
					
					#version 330
					#extension GL_ARB_explicit_attrib_location : require
					#extension GL_ARB_explicit_uniform_location : require
					
					layout(std140) uniform PGlobals {
						vec4 unused_0_0[3];
						vec4 _TintColor;
						vec2 _Threshhold;
						float _Saturation;
						vec4 unused_0_4;
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec2 vs_TEXCOORD2;
					in  vec2 vs_TEXCOORD3;
					in  vec2 vs_TEXCOORD4;
					in  vec2 vs_TEXCOORD5;
					in  vec2 vs_TEXCOORD6;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat16_0;
					vec4 u_xlat10_0;
					float u_xlat1;
					vec4 u_xlat10_1;
					void main()
					{
					    u_xlat10_0 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat10_1 = texture(_MainTex, vs_TEXCOORD1.xy);
					    u_xlat16_0 = u_xlat10_0 + u_xlat10_1;
					    u_xlat10_1 = texture(_MainTex, vs_TEXCOORD2.xy);
					    u_xlat16_0 = u_xlat16_0 + u_xlat10_1;
					    u_xlat10_1 = texture(_MainTex, vs_TEXCOORD3.xy);
					    u_xlat16_0 = u_xlat16_0 + u_xlat10_1;
					    u_xlat10_1 = texture(_MainTex, vs_TEXCOORD4.xy);
					    u_xlat16_0 = u_xlat16_0 + u_xlat10_1;
					    u_xlat10_1 = texture(_MainTex, vs_TEXCOORD5.xy);
					    u_xlat16_0 = u_xlat16_0 + u_xlat10_1;
					    u_xlat10_1 = texture(_MainTex, vs_TEXCOORD6.xy);
					    u_xlat16_0 = u_xlat16_0 + u_xlat10_1;
					    u_xlat0 = u_xlat16_0 * vec4(0.142857149, 0.142857149, 0.142857149, 0.142857149) + (-_Threshhold.xxyx.yyyy);
					    u_xlat0 = max(u_xlat0, vec4(0.0, 0.0, 0.0, 0.0));
					    u_xlat1 = dot(u_xlat0.xyz, vec3(0.219999999, 0.707000017, 0.0710000023));
					    u_xlat0.xyz = u_xlat0.xyz + (-vec3(u_xlat1));
					    SV_Target0.w = u_xlat0.w;
					    u_xlat0.xyz = vec3(vec3(_Saturation, _Saturation, _Saturation)) * u_xlat0.xyz + vec3(u_xlat1);
					    SV_Target0.xyz = u_xlat0.xyz * _TintColor.xyz;
					    return;
					}"
				}
			}
		}
		Pass {
			ZClip Off
			ZTest Always
			ZWrite Off
			Cull Off
			GpuProgramID 217664
			Program "vp" {
				SubProgram "d3d9 " {
					"!!vs_3_0
					
					//
					// Generated by Microsoft (R) HLSL Shader Compiler 10.1
					//
					// Parameters:
					//
					//   float4 _MainTex_TexelSize;
					//   float4 _Offsets;
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
					//   _Offsets            c8       1
					//   _MainTex_TexelSize  c9       1
					//
					
					    vs_3_0
					    def c10, 1, 0, 0.5, 1.5
					    def c11, 2.5, 0, 0, 0
					    dcl_position v0
					    dcl_texcoord v1
					    dcl_position o0
					    dcl_texcoord o1.xy
					    dcl_texcoord1 o2.xy
					    dcl_texcoord2 o3.xy
					    dcl_texcoord3 o4.xy
					    dcl_texcoord4 o5.xy
					    dcl_texcoord5 o6.xy
					    dcl_texcoord6 o7.xy
					    mad r0, v0.xyzx, c10.xxxy, c10.yyyx
					    dp4 r1.x, c0, r0
					    dp4 r1.y, c1, r0
					    dp4 r1.z, c2, r0
					    dp4 r1.w, c3, r0
					    dp4 r2.x, c4, r1
					    dp4 r2.y, c5, r1
					    dp4 r2.z, c6, r1
					    dp4 r2.w, c7, r1
					    mov r0.xy, c8
					    mul r0.xy, r0, c9
					    mad o2.xy, r0, c10.z, v1
					    mad o3.xy, r0, -c10.z, v1
					    mad o4.xy, r0, c10.w, v1
					    mad o5.xy, r0, -c10.w, v1
					    mad o6.xy, r0, c11.x, v1
					    mad o7.xy, r0, -c11.x, v1
					    mov o1.xy, v1
					    mad o0.xy, r2.w, c255, r2
					    mov o0.zw, r2
					
					// approximately 20 instruction slots used"
				}
				SubProgram "d3d11 " {
					"!!vs_4_0
					
					#version 330
					#extension GL_ARB_explicit_attrib_location : require
					#extension GL_ARB_explicit_uniform_location : require
					
					layout(std140) uniform VGlobals {
						vec4 unused_0_0[2];
						vec4 _Offsets;
						vec4 unused_0_2[2];
						vec4 _MainTex_TexelSize;
					};
					layout(std140) uniform UnityPerDraw {
						mat4x4 unity_ObjectToWorld;
						vec4 unused_1_1[6];
					};
					layout(std140) uniform UnityPerFrame {
						vec4 unused_2_0[17];
						mat4x4 unity_MatrixVP;
						vec4 unused_2_2[3];
					};
					in  vec4 in_POSITION0;
					in  vec2 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec2 vs_TEXCOORD2;
					out vec2 vs_TEXCOORD3;
					out vec2 vs_TEXCOORD4;
					out vec2 vs_TEXCOORD5;
					out vec2 vs_TEXCOORD6;
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
					    u_xlat0.xy = _Offsets.xy * _MainTex_TexelSize.xy;
					    vs_TEXCOORD1.xy = u_xlat0.xy * vec2(0.5, 0.5) + in_TEXCOORD0.xy;
					    vs_TEXCOORD2.xy = (-u_xlat0.xy) * vec2(0.5, 0.5) + in_TEXCOORD0.xy;
					    vs_TEXCOORD3.xy = u_xlat0.xy * vec2(1.5, 1.5) + in_TEXCOORD0.xy;
					    vs_TEXCOORD4.xy = (-u_xlat0.xy) * vec2(1.5, 1.5) + in_TEXCOORD0.xy;
					    vs_TEXCOORD5.xy = u_xlat0.xy * vec2(2.5, 2.5) + in_TEXCOORD0.xy;
					    vs_TEXCOORD6.xy = (-u_xlat0.xy) * vec2(2.5, 2.5) + in_TEXCOORD0.xy;
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
					//
					//
					// Registers:
					//
					//   Name         Reg   Size
					//   ------------ ----- ----
					//   _MainTex     s0       1
					//
					
					    ps_3_0
					    def c0, 0.219999999, 0.707000017, 0.0710000023, 7.5
					    dcl_texcoord_pp v0.xy
					    dcl_texcoord1_pp v1.xy
					    dcl_texcoord2_pp v2.xy
					    dcl_texcoord3_pp v3.xy
					    dcl_texcoord4_pp v4.xy
					    dcl_texcoord5_pp v5.xy
					    dcl_texcoord6_pp v6.xy
					    dcl_2d s0
					    texld_pp r0, v0, s0
					    texld r1, v1, s0
					    add_pp r0, r0, r1
					    texld r1, v2, s0
					    add_pp r0, r0, r1
					    texld r1, v3, s0
					    add_pp r0, r0, r1
					    texld r1, v4, s0
					    add_pp r0, r0, r1
					    texld r1, v5, s0
					    add_pp r0, r0, r1
					    texld r1, v6, s0
					    add_pp r0, r0, r1
					    dp3_pp r1.x, r0, c0
					    add_pp r1.x, r1.x, c0.w
					    rcp r1.x, r1.x
					    mul_pp oC0, r0, r1.x
					
					// approximately 17 instruction slots used (7 texture, 10 arithmetic)"
				}
				SubProgram "d3d11 " {
					"!!ps_4_0
					
					#version 330
					#extension GL_ARB_explicit_attrib_location : require
					#extension GL_ARB_explicit_uniform_location : require
					
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec2 vs_TEXCOORD2;
					in  vec2 vs_TEXCOORD3;
					in  vec2 vs_TEXCOORD4;
					in  vec2 vs_TEXCOORD5;
					in  vec2 vs_TEXCOORD6;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat16_0;
					vec4 u_xlat10_0;
					float u_xlat16_1;
					vec4 u_xlat10_1;
					void main()
					{
					    u_xlat10_0 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat10_1 = texture(_MainTex, vs_TEXCOORD1.xy);
					    u_xlat16_0 = u_xlat10_0 + u_xlat10_1;
					    u_xlat10_1 = texture(_MainTex, vs_TEXCOORD2.xy);
					    u_xlat16_0 = u_xlat16_0 + u_xlat10_1;
					    u_xlat10_1 = texture(_MainTex, vs_TEXCOORD3.xy);
					    u_xlat16_0 = u_xlat16_0 + u_xlat10_1;
					    u_xlat10_1 = texture(_MainTex, vs_TEXCOORD4.xy);
					    u_xlat16_0 = u_xlat16_0 + u_xlat10_1;
					    u_xlat10_1 = texture(_MainTex, vs_TEXCOORD5.xy);
					    u_xlat16_0 = u_xlat16_0 + u_xlat10_1;
					    u_xlat10_1 = texture(_MainTex, vs_TEXCOORD6.xy);
					    u_xlat16_0 = u_xlat16_0 + u_xlat10_1;
					    u_xlat16_1 = dot(u_xlat16_0.xyz, vec3(0.219999999, 0.707000017, 0.0710000023));
					    u_xlat16_1 = u_xlat16_1 + 7.5;
					    SV_Target0 = u_xlat16_0 / vec4(u_xlat16_1);
					    return;
					}"
				}
			}
		}
		Pass {
			ZClip Off
			ZTest Always
			ZWrite Off
			Cull Off
			GpuProgramID 281245
			Program "vp" {
				SubProgram "d3d9 " {
					"!!vs_3_0
					
					//
					// Generated by Microsoft (R) HLSL Shader Compiler 10.1
					//
					// Parameters:
					//
					//   float4 _Offsets;
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
					//   _Offsets            c8       1
					//
					
					    vs_3_0
					    def c9, 1, 0, -1, 0
					    def c10, 2, -2, 3, -3
					    def c11, 5, -5, 0, 0
					    dcl_position v0
					    dcl_texcoord v1
					    dcl_position o0
					    dcl_texcoord o1.xy
					    dcl_texcoord1 o2
					    dcl_texcoord2 o3
					    dcl_texcoord3 o4
					    dcl_texcoord4 o5
					    mad r0, v0.xyzx, c9.xxxy, c9.yyyx
					    dp4 r1.x, c0, r0
					    dp4 r1.y, c1, r0
					    dp4 r1.z, c2, r0
					    dp4 r1.w, c3, r0
					    dp4 r2.x, c4, r1
					    dp4 r2.y, c5, r1
					    dp4 r2.z, c6, r1
					    dp4 r2.w, c7, r1
					    mov r0.xy, c8
					    mad o2, r0.xyxy, c9.xxzz, v1.xyxy
					    mad o3, r0.xyxy, c10.xxyy, v1.xyxy
					    mad o4, r0.xyxy, c10.zzww, v1.xyxy
					    mad o5, r0.xyxy, c11.xxyy, v1.xyxy
					    mov o1.xy, v1
					    mad o0.xy, r2.w, c255, r2
					    mov o0.zw, r2
					
					// approximately 17 instruction slots used"
				}
				SubProgram "d3d11 " {
					"!!vs_4_0
					
					#version 330
					#extension GL_ARB_explicit_attrib_location : require
					#extension GL_ARB_explicit_uniform_location : require
					
					layout(std140) uniform VGlobals {
						vec4 unused_0_0[2];
						vec4 _Offsets;
						vec4 unused_0_2[3];
					};
					layout(std140) uniform UnityPerDraw {
						mat4x4 unity_ObjectToWorld;
						vec4 unused_1_1[6];
					};
					layout(std140) uniform UnityPerFrame {
						vec4 unused_2_0[17];
						mat4x4 unity_MatrixVP;
						vec4 unused_2_2[3];
					};
					in  vec4 in_POSITION0;
					in  vec2 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					out vec4 vs_TEXCOORD1;
					out vec4 vs_TEXCOORD2;
					out vec4 vs_TEXCOORD3;
					out vec4 vs_TEXCOORD4;
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
					    vs_TEXCOORD1 = _Offsets.xyxy * vec4(1.0, 1.0, -1.0, -1.0) + in_TEXCOORD0.xyxy;
					    vs_TEXCOORD2 = _Offsets.xyxy * vec4(2.0, 2.0, -2.0, -2.0) + in_TEXCOORD0.xyxy;
					    vs_TEXCOORD3 = _Offsets.xyxy * vec4(3.0, 3.0, -3.0, -3.0) + in_TEXCOORD0.xyxy;
					    vs_TEXCOORD4 = _Offsets.xyxy * vec4(5.0, 5.0, -5.0, -5.0) + in_TEXCOORD0.xyxy;
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
					//
					//
					// Registers:
					//
					//   Name         Reg   Size
					//   ------------ ----- ----
					//   _MainTex     s0       1
					//
					
					    ps_3_0
					    def c0, 0.0524999984, 0, 0, 0
					    def c1, 0.150000006, 0.224999994, 0.109999999, 0.075000003
					    dcl_texcoord_pp v0.xy
					    dcl_texcoord1_pp v1
					    dcl_texcoord2_pp v2
					    dcl_texcoord3_pp v3
					    dcl_texcoord4_pp v4
					    dcl_2d s0
					    texld r0, v1, s0
					    mul r0, r0, c1.x
					    texld r1, v0, s0
					    mad_pp r0, r1, c1.y, r0
					    texld r1, v1.zwzw, s0
					    mad_pp r0, r1, c1.x, r0
					    texld r1, v2, s0
					    mad_pp r0, r1, c1.z, r0
					    texld r1, v2.zwzw, s0
					    mad_pp r0, r1, c1.z, r0
					    texld r1, v3, s0
					    mad_pp r0, r1, c1.w, r0
					    texld r1, v3.zwzw, s0
					    mad_pp r0, r1, c1.w, r0
					    texld r1, v4, s0
					    mad_pp r0, r1, c0.x, r0
					    texld r1, v4.zwzw, s0
					    mad_pp oC0, r1, c0.x, r0
					
					// approximately 18 instruction slots used (9 texture, 9 arithmetic)"
				}
				SubProgram "d3d11 " {
					"!!ps_4_0
					
					#version 330
					#extension GL_ARB_explicit_attrib_location : require
					#extension GL_ARB_explicit_uniform_location : require
					
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec4 vs_TEXCOORD1;
					in  vec4 vs_TEXCOORD2;
					in  vec4 vs_TEXCOORD3;
					in  vec4 vs_TEXCOORD4;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat16_0;
					vec4 u_xlat10_0;
					vec4 u_xlat10_1;
					void main()
					{
					    u_xlat10_0 = texture(_MainTex, vs_TEXCOORD1.xy);
					    u_xlat16_0 = u_xlat10_0 * vec4(0.150000006, 0.150000006, 0.150000006, 0.150000006);
					    u_xlat10_1 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat16_0 = u_xlat10_1 * vec4(0.224999994, 0.224999994, 0.224999994, 0.224999994) + u_xlat16_0;
					    u_xlat10_1 = texture(_MainTex, vs_TEXCOORD1.zw);
					    u_xlat16_0 = u_xlat10_1 * vec4(0.150000006, 0.150000006, 0.150000006, 0.150000006) + u_xlat16_0;
					    u_xlat10_1 = texture(_MainTex, vs_TEXCOORD2.xy);
					    u_xlat16_0 = u_xlat10_1 * vec4(0.109999999, 0.109999999, 0.109999999, 0.109999999) + u_xlat16_0;
					    u_xlat10_1 = texture(_MainTex, vs_TEXCOORD2.zw);
					    u_xlat16_0 = u_xlat10_1 * vec4(0.109999999, 0.109999999, 0.109999999, 0.109999999) + u_xlat16_0;
					    u_xlat10_1 = texture(_MainTex, vs_TEXCOORD3.xy);
					    u_xlat16_0 = u_xlat10_1 * vec4(0.075000003, 0.075000003, 0.075000003, 0.075000003) + u_xlat16_0;
					    u_xlat10_1 = texture(_MainTex, vs_TEXCOORD3.zw);
					    u_xlat16_0 = u_xlat10_1 * vec4(0.075000003, 0.075000003, 0.075000003, 0.075000003) + u_xlat16_0;
					    u_xlat10_1 = texture(_MainTex, vs_TEXCOORD4.xy);
					    u_xlat16_0 = u_xlat10_1 * vec4(0.0524999984, 0.0524999984, 0.0524999984, 0.0524999984) + u_xlat16_0;
					    u_xlat10_1 = texture(_MainTex, vs_TEXCOORD4.zw);
					    SV_Target0 = u_xlat10_1 * vec4(0.0524999984, 0.0524999984, 0.0524999984, 0.0524999984) + u_xlat16_0;
					    return;
					}"
				}
			}
		}
	}
}