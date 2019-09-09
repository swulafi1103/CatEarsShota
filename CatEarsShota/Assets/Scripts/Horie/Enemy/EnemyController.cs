using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    Enemy[] RespawnEnemys;
    //List<float> MaxTime = new List<float>();
    //List<float> FalseTime = new List<float>();

    //[SerializeField]
    //float Land_E_RespawnTime = 15;

    //[SerializeField]
    //float Fly_E_RespawnTime = 30;

    [SerializeField]
    Transform[] respawnPos = new Transform[2];

    static EnemyController instance;
    public static EnemyController Instance {
        get { return instance; }
    }

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        SetEnemyData();
    }

    // Update is called once per frame
    void Update()
    {
        //RespawnTimer();
        //Debug.Log(FalseTime[0]);
        //if(Input.GetKeyDown(KeyCode.Z)) SetEnemys();    //debug
    }
    

    private void SetEnemyData() {
        RespawnEnemys = GetComponentsInChildren<LandEnemy>();
        foreach(Enemy enemy in RespawnEnemys) {
            enemy.gameObject.SetActive(false);
        }
        
        //LandEnemy[] LEnemy = FindObjectsOfType<LandEnemy>();
        //foreach(LandEnemy e in LEnemy) {
        //    RespawnEnemys.Add(e.gameObject);
        //    MaxTime.Add(Land_E_RespawnTime);
        //    FalseTime.Add(0);
        //}

        //FlyEnemy[] FEnemy = FindObjectsOfType<FlyEnemy>();
        //foreach (FlyEnemy e in FEnemy) {
        //    RespawnEnemys.Add(e.gameObject);
        //    MaxTime.Add(Fly_E_RespawnTime);
        //    FalseTime.Add(0);
        //}
    }

    public void SetEnemys() {
        foreach(Enemy enemy in RespawnEnemys) {
            enemy.SetStartPos();
        }
    }

    //private void RespawnTimer() {
    //    for(int i = 0; i < RespawnEnemys.Count; i++) {
    //        if (RespawnEnemys[i].activeSelf) continue;

    //        FalseTime[i] += Time.deltaTime;
    //        if (FalseTime[i] >= MaxTime[i]) {
    //            EnemyRespawn(i);
    //        }
    //    }
    //}

    //private void EnemyRespawn(int num) {
    //    RespawnEnemys[num].SetActive(true);
    //    RespawnEnemys[num].GetComponent<Enemy>().SetStartPos();
    //    FalseTime[num] = 0;
    //}


    /// <summary>
    /// フランが戻る
    /// </summary>
    public void RespawnFran()
    {
        StartCoroutine(ResetFranPos());
    }

    IEnumerator ResetFranPos()
    {
        Fade.Instance.StartFade(0.5f, Color.black);
        while (!Fade.Instance.Fading == false)
            yield return null;
        PlayerManager.Instance.Fran.transform.position = checkPos();
        Fade.Instance.ClearFade(0.5f, Color.clear);

        yield break;
    }

    Vector3 checkPos()
    {
        Vector3 pos = respawnPos[0].position;
        Vector3 fran = PlayerManager.Instance.Fran.transform.position;

        float dev1 = Mathf.Abs(respawnPos[0].position.x - fran.x);
        float dev2 = Mathf.Abs(respawnPos[1].position.x - fran.x);

        if (dev1 > dev2)
        {
            pos = respawnPos[1].position;
        }

        return pos;
    }
}
