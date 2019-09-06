using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomControll : MonoBehaviour
{
    private static MushroomControll instance;
    public static MushroomControll Instance {
        get { return instance; }
    }

    private void Awake() {
        instance = this;
    }

    private void Update() {

        if (Input.GetKeyDown(KeyCode.Y))
            SetPastMush(0);
    }

    [SerializeField]
    PastMushroom[] pastMushs;

    public void SetPastMush(int num) {
        pastMushs[num].SetMushroom();
    }
}
