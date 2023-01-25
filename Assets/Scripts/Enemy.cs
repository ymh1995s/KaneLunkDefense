using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public string enemyName;
    public float speed;
    public int health;
    public int armor;

    public Sprite[] sprites;

    Rigidbody2D rigid;

    public GameManager gameManager;

    int BossHP = 200000;

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
                health = 20;
                armor = 0;
                break;
            case "B":
                health =375;
                armor = 5;
                break;
            case "C":
                health =900;
                armor =10;
                break;
            case "D":
                health =1800;
                armor =20;
                break;
            case "E":
                health = 200000;
                armor = 30;
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
            gameManager.money += 20;
            gameObject.SetActive(false);
            if(gameManager.life<=0|| enemyName=="E")
            {
                gameManager.OverGame();
            }
        }

        else if (collision.gameObject.tag == "Bullet")
        {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            OnHit(bullet.dmg + gameManager.powerUp);
            collision.gameObject.SetActive(false);
        }
    }

    public void OnHit(int dmg)
    {
        //print(dmg);
        //print(health);
        int trueDmg = dmg - armor;
        if (trueDmg < 1) trueDmg = 1;
        health -= trueDmg;

        if(enemyName=="E")
        {
            BossHP -= trueDmg;
            gameManager.BossHP_text.text = "유 썩 {체력 : " + BossHP + "}";
        }

        if (health <= 0)//적 기체 피격
        {
            gameObject.SetActive(false);
            gameManager.money += 2;
            gameManager.money_text.text= gameManager.money.ToString();
            transform.rotation = Quaternion.identity;
            gameManager.EnemyDeadSound();

            if(enemyName == "E")
            {
                gameManager.ClearGame();
            }
        }
    }
}
