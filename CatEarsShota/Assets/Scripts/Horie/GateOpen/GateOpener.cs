using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateOpener : MonoBehaviour
{
    [SerializeField]
    Animator[] gateAnim;

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Numlock)) OpenGate(0);
    //    if (Input.GetKeyDown(KeyCode.End)) OpenGate(1);
    //}

    public void OpenGate(int num)
    {
        foreach(Animator anim in gateAnim)
        {
            anim.SetTrigger("Open" + num);
        }
    }
}
