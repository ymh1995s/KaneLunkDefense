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

    public GameManager gameManager;

    private void Awake()
    {

        rigid = GetComponent<Rigidbody2D>();
        rigid.velocity = Vector2.right * speed;
        
    }
    private void OnEnable()
    {
        switch (enemyName)
        {
            case "A":
                health = 10;
                break;
            case "B":
                health = 100;
                break;
            case "C":
                health = 650;
                break;
            case "D":
                health = 2500;
                break;
            case "E":
                health = 70000;
                break;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Border_DefenseLine")
        {
            transform.rotation = Quaternion.identity; //0도로 다시 돌림
            gameManager.life -= 1;
            gameManager.life_text.text = gameManager.life.ToString();
            gameManager.EnemyCollsionSound();
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
        //print(health);
        health -= dmg;
        if (health <= 0)//적 기체 피격
        {
            gameObject.SetActive(false);
            gameManager.money += 2;
            gameManager.money_text.text= gameManager.money.ToString();
            transform.rotation = Quaternion.identity;
            gameManager.EnemyDeadSound();
        }
    }
}
