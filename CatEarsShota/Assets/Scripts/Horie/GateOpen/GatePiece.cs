using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatePiece : MonoBehaviour
{
    [SerializeField]
    BoxCollider2D[] Colliders;
    

    public void OnEnableCollider(int num)
    {
        Colliders[num].enabled = false;
    }
}
