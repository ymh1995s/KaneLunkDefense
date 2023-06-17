using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public SpriteRenderer spriter;
    Rigidbody2D rigid;

    [Header("# Enemy Info")]
    public string enemyName;
    public float speed;
    public float health;
    public int armor;
    public bool isAlive=true;

    string BossAHP;
    string BossBHP;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        BossAHP = Convert.ToString(GameManager.instance.EnemySpecHP[4]);
        BossBHP = Convert.ToString(GameManager.instance.EnemySpecHP[7]);
    }

    private void OnEnable()
    {
        switch (enemyName)
        {
            case "A":
                health = GameManager.instance.EnemySpecHP[0];
                armor = GameManager.instance.EnemySpecArmor[0];
                break;
            case "B":
                health = GameManager.instance.EnemySpecHP[1];
                armor = GameManager.instance.EnemySpecArmor[1];
                break;
            case "C":
                health = GameManager.instance.EnemySpecHP[2];
                armor = GameManager.instance.EnemySpecArmor[2];
                break;
            case "D":
                health = GameManager.instance.EnemySpecHP[3];
                armor = GameManager.instance.EnemySpecArmor[3];
                break;
            case "E":
                health = GameManager.instance.EnemySpecHP[4];
                armor = GameManager.instance.EnemySpecArmor[4];
                break;
            case "F":
                health = GameManager.instance.EnemySpecHP[5];
                armor = GameManager.instance.EnemySpecArmor[5];
                break;
            case "G":
                health = GameManager.instance.EnemySpecHP[6];
                armor = GameManager.instance.EnemySpecArmor[6];
                break;
            case "H":
                health = GameManager.instance.EnemySpecHP[7];
                armor = GameManager.instance.EnemySpecArmor[7];
                break;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Border_DefenseLine")
        {
            isAlive = false;

            transform.rotation = Quaternion.identity; //0도로 다시 돌림

            GameManager.instance.life -= 1;
            GameManager.instance.money += 20;
            GameManager.instance.enemySFX.BorderHitSound();

            gameObject.SetActive(false);

            if (GameManager.instance.life <= 0 || enemyName == "E" || enemyName == "H") GameManager.instance.OverGame();
        }

        else if (collision.gameObject.tag == "Bullet")
        {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            OnHit(bullet.dmg + GameManager.instance.powerUp);
            collision.gameObject.SetActive(false);
        }
    }

    public void OnHit(int dmg)
    {
        int trueDmg = dmg - armor;
        if (trueDmg < 1) trueDmg = 1;
        health -= trueDmg;
        if (enemyName == "E")
        {
            GameManager.instance.inGameUI.BOSSHP_Text.text = "유 썩 " + Convert.ToString(health);
            GameManager.instance.inGameUI.BOSSHP_Image.fillAmount = health / GameManager.instance.EnemySpecHP[4];
        }
        if (enemyName == "H")
        {
            GameManager.instance.inGameUI.BOSSHP_Text.text = "타지리 " + Convert.ToString(health);
            GameManager.instance.inGameUI.BOSSHP_Image.fillAmount = health / GameManager.instance.EnemySpecHP[7];
        }

        if (health <= 0)//적 기체 피격
        {
            if (!isAlive) return; //돈 중복 수혜 방지

            isAlive = false;

            GameManager.instance.money += 2;
            GameManager.instance.enemySFX.DeadSound();

            transform.rotation = Quaternion.identity;
            gameObject.SetActive(false);

            if (enemyName == "E")
            {
                GameManager.instance.money += 98;
                GameManager.instance.time_start = 0;
                GameManager.instance.spawner.isLade = false;
                GameManager.instance.inGameUI.BoSSInfo.SetActive(false);
                GameManager.instance.inGameUI.BOSSHP_Text.text = "타지리 400000";
                GameManager.instance.inGameUI.BOSSHP_Image.fillAmount = 1;
            }
            else if (enemyName == "F") GameManager.instance.money += 2;
            else if (enemyName == "G") GameManager.instance.money += 2;
            else if (enemyName == "H") GameManager.instance.ClearGame();
        }
    }
}
