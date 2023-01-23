using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject MainImage;
    public GameObject ClearImage;
    public GameObject OverImage;

    AudioSource theAudio;

    [SerializeField] AudioClip[] Audio_BGM;
    [SerializeField] AudioClip[] Audio_PowerUPSound;
    [SerializeField] AudioClip[] Audio_DrawSound;
    [SerializeField] AudioClip[] Audio_EnemyDeadSound;
    [SerializeField] AudioClip Audio_EnemyCollisionSound;
    [SerializeField] AudioClip Audio_GameOver;
    [SerializeField] AudioClip Audio_GameClear;

    public GameObject[] enemyObjs;
    public Transform[] spawnPoints;

    public ObjectManager objectManager;
    public Bullet bullet;

    int[] AllySpec = new int[5] { 8, 11, 15, 20, 30 };
    int[] EnemySpecHP = new int[5] {10,250,800,1800, 200000};
    int[] EnemySpecArmor = new int[5] { 0,5,10,20,30};

    public float curSpawnDelay;
    public float maxSpawnDelay;

    private float time_start=0;
    private float time_current;

    public Text money_text;
    public Text life_text;
    public Text enemySpec_text;
    public Text BossHP_text;
    public Text Ally1Spec_text;
    public Text Ally2Spec_text;
    public Text Ally3Spec_text;
    public Text Ally4Spec_text;
    public Text Ally5Spec_text;
    public int money;
    public int life;
    int PowerUpCost = 10;

    public int powerUp = 0;

    public int round=0;
    
    bool isBossSpawn = false;
    bool isBossClear = false;
    bool isGameStart = false;
    bool isGameClear = false;

    public void PushStartButton()
    {
        MainImage.SetActive(false);

        isGameStart = true;

        time_start = Time.time;//쭈꾸다시용
    }

    public void PushStartEnd()
    {
        Application.Quit();
    }

    public void ClearGame()
    {
        for (int i = 0; i < objectManager.enemy1.Length; i++) objectManager.enemy1[i].SetActive(false);
        for (int i = 0; i < objectManager.enemy2.Length; i++) objectManager.enemy2[i].SetActive(false);
        for (int i = 0; i < objectManager.enemy3.Length; i++) objectManager.enemy3[i].SetActive(false);
        for (int i = 0; i < objectManager.enemy4.Length; i++) objectManager.enemy4[i].SetActive(false);
        for (int i = 0; i < objectManager.enemy5.Length; i++) objectManager.enemy5[i].SetActive(false);
        AudioSource.PlayClipAtPoint(Audio_GameClear, transform.position, 1.0f); //1회성 효과음용,재생되면 알아서 삭제됨 메모리 이용 개꿀
        isGameClear = true;
        ClearImage.SetActive(true);
    }

    public void OverGame()
    {
        for (int i = 0; i < objectManager.enemy1.Length; i++) objectManager.enemy1[i].SetActive(false);
        for (int i = 0; i < objectManager.enemy2.Length; i++) objectManager.enemy2[i].SetActive(false);
        for (int i = 0; i < objectManager.enemy3.Length; i++) objectManager.enemy3[i].SetActive(false);
        for (int i = 0; i < objectManager.enemy4.Length; i++) objectManager.enemy4[i].SetActive(false);
        for (int i = 0; i < objectManager.enemy5.Length; i++) objectManager.enemy5[i].SetActive(false);
        AudioSource.PlayClipAtPoint(Audio_GameOver, transform.position, 1.0f); //1회성 효과음용,재생되면 알아서 삭제됨 메모리 이용 개꿀
        isGameClear = true;
        OverImage.SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        theAudio = GetComponent<AudioSource>();

        Ally1Spec_text.text = "슈터 1 {공격력 : " + (AllySpec[0] + powerUp) + "}";
        Ally2Spec_text.text = "슈터 2 {공격력 : " + (AllySpec[1] + powerUp) + "}";
        Ally3Spec_text.text = "슈터 3 {공격력 : " + (AllySpec[2] + powerUp) + "}";
        Ally4Spec_text.text = "슈터 4 {공격력 : " + (AllySpec[3] + powerUp) + "}";
        Ally5Spec_text.text = "슈터 5 {공격력 : " + (AllySpec[4] + powerUp) + "}";
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGameStart)//뭉탱이 월드
        {
            theAudio.clip = Audio_BGM[0];
        }
        else
        {
           theAudio.clip = Audio_BGM[1];
        }

        if (!theAudio.isPlaying)
        {
            theAudio.volume = 0.1f;
            theAudio.Play();
        }

        if (!isGameStart) return;

        if (!isGameClear)
        {
            Check_Timer();

            curSpawnDelay += Time.deltaTime;

            if (curSpawnDelay > maxSpawnDelay)
            {
                SpawnEnemy();

                maxSpawnDelay = Random.Range(0.2f, 1f);
                curSpawnDelay = 0;
            }
        }
    }

    public void DrawClickSound(int SoundNo)
    {
        AudioSource.PlayClipAtPoint(Audio_DrawSound[SoundNo], transform.position, 1.0f); //1회성 효과음용,재생되면 알아서 삭제됨 메모리 이용 개꿀
    }

    public void PowerUPClickSound()
    {
        int SoundNo = Random.Range(0, 4);
        float soundVolumn = 1.0f;
        if (SoundNo == 2) soundVolumn = 0.7f; //가람이 시계
        AudioSource.PlayClipAtPoint(Audio_PowerUPSound[SoundNo], transform.position, soundVolumn); //1회성 효과음용,재생되면 알아서 삭제됨 메모리 이용 개꿀
    }

    public void EnemyDeadSound()
    {
        int SoundNo = Random.Range(0, 5);
        float soundVolumn = 0.2f;
        if (SoundNo == 4) soundVolumn = 0.1f; //아이고난5

        AudioSource.PlayClipAtPoint(Audio_EnemyDeadSound[SoundNo], transform.position, soundVolumn); //1회성 효과음용,재생되면 알아서 삭제됨 메모리 이용 개꿀
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
        
        money -= 10;

        Ally allyLogic = null; //일단 초기화맨

        if (AllyLevel < 3600) {
            xpoint = Random.Range(450, 490);
            maxShotDelay = Random.Range(850f, 1100f) / 1000f;
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
            maxShotDelay = Random.Range(650f, 800f) / 1000f;
            allyLogic = ally.GetComponent<Ally>();
            allyLogic.bulletNo = "Bullet2";
            allyLogic.bulletspeed = 12;
            DrawClickSound(0);
        }
        else if (AllyLevel < 9700)
        {
            xpoint = Random.Range(610, 650);
            ally = objectManager.MakeObj("Ally3");
            maxShotDelay = Random.Range(400f, 600f) / 1000f;
            allyLogic = ally.GetComponent<Ally>();
            allyLogic.bulletNo = "Bullet3";
            allyLogic.bulletspeed = 14;
            DrawClickSound(0);
        }
        else if (AllyLevel < 9930) {
            xpoint = Random.Range(700, 720);
            ally = objectManager.MakeObj("Ally4");
            maxShotDelay = Random.Range(150f, 300f) / 1000f;
            allyLogic = ally.GetComponent<Ally>();
            allyLogic.bulletNo = "Bullet4";
            allyLogic.bulletspeed = 16;
            DrawClickSound(1);
        }
        else if (AllyLevel < 10000) {
            xpoint = Random.Range(760, 780);
            ally = objectManager.MakeObj("Ally5");
            maxShotDelay = Random.Range(50f, 90f) / 1000f;
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
            enemySpec_text.text = (round+1)+"단계 {체력 : "+ EnemySpecHP[round]+ "} " + "{방어력 : "+ EnemySpecArmor[round] + "}";
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
        MainImage.SetActive(false);
        OverImage.SetActive(false);
        ClearImage.SetActive(false);
        SceneManager.LoadScene(0);
    }

    public void PowerUp()
    {
        if (money < PowerUpCost) return;
        else
        {
            PowerUPClickSound();
            money -= PowerUpCost;
            PowerUpCost++;
            money_text.text = money.ToString();
            powerUp += 2;
            Ally1Spec_text.text = "슈터 1 {공격력 : " + (AllySpec[0] + powerUp) + "}";
            Ally2Spec_text.text = "슈터 2 {공격력 : " + (AllySpec[1] + powerUp) + "}";
            Ally3Spec_text.text = "슈터 3 {공격력 : " + (AllySpec[2] + powerUp) + "}";
            Ally4Spec_text.text = "슈터 4 {공격력 : " + (AllySpec[3] + powerUp) + "}";
            Ally5Spec_text.text = "슈터 5 {공격력 : " + (AllySpec[4] + powerUp) + "}";
        }        
    }

    public void SpawnLevel5()
    {
        //ClearGame();
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
