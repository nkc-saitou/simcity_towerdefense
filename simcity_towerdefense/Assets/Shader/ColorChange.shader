Shader "Custom/ColorChange"
{
    // inspector�ɕ\������
    Properties{
        // �F��ύX����
        _BaseColor("Base Color",Color) = (1,1,1,1)
    }

        SubShader{
        // ��ŗ�������
        Tags{ "RenderType" = "Opaque"}
        LOD 200

        // ��ŗ�������
        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows
        #pragma target 3.0

        // input�\����
        struct Input {
        // �K�v�Ȃ����Asurf�֐����g�����߂Ɏd���Ȃ��g�p
        float2 uv_MainTex;
    };

    // �F��ύX����ϐ�
    fixed4 _BaseColor;

    // surf�֐�
    void surf(Input IN, inout SurfaceOutputStandard o) {
        // �x�[�X�J���[���v���p�e�B����ύX�����F�ɕς���
        o.Albedo = _BaseColor.rgb;
    }
    // ��ŗ�������
    ENDCG
    }
        // ��ŗ�������
        FallBack "Diffuse"
}
