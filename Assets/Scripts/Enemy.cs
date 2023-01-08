using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public string enemyName;
    public float speed;
    public int health;
    
    public Sprite[] sprites;

    Rigidbody2D rigid;


    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        rigid.velocity = Vector2.right * speed;
        
    }
    private void OnEnable()
    {
        switch (enemyName)
        {
            //case "A":
            //    health = 10;
            //    break;
            //case "B":
            //    health = 100;
            //    break;
            //case "S":
            //    health = 1000;
            //    break;
            //case "C":
            //    health = 10000;
            //    Invoke("Stop", 2);
            //    break;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Border_DefenseLine")
        {
            transform.rotation = Quaternion.identity; //0도로 다시 돌림
            gameObject.SetActive(false);
        }

        else if (collision.gameObject.tag == "Bullet")
        {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            OnHit(bullet.dmg);
            collision.gameObject.SetActive(false);
        }
    }

    public void OnHit(int dmg)
    {
        health -= dmg;
        if (health <= 0)
        {
            gameObject.SetActive(false);
            transform.rotation = Quaternion.identity;
        }
    }
}
