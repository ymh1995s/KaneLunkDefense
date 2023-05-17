using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("# 타 스크립트 객체")]
    public ObjectManager objectManager;
    public Spawn spawner;
    public EnemySFX enemySFX;

    [Header("# Player")]
    public int money;
    public int life;
    public int PowerUpCost;
    public int powerUp;

    [Header("# UI")]
    public InGameUI inGameUI;
    public StartUI startUI;
    public GameOverUI gameOverUI;
    public GameClearUI gameClearUI;

    [Header("# BGM")]
    public AudioClip[] Audio_BGM;
    
    [Header("# 게임 진행 로직 관련")]
    public float time_start;
    public float time_current;
    public bool isGameStart = false;
    public bool isGameClear = false;

    [Header("# Spawn Point 묶음")]
    public Transform[] spawnPoints;

    [Header("# ETC")]
    public static GameManager instance;

    [Header("# 이하 Private")]
    int round=0;

    float curSpawnDelay;
    float maxSpawnDelay;

    AudioSource theAudio;

    void Start()
    {
        theAudio = GetComponent<AudioSource>();
        gameOverUI.Start();
        gameClearUI.Start();
    }

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if (!isGameStart) theAudio.clip = Audio_BGM[0]; //뭉탱이 월드
        else theAudio.clip = Audio_BGM[1]; //뭉탱이 서바이버

        if (!theAudio.isPlaying)
        {
            theAudio.volume = 0.4f;
            theAudio.Play();
        }

        if (!isGameStart) return;

        if (!isGameClear)
        {
            Check_Timer();

            curSpawnDelay += Time.deltaTime;

            if (curSpawnDelay > maxSpawnDelay)
            {
                spawner.SpawnEnemy(round);

                maxSpawnDelay = Random.Range(0.2f, 1f);
                curSpawnDelay = 0;
            }
        }
    }

    public void ClearGame()
    {
        gameClearUI.ClearGame();
    }

    public void OverGame()
    {
        gameOverUI.OverGame();
    }

    private void Check_Timer()
    {
        time_current = Time.time- time_start;
        if (time_current > 5)
        {
            //objectManager.level++;
            round++;
            time_start = Time.time;
            inGameUI.enemyLevelUp(round);
        }
    }

    public void DebugBtn5()
    {
        float ypoint = Random.Range(-500, 0);
        float xpoint = 200; //= Random.Range(400, 800);
        float maxShotDelay = 0; //default1

        GameObject ally = null;

        Ally allyLogic = null; //일단 초기화맨

        xpoint = Random.Range(800, 820);
        ally = GameManager.instance.objectManager.MakeObj("Ally5");
        maxShotDelay = Random.Range(50f, 100f) / 1000f;
        allyLogic = ally.GetComponent<Ally>();
        allyLogic.bulletNo = "Bullet5";
        allyLogic.bulletspeed = 22;

        allyLogic.maxShotDelay = maxShotDelay;
        allyLogic.objectManager = GameManager.instance.objectManager;
        ally.transform.position = new Vector3(xpoint / 100, ypoint / 100, 0);
    }
}
