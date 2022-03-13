using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DataType
{
    none = 0,
    objectData = 1,
    playerData = 2,
}

public class SquaresData : MonoBehaviour
{
    const int SquareMaxSize = 4000;
    int[,] squaresDataArray = new int[SquareMaxSize, SquareMaxSize];
    public int[,] SquaresDataArray { get { return squaresDataArray; } }

    // Start is called before the first frame update
    void Start()
    {
        for(int x = 0; x < SquareMaxSize; x++)
        {
            for (int y = 0; y < SquareMaxSize; y++)
            {
                squaresDataArray[x, y] = (int)DataType.none;
            }
        }
    }

    /// <summary>
    /// マスに何も置かれていないかをチェック
    /// </summary>
    public bool CheckSquares(int pos_x,int pos_y,int squares_x,int squares_y)
    {
        // 範囲外になっていないかどうかを確認
        if (pos_x + squares_x > SquareMaxSize) return false;
        else if (pos_y + squares_y > SquareMaxSize) return false;

        for (int x = pos_x; x < pos_x + squares_x; x++)
        {
            for (int y = pos_y; y < pos_y + squares_y; y++)
            {
                Debug.Log(SquaresDataArray[x, y]);
                if (SquaresDataArray[x, y] != (int)DataType.none) return false;
            }
        }
        return true;
    }


    /// <summary>
    /// マスに何も置かれていないかをチェック
    /// </summary>
    public bool UpdateSquaresStatus(int pos_x, int pos_y, int squares_x, int squares_y)
    {
        // 範囲外になっていないかどうかを確認
        if (pos_x + squares_x > SquareMaxSize) return false;
        else if (pos_y + squares_y > SquareMaxSize) return false;

        for (int x = pos_x; x < pos_x + squares_x; x++)
        {
            for (int y = pos_y; y < pos_y + squares_y; y++)
            {
                SquaresDataArray[x, y] = (int)DataType.objectData;
            }
        }
        return true;
    }
}
