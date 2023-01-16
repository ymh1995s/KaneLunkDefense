using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    AudioSource theAudio;
    AudioSource theAudio2;

    [SerializeField] AudioClip[] Audio_BGM;
    [SerializeField] AudioClip[] Audio_DrawSound;
    [SerializeField] AudioClip[] Audio_EnemyDeadSound;
    [SerializeField] AudioClip Audio_EnemyCollisionSound;
    /// <summary>
    /// ///////////오디오 테스트
    /// </summary>

    public GameObject[] enemyObjs;
    public Transform[] spawnPoints;

    public ObjectManager objectManager;
    public Bullet bullet;


    public float curSpawnDelay;
    public float maxSpawnDelay;

    private float time_start=0;
    private float time_current;

    public Text money_text;
    public Text life_text;
    public int money;
    public int life;

    public int round;

    private int preDrawSound = 0;

    bool isBossSpawn = false;

    // Start is called before the first frame update
    void Start()
    {
        theAudio = GetComponent<AudioSource>();
        theAudio2 = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!theAudio.isPlaying)
        {
            theAudio.clip = Audio_BGM[0];
            theAudio.volume = 0.1f;
            theAudio.Play();
        }

        Check_Timer();

        curSpawnDelay += Time.deltaTime;

        if (curSpawnDelay>maxSpawnDelay)
        {
            SpawnEnemy();
            
            maxSpawnDelay = Random.Range(0.2f, 1f);
            curSpawnDelay = 0;
        }
        
    }

    public void DrawClickSound(int SoundNo)
    {
        AudioSource.PlayClipAtPoint(Audio_DrawSound[SoundNo], transform.position,1.0f); //1회성 효과음용,재생되면 알아서 삭제됨 메모리 이용 개꿀
    }


    public void EnemyDeadSound()
    {
        int SoundNo = Random.Range(0, 5);
        AudioSource.PlayClipAtPoint(Audio_EnemyDeadSound[SoundNo], transform.position, 0.1f); //1회성 효과음용,재생되면 알아서 삭제됨 메모리 이용 개꿀
    }

    public void EnemyCollsionSound()
    {
        AudioSource.PlayClipAtPoint(Audio_EnemyCollisionSound, transform.position, 0.1f); //1회성 효과음용,재생되면 알아서 삭제됨 메모리 이용 개꿀
    }


    void SpawnEnemy()
    {
        if(!isBossSpawn)
        {
            int ranPoint = Random.Range(0, 8);
            //int ranPointY = Random.Range(-3, 0);
            string enemyround = "Enemy" + round;
            GameObject enemy = objectManager.MakeObj(enemyround);
            enemy.transform.position = spawnPoints[ranPoint].position;
            //enemy.transform.position = new Vector2(-9.5f, ranPointY);
            Rigidbody2D rigid = enemy.GetComponent<Rigidbody2D>();
            if (enemyround == "Enemy4")
            {
                rigid.velocity = new Vector2(0.2f, 0);
                isBossSpawn = true;
            }
            else rigid.velocity = new Vector2(1, 0);

            Enemy enemyLogic = enemy.GetComponent<Enemy>();
            enemyLogic.gameManager = this;
        }        
    }


    void SpawnAlly()
    {
        float ypoint = Random.Range(-500, -100);
        float xpoint = 200; //= Random.Range(400, 800);
        float maxShotDelay = 0; //default1

        GameObject ally = null;
        int AllyLevel = Random.Range(0, 10000); //0~4
        
        money -= 0;

        Ally allyLogic = null; //일단 초기화맨

        if (AllyLevel < 3600) {
            xpoint = Random.Range(450, 490);
            maxShotDelay = Random.Range(850f, 1150f) / 1000f;
            ally = objectManager.MakeObj("Ally1");
            allyLogic = ally.GetComponent<Ally>();
            allyLogic.bulletNo = "Bullet1";
            allyLogic.bulletspeed = 10;
            DrawClickSound(0);
        }
        else if (AllyLevel < 8000)
        {
            xpoint = Random.Range(530, 570);
            ally = objectManager.MakeObj("Ally2");
            maxShotDelay = Random.Range(650f, 850f) / 1000f;
            allyLogic = ally.GetComponent<Ally>();
            allyLogic.bulletNo = "Bullet2";
            allyLogic.bulletspeed = 12;
            DrawClickSound(0);
        }
        else if (AllyLevel < 9700)
        {
            xpoint = Random.Range(610, 650);
            ally = objectManager.MakeObj("Ally3");
            maxShotDelay = Random.Range(400f, 650f) / 1000f;
            allyLogic = ally.GetComponent<Ally>();
            allyLogic.bulletNo = "Bullet3";
            allyLogic.bulletspeed = 14;
            DrawClickSound(0);
        }
        else if (AllyLevel < 9930) {
            xpoint = Random.Range(700, 720);
            ally = objectManager.MakeObj("Ally4");
            maxShotDelay = Random.Range(200f, 400f) / 1000f;
            allyLogic = ally.GetComponent<Ally>();
            allyLogic.bulletNo = "Bullet4";
            allyLogic.bulletspeed = 16;
            DrawClickSound(1);
        }
        else if (AllyLevel < 10000) {
            xpoint = Random.Range(760, 780);
            ally = objectManager.MakeObj("Ally5");
            maxShotDelay = Random.Range(50f, 100f) / 1000f;
            allyLogic = ally.GetComponent<Ally>();
            allyLogic.bulletNo = "Bullet5";
            allyLogic.bulletspeed = 20;
            DrawClickSound(2);
        }

        allyLogic.maxShotDelay = maxShotDelay;
        allyLogic.gameManager = this;
        allyLogic.objectManager = objectManager;
        ally.transform.position = new Vector3(xpoint/100, ypoint / 100, 0);
    }


    private void Check_Timer()
    {
        time_current = Time.time- time_start;
        if (time_current > 60)
        {
            objectManager.level++;
            round++;
            time_start = Time.time;
        }
    }

    public void Draw()
    {
        if (money < 10) return;
        else
        {
            SpawnAlly();
            money_text.text = money.ToString();
        }
    }

    public void Retry()
    {
        SceneManager.LoadScene(0);
    }

    public void PowerUp()
    {
        //bullet.dmg +=3;
        //print(bullet.dmg);
    }

    public void SpawnLevel5()
    {
        float ypoint = Random.Range(-500, 0);
        float xpoint = 200; //= Random.Range(400, 800);
        float maxShotDelay = 0; //default1

        GameObject ally = null;
        int AllyLevel = Random.Range(0, 10000); //0~4

        money -= 10;

        Ally allyLogic = null; //일단 초기화맨

        xpoint = Random.Range(760, 780);
        ally = objectManager.MakeObj("Ally5");
        maxShotDelay = Random.Range(50f, 100f) / 1000f;
        allyLogic = ally.GetComponent<Ally>();
        allyLogic.bulletNo = "Bullet5";
        allyLogic.bulletspeed = 20;

        allyLogic.maxShotDelay = maxShotDelay;
        allyLogic.gameManager = this;
        allyLogic.objectManager = objectManager;
        ally.transform.position = new Vector3(xpoint / 100, ypoint / 100, 0);
    }
}
