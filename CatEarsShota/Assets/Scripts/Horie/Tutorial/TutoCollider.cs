using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoCollider : MonoBehaviour
{

    [SerializeField]
    List<Vector3> TutoColliderPos = new List<Vector3>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        TutorialContriller.Instance.OnTutoCollider();
    }

    public void SetPosition(int num)
    {
        transform.position = TutoColliderPos[num];
    }
}
