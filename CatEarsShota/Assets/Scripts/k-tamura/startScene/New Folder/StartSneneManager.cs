using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSneneManager : MonoBehaviour
{
    // Start is called before the first frame update
    bool loading;

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown&&loading==false)
        {
            loading = true;
            SceneLoadManager.LoadScene("main");
        }
    }
}
