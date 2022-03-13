Shader "Custom/ColorChange"
{
    // inspectorに表示する
    Properties{
        // 色を変更する
        _BaseColor("Base Color",Color) = (1,1,1,1)
    }

        SubShader{
        // 後で理解する
        Tags{ "RenderType" = "Opaque"}
        LOD 200

        // 後で理解する
        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows
        #pragma target 3.0

        // input構造体
        struct Input {
        // 必要ないが、surf関数を使うために仕方なく使用
        float2 uv_MainTex;
    };

    // 色を変更する変数
    fixed4 _BaseColor;

    // surf関数
    void surf(Input IN, inout SurfaceOutputStandard o) {
        // ベースカラーをプロパティから変更した色に変える
        o.Albedo = _BaseColor.rgb;
    }
    // 後で理解する
    ENDCG
    }
        // 後で理解する
        FallBack "Diffuse"
}
