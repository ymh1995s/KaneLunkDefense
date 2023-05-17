using UnityEngine;

public class Enemy : MonoBehaviour
{
    public SpriteRenderer spriter;
    Rigidbody2D rigid;

    [Header("# Enemy Info")]
    public string enemyName;
    public float speed;
    public int health;
    public int armor;
    public bool isAlive=true;

    private int BossHP = 200000;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
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
            isAlive = false;

            transform.rotation = Quaternion.identity; //0도로 다시 돌림

            GameManager.instance.life -= 1;
            GameManager.instance.money += 20;
            GameManager.instance.enemySFX.BorderHitSound();

            gameObject.SetActive(false);

            if (GameManager.instance.life<=0|| enemyName=="E")  GameManager.instance.OverGame();
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

        if(enemyName=="E")
        {
            BossHP -= trueDmg;
            GameManager.instance.inGameUI.texts[12].text = "유 썩 {체력 : " + BossHP + "}";
        }

        if (health <= 0)//적 기체 피격
        {
            if (!isAlive) return;

            isAlive = false;

            GameManager.instance.money += 2;
            GameManager.instance.enemySFX.DeadSound();

            transform.rotation = Quaternion.identity;
            gameObject.SetActive(false);

            if (enemyName == "E")
            {
                GameManager.instance.ClearGame();
            }
        }
    }
}
