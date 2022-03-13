using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectBase : MonoBehaviour
{
    //�I�u�W�F�N�g�ݒu�ɕK�v�ȃ}�X�ڂ̐�
    [SerializeField,Header("�I�u�W�F�N�g�ݒu�ɕK�v�ȃ}�X�ڂ̐�")]
    protected int squaresCount_x = 1;
    [SerializeField]
    protected int squaresCount_y = 1;
    [SerializeField]
    protected int squaresCount_z = 1;

    //�ݒu�ɕK�v�Ȃ���
    [SerializeField,Header("�ݒu�ɕK�v�Ȃ���")]
    protected int cost = 0;


    /// <summary>
    /// �I�u�W�F�N�g�ݒu�ɕK�v�ȃ}�X�ڂ̐� x��
    /// </summary>
    public int SquaresCount_x { get { return squaresCount_x; } }

    /// <summary>
    /// �I�u�W�F�N�g�ݒu�ɕK�v�ȃ}�X�ڂ̐� x��
    /// </summary>
    public int SquaresCount_y { get { return squaresCount_y; } }

    /// <summary>
    /// �I�u�W�F�N�g�ݒu�ɕK�v�ȃ}�X�ڂ̐� x��
    /// </summary>
    public int SquaresCount_z { get { return squaresCount_z; } }

    /// <summary>
    /// �I�u�W�F�N�g�ݒu�ɕK�v�Ȃ���
    /// </summary>
    public int Cost { get { return cost; } }
}
