//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class WallEnemy : Enemy
//{
//    Transform Houdai;

//    [SerializeField]
//    float[] maxRotate = new float[2];

//    [SerializeField,Range(5,20f)]
//    float RotateSpeed = 5;

//    float moves = -1;

//    EnemyBulletPool bulletBase;

//    [SerializeField,Range(1,3)]
//    float bulletHosei = 1.5f;
    

//    protected override void Awake() {
//        base.Awake();
//        base.Hp = 2;
//        nowHp = Hp;
//        Houdai = transform.GetChild(0);
//        bulletBase = FindObjectOfType<EnemyBulletPool>();
//    }

//    protected override void EnemyMove() {
//        float z = moves * Time.deltaTime * RotateSpeed;
//        Houdai.Rotate(new Vector3(0, 0, z));
//        if (Houdai.rotation.eulerAngles.z <= maxRotate[0]) {
//            moves *= -1;
//        }
//        else if (Houdai.rotation.eulerAngles.z >= maxRotate[1]) {
//            moves *= -1;
//        }
//    }

//    protected override void Attack() {
//        Timer += Time.deltaTime;
//        if (Timer < attackInterval) return;
//        Timer = 0;
//        PushBullet();

//    }

//    private void PushBullet() {

//        for (int i = 0; i < 3; i++) {
//            int num = -1 + i;
//            Quaternion baseq = Houdai.rotation;
//            baseq.z += num * 0.1f;
//            Vector3 pos = transform.position + (baseq * new Vector3(bulletHosei, 0, 0));
//            GameObject bullet = bulletBase.ReturnBullet(pos);
//            bullet.transform.rotation = baseq;
//        }
//    }
    
//}
