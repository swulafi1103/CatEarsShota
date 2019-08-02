using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePosObj : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetSavePos(Vector3 pos) {
        this.transform.position = pos;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag != "Player") return;
        SaveManager.Instance.SavePos(this);
    }
}
