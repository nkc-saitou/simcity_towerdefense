using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectBase : MonoBehaviour
{
    //オブジェクト設置に必要なマス目の数
    [SerializeField,Header("オブジェクト設置に必要なマス目の数")]
    protected int squaresCount_x = 1;
    [SerializeField]
    protected int squaresCount_y = 1;
    [SerializeField]
    protected int squaresCount_z = 1;

    //設置に必要なお金
    [SerializeField,Header("設置に必要なお金")]
    protected int cost = 0;


    /// <summary>
    /// オブジェクト設置に必要なマス目の数 x軸
    /// </summary>
    public int SquaresCount_x { get { return squaresCount_x; } }

    /// <summary>
    /// オブジェクト設置に必要なマス目の数 x軸
    /// </summary>
    public int SquaresCount_y { get { return squaresCount_y; } }

    /// <summary>
    /// オブジェクト設置に必要なマス目の数 x軸
    /// </summary>
    public int SquaresCount_z { get { return squaresCount_z; } }

    /// <summary>
    /// オブジェクト設置に必要なお金
    /// </summary>
    public int Cost { get { return cost; } }
}
