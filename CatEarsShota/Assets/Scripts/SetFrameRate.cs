using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetFrameRate : MonoBehaviour
{
    private int rate = 60;
    private void Awake()
    {
        Application.targetFrameRate = rate;
    }
}
