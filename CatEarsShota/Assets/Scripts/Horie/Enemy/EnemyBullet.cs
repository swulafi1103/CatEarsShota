using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider2D))]
public class EnemyBullet : MonoBehaviour
{
    [SerializeField,Range(0.1f,20f)]
    float bulletSpeed;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 vec = transform.rotation * new Vector3(bulletSpeed,0,0);
        transform.position += vec * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Player") {
            Debug.Log("HIT");   //プレイヤーHP減算
        }
        else if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Bullet") {
            return;
        }
        this.gameObject.SetActive(false);
    }
}
