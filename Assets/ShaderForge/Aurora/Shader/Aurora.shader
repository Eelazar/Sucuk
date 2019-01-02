// Shader created with Shader Forge v1.38 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:2,bsrc:0,bdst:0,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:8661,x:34019,y:32179,varname:node_8661,prsc:2|emission-9389-OUT,voffset-2021-OUT;n:type:ShaderForge.SFN_Tex2dAsset,id:3785,x:31433,y:32678,ptovrint:False,ptlb:MIX,ptin:_MIX,varname:node_3785,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:6051eb752d427574db3407e41fd76073,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:595,x:32116,y:33112,varname:node_595,prsc:2,tex:6051eb752d427574db3407e41fd76073,ntxv:0,isnm:False|UVIN-652-OUT,TEX-3785-TEX;n:type:ShaderForge.SFN_Multiply,id:3351,x:32427,y:32977,varname:node_3351,prsc:2|A-595-R,B-4139-OUT;n:type:ShaderForge.SFN_ValueProperty,id:4139,x:32427,y:33126,ptovrint:False,ptlb:VO_Strenght,ptin:_VO_Strenght,varname:node_4139,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.1;n:type:ShaderForge.SFN_Append,id:2021,x:32716,y:32820,varname:node_2021,prsc:2|A-4205-OUT,B-3351-OUT;n:type:ShaderForge.SFN_ValueProperty,id:4205,x:32701,y:32966,ptovrint:False,ptlb:,ptin:_,varname:node_4205,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_TexCoord,id:4578,x:31433,y:32512,varname:node_4578,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_ValueProperty,id:9235,x:31249,y:33097,ptovrint:False,ptlb:VO_SPD_U,ptin:_VO_SPD_U,varname:node_9235,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.1;n:type:ShaderForge.SFN_ValueProperty,id:4893,x:31249,y:33187,ptovrint:False,ptlb:VO_SPD_V,ptin:_VO_SPD_V,varname:node_4893,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_Append,id:6708,x:31429,y:33053,varname:node_6708,prsc:2|A-9235-OUT,B-4893-OUT;n:type:ShaderForge.SFN_Time,id:8625,x:31433,y:32839,varname:node_8625,prsc:2;n:type:ShaderForge.SFN_Multiply,id:8538,x:31613,y:33086,varname:node_8538,prsc:2|A-6708-OUT,B-8625-T;n:type:ShaderForge.SFN_Add,id:4879,x:31802,y:33086,varname:node_4879,prsc:2|A-8538-OUT,B-4578-UVOUT;n:type:ShaderForge.SFN_Tex2d,id:8563,x:31961,y:32803,varname:node_8563,prsc:2,tex:6051eb752d427574db3407e41fd76073,ntxv:0,isnm:False|TEX-3785-TEX;n:type:ShaderForge.SFN_ValueProperty,id:3347,x:31557,y:32338,ptovrint:False,ptlb:Vertiefungen_SPD_U,ptin:_Vertiefungen_SPD_U,varname:_VO_SPD_U_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.1;n:type:ShaderForge.SFN_ValueProperty,id:522,x:31557,y:32428,ptovrint:False,ptlb:Vertiefungen_SPD_V,ptin:_Vertiefungen_SPD_V,varname:_VO_SPD_V_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_Append,id:4598,x:31737,y:32294,varname:node_4598,prsc:2|A-3347-OUT,B-522-OUT;n:type:ShaderForge.SFN_Multiply,id:9532,x:31921,y:32327,varname:node_9532,prsc:2|A-4598-OUT,B-8625-T;n:type:ShaderForge.SFN_Add,id:2079,x:32114,y:32327,varname:node_2079,prsc:2|A-9532-OUT,B-4578-UVOUT;n:type:ShaderForge.SFN_Tex2d,id:92,x:31961,y:32652,varname:node_92,prsc:2,tex:6051eb752d427574db3407e41fd76073,ntxv:0,isnm:False|UVIN-2095-OUT,TEX-3785-TEX;n:type:ShaderForge.SFN_Add,id:7679,x:32206,y:32581,varname:node_7679,prsc:2|A-4684-OUT,B-92-B;n:type:ShaderForge.SFN_ValueProperty,id:4684,x:32206,y:32744,ptovrint:False,ptlb:Vertiefungen_Wert,ptin:_Vertiefungen_Wert,varname:node_4684,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.5;n:type:ShaderForge.SFN_ValueProperty,id:7215,x:31556,y:32007,ptovrint:False,ptlb:Lines_SPD_U,ptin:_Lines_SPD_U,varname:_Vertiefungen_SPD_U_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.1;n:type:ShaderForge.SFN_ValueProperty,id:1901,x:31556,y:32097,ptovrint:False,ptlb:Lines_SPD_V,ptin:_Lines_SPD_V,varname:_Vertiefungen_SPD_V_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_Append,id:2164,x:31736,y:31963,varname:node_2164,prsc:2|A-7215-OUT,B-1901-OUT;n:type:ShaderForge.SFN_Multiply,id:4365,x:31934,y:31996,varname:node_4365,prsc:2|A-2164-OUT,B-8625-T;n:type:ShaderForge.SFN_Add,id:1081,x:32184,y:31996,varname:node_1081,prsc:2|A-4365-OUT,B-4578-UVOUT;n:type:ShaderForge.SFN_Tex2d,id:698,x:32552,y:32131,varname:node_698,prsc:2,tex:6051eb752d427574db3407e41fd76073,ntxv:0,isnm:False|UVIN-1453-OUT,TEX-3785-TEX;n:type:ShaderForge.SFN_Add,id:6952,x:32467,y:32432,varname:node_6952,prsc:2|A-4263-OUT,B-7679-OUT;n:type:ShaderForge.SFN_Multiply,id:8341,x:33173,y:32460,varname:node_8341,prsc:2|A-2472-OUT,B-8563-G;n:type:ShaderForge.SFN_Multiply,id:4263,x:32874,y:32651,varname:node_4263,prsc:2|A-8801-OUT,B-698-A;n:type:ShaderForge.SFN_ValueProperty,id:8801,x:32655,y:32493,ptovrint:False,ptlb:Line_Strength,ptin:_Line_Strength,varname:node_8801,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:2;n:type:ShaderForge.SFN_Multiply,id:1453,x:31950,y:31787,varname:node_1453,prsc:2|A-3445-OUT,B-1081-OUT;n:type:ShaderForge.SFN_ValueProperty,id:9584,x:31559,y:31821,ptovrint:False,ptlb:Lines_tile_u,ptin:_Lines_tile_u,varname:node_9584,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:4;n:type:ShaderForge.SFN_ValueProperty,id:8394,x:31559,y:31908,ptovrint:False,ptlb:Lines_tile_v,ptin:_Lines_tile_v,varname:node_8394,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Append,id:3445,x:31738,y:31777,varname:node_3445,prsc:2|A-9584-OUT,B-8394-OUT;n:type:ShaderForge.SFN_Multiply,id:2095,x:31948,y:32144,varname:node_2095,prsc:2|A-6418-OUT,B-2079-OUT;n:type:ShaderForge.SFN_ValueProperty,id:4169,x:31557,y:32178,ptovrint:False,ptlb:Vert_tile_u,ptin:_Vert_tile_u,varname:_Lines_tile_u_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:3;n:type:ShaderForge.SFN_ValueProperty,id:9853,x:31557,y:32264,ptovrint:False,ptlb:Vert_tile_v,ptin:_Vert_tile_v,varname:_Lines_tile_v_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Append,id:6418,x:31736,y:32134,varname:node_6418,prsc:2|A-4169-OUT,B-9853-OUT;n:type:ShaderForge.SFN_Multiply,id:652,x:31825,y:33235,varname:node_652,prsc:2|A-4879-OUT,B-6887-OUT;n:type:ShaderForge.SFN_Append,id:6887,x:31613,y:33225,varname:node_6887,prsc:2|A-8528-OUT,B-724-OUT;n:type:ShaderForge.SFN_ValueProperty,id:8528,x:31378,y:33285,ptovrint:False,ptlb:VO_tile_u,ptin:_VO_tile_u,varname:node_8528,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_ValueProperty,id:724,x:31378,y:33387,ptovrint:False,ptlb:VO_tile_v,ptin:_VO_tile_v,varname:node_724,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_Slider,id:6955,x:32744,y:32141,ptovrint:False,ptlb:ColorSlider,ptin:_ColorSlider,varname:node_6955,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:2;n:type:ShaderForge.SFN_Multiply,id:1202,x:32833,y:32273,varname:node_1202,prsc:2|A-6952-OUT,B-6955-OUT;n:type:ShaderForge.SFN_Lerp,id:2472,x:33318,y:32092,varname:node_2472,prsc:2|A-2585-RGB,B-6695-RGB,T-1202-OUT;n:type:ShaderForge.SFN_Color,id:2585,x:33051,y:31714,ptovrint:False,ptlb:Color1,ptin:_Color1,varname:node_2585,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.3949394,c2:1,c3:0,c4:1;n:type:ShaderForge.SFN_Color,id:6695,x:33051,y:31931,ptovrint:False,ptlb:Color2,ptin:_Color2,varname:node_6695,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0,c2:0.09174347,c3:1,c4:1;n:type:ShaderForge.SFN_Multiply,id:6049,x:33517,y:32425,varname:node_6049,prsc:2|A-2472-OUT,B-8341-OUT;n:type:ShaderForge.SFN_ValueProperty,id:3853,x:33775,y:32358,ptovrint:False,ptlb:Emissive_strength,ptin:_Emissive_strength,varname:node_3853,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1.5;n:type:ShaderForge.SFN_Multiply,id:9389,x:33775,y:32423,varname:node_9389,prsc:2|A-6049-OUT,B-3853-OUT;proporder:3785-4139-4205-9235-4893-3347-522-4684-7215-1901-8801-9584-8394-4169-9853-8528-724-6955-2585-6695-3853;pass:END;sub:END;*/

