using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    List<GameObject> RespawnEnemys = new List<GameObject>();
    List<float> MaxTime = new List<float>();
    List<float> FalseTime = new List<float>();

    [SerializeField]
    float Land_E_RespawnTime = 15;

    [SerializeField]
    float Fly_E_RespawnTime = 30;

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
        RespawnTimer();
        //Debug.Log(FalseTime[0]);
    }
    

    private void SetEnemyData() {
        LandEnemy[] LEnemy = FindObjectsOfType<LandEnemy>();
        foreach(LandEnemy e in LEnemy) {
            RespawnEnemys.Add(e.gameObject);
            MaxTime.Add(Land_E_RespawnTime);
            FalseTime.Add(0);
        }

        FlyEnemy[] FEnemy = FindObjectsOfType<FlyEnemy>();
        foreach (FlyEnemy e in FEnemy) {
            RespawnEnemys.Add(e.gameObject);
            MaxTime.Add(Fly_E_RespawnTime);
            FalseTime.Add(0);
        }
        

        Debug.Log(RespawnEnemys.Count);
    }

    private void RespawnTimer() {
        for(int i = 0; i < RespawnEnemys.Count; i++) {
            if (RespawnEnemys[i].activeSelf) continue;

            FalseTime[i] += Time.deltaTime;
            if (FalseTime[i] >= MaxTime[i]) {
                EnemyRespawn(i);
            }
        }
    }

    private void EnemyRespawn(int num) {
        RespawnEnemys[num].SetActive(true);
        RespawnEnemys[num].GetComponent<Enemy>().SetStartPos();
        FalseTime[num] = 0;
    }
}
