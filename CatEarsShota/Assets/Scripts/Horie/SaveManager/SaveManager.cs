using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    private static SaveManager instance;
    public static SaveManager Instance {
        get { return instance; }
    }

    private void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    [SerializeField]
    List<Vector3> nowRestartPos = new List<Vector3>();
    [SerializeField]
    List<Vector3> pastRestartPos = new List<Vector3>();

    SavePosObj[] sevePosObjs;

    int nowPos = 0;
    int pastPos = 0;

    GameObject perrault;
    GameObject fran;

    // Start is called before the first frame update
    void Start() {
        setData();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Numlock)) {
            RestartPointer();
        }
    }

    void setData() {
        nowPos = 0;
        pastPos = 0;
        sevePosObjs = GetComponentsInChildren<SavePosObj>();
        sevePosObjs[0].SetSavePos(nowRestartPos[nowPos + 1]);
        sevePosObjs[1].SetSavePos(pastRestartPos[pastPos + 1]);
        perrault = PlayerManager.Instance.Pero;
        fran = PlayerManager.Instance.Fran;

    }

    public void RestartPointer() {
        if (FlagManager.Instance.IsPast) {
            fran.transform.position = pastRestartPos[pastPos];
        }
        else {
            perrault.transform.position = nowRestartPos[nowPos];
        }
        Debug.Log("Restart Pointer");
    }

    public void SavePos(SavePosObj obj) {
        if(obj == sevePosObjs[0]) {
            nowPos++;
            sevePosObjs[0].SetSavePos(nowRestartPos[nowPos + 1]);
            Debug.Log("now Save");
        }
        else {
            pastPos++;
            sevePosObjs[1].SetSavePos(pastRestartPos[pastPos + 1]);
            Debug.Log("past Save");
        }
    }
}
