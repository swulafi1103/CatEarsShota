using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpManager : MonoBehaviour
{

    [SerializeField]
    private Transform[] warpPos = new Transform[7];

    //  配列の座標にワープ
    public void WarpFran(int value)
    {
        //  不正な値のチェック
        if (value < 0 || value > warpPos.Length)
        {
            Debug.LogWarning("ワープで予定外の値:" + value);
            return;
        }
        Vector3 pos = warpPos[value].position;
        pos.z = 0;
        PlayerManager.Instance.Fran.transform.position = pos;
    }

}