Shader "Custom/Aurora" {
    Properties {
        _MIX ("MIX", 2D) = "white" {}
        _VO_Strenght ("VO_Strenght", Float ) = 0.1
        _ ("", Float ) = 0
        _VO_SPD_U ("VO_SPD_U", Float ) = 0.1
        _VO_SPD_V ("VO_SPD_V", Float ) = 0
        _Vertiefungen_SPD_U ("Vertiefungen_SPD_U", Float ) = 0.1
        _Vertiefungen_SPD_V ("Vertiefungen_SPD_V", Float ) = 0
        _Vertiefungen_Wert ("Vertiefungen_Wert", Float ) = 0.5
        _Lines_SPD_U ("Lines_SPD_U", Float ) = 0.1
        _Lines_SPD_V ("Lines_SPD_V", Float ) = 0
        _Line_Strength ("Line_Strength", Float ) = 2
        _Lines_tile_u ("Lines_tile_u", Float ) = 4
        _Lines_tile_v ("Lines_tile_v", Float ) = 1
        _Vert_tile_u ("Vert_tile_u", Float ) = 3
        _Vert_tile_v ("Vert_tile_v", Float ) = 1
        _VO_tile_u ("VO_tile_u", Float ) = 0
        _VO_tile_v ("VO_tile_v", Float ) = 0
        _ColorSlider ("ColorSlider", Range(0, 2)) = 1
        _Color1 ("Color1", Color) = (0.3949394,1,0,1)
        _Color2 ("Color2", Color) = (0,0.09174347,1,1)
        _Emissive_strength ("Emissive_strength", Float ) = 1.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        LOD 200
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend One One
            Cull Off
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform sampler2D _MIX; uniform float4 _MIX_ST;
            uniform float _VO_Strenght;
            uniform float _;
            uniform float _VO_SPD_U;
            uniform float _VO_SPD_V;
            uniform float _Vertiefungen_SPD_U;
            uniform float _Vertiefungen_SPD_V;
            uniform float _Vertiefungen_Wert;
            uniform float _Lines_SPD_U;
            uniform float _Lines_SPD_V;
            uniform float _Line_Strength;
            uniform float _Lines_tile_u;
            uniform float _Lines_tile_v;
            uniform float _Vert_tile_u;
            uniform float _Vert_tile_v;
            uniform float _VO_tile_u;
            uniform float _VO_tile_v;
            uniform float _ColorSlider;
            uniform float4 _Color1;
            uniform float4 _Color2;
            uniform float _Emissive_strength;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                UNITY_FOG_COORDS(1)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                float4 node_8625 = _Time;
                float2 node_652 = (((float2(_VO_SPD_U,_VO_SPD_V)*node_8625.g)+o.uv0)*float2(_VO_tile_u,_VO_tile_v));
                float4 node_595 = tex2Dlod(_MIX,float4(TRANSFORM_TEX(node_652, _MIX),0.0,0));
                v.vertex.xyz += float3(float2(_,(node_595.r*_VO_Strenght)),0.0);
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
////// Lighting:
////// Emissive:
                float4 node_8625 = _Time;
                float2 node_1453 = (float2(_Lines_tile_u,_Lines_tile_v)*((float2(_Lines_SPD_U,_Lines_SPD_V)*node_8625.g)+i.uv0));
                float4 node_698 = tex2D(_MIX,TRANSFORM_TEX(node_1453, _MIX));
                float2 node_2095 = (float2(_Vert_tile_u,_Vert_tile_v)*((float2(_Vertiefungen_SPD_U,_Vertiefungen_SPD_V)*node_8625.g)+i.uv0));
                float4 node_92 = tex2D(_MIX,TRANSFORM_TEX(node_2095, _MIX));
                float3 node_2472 = lerp(_Color1.rgb,_Color2.rgb,(((_Line_Strength*node_698.a)+(_Vertiefungen_Wert+node_92.b))*_ColorSlider));
                float4 node_8563 = tex2D(_MIX,TRANSFORM_TEX(i.uv0, _MIX));
                float3 emissive = ((node_2472*(node_2472*node_8563.g))*_Emissive_strength);
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "ShadowCaster"
            Tags {
                "LightMode"="ShadowCaster"
            }
            Offset 1, 1
            Cull Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform sampler2D _MIX; uniform float4 _MIX_ST;
            uniform float _VO_Strenght;
            uniform float _;
            uniform float _VO_SPD_U;
            uniform float _VO_SPD_V;
            uniform float _VO_tile_u;
            uniform float _VO_tile_v;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
                float2 uv0 : TEXCOORD1;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                float4 node_8625 = _Time;
                float2 node_652 = (((float2(_VO_SPD_U,_VO_SPD_V)*node_8625.g)+o.uv0)*float2(_VO_tile_u,_VO_tile_v));
                float4 node_595 = tex2Dlod(_MIX,float4(TRANSFORM_TEX(node_652, _MIX),0.0,0));
                v.vertex.xyz += float3(float2(_,(node_595.r*_VO_Strenght)),0.0);
                o.pos = UnityObjectToClipPos( v.vertex );
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
