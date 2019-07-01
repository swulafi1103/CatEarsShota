using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletPool : MonoBehaviour
{
    [SerializeField]
    GameObject bulletBase;
    [SerializeField]
    int BaseCount = 20;

    List<GameObject> BulletList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        FirstCreate();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FirstCreate() {
        for(int i = 0; i < BaseCount; i++) {
            GameObject obj = CreateBullet(false);
            BulletList.Add(obj);
        }
    }

    public GameObject ReturnBullet(Vector3 pos) {
        foreach(GameObject b in BulletList) {
            if (b.activeSelf == false) {
                b.transform.position = pos;
                b.transform.rotation = new Quaternion(0, 0, 0, 0);
                b.SetActive(true);
                return b;
            }
        }

        GameObject bullet = CreateBullet(true);
        bullet.transform.position = pos;
        bullet.transform.rotation = new Quaternion(0, 0, 0, 0);
        BulletList.Add(bullet);
        return bullet;
    }

    private GameObject CreateBullet(bool act) {
        GameObject obj = Instantiate(bulletBase);
        obj.transform.position = Vector3.zero;
        obj.transform.rotation = Quaternion.identity;
        obj.name = "EnemyBullet_" + BulletList.Count;
        obj.transform.parent = this.transform;
        obj.SetActive(act);
        return obj;
    }
}
