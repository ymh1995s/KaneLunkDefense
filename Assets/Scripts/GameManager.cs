using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject[] enemyObjs;
    public Transform[] spawnPoints;

    public ObjectManager objectManager;


    public float curSpawnDelay;
    public float maxSpawnDelay;

    private float time_start=0;
    private float time_current;

    public int round = 0;

    // Start is called before the first frame update
    void Start()
    {
        //
    }

    // Update is called once per frame
    void Update()
    {
        Check_Timer();

        curSpawnDelay += Time.deltaTime;

        print(curSpawnDelay);
        print(maxSpawnDelay);
        if (curSpawnDelay>maxSpawnDelay)
        {
            SpawnEnemy();
            SpawnAlly();
            maxSpawnDelay = Random.Range(0.5f, 3f);
            curSpawnDelay = 0;
        }
        
    }

    void SpawnEnemy()
    {
        int ranPoint = Random.Range(0, 8);
        GameObject enemy = objectManager.MakeObj("Enemy"+round);
        enemy.transform.position = spawnPoints[ranPoint].position;
        Rigidbody2D rigid = enemy.GetComponent<Rigidbody2D>();
        rigid.velocity = new Vector2(3, 0);
    }


    void SpawnAlly()
    {

        float ypoint = Random.Range(-500, 0);
        float xpoint = 200; //= Random.Range(400, 800);
        GameObject ally = null;
        int AllyLevel = Random.Range(0, 5); //0~4

        switch (AllyLevel)
        {
            case 0:
                xpoint = Random.Range(400, 450);
                ally = objectManager.MakeObj("Ally1");
                break;
            case 1:
                xpoint = Random.Range(500, 550);
                ally = objectManager.MakeObj("Ally2");
                break;
            case 2:
                xpoint = Random.Range(600, 650);
                ally = objectManager.MakeObj("Ally3");
                break;
            case 3:
                xpoint = Random.Range(700, 750);
                ally = objectManager.MakeObj("Ally4");
                break;
            case 4:
                xpoint = Random.Range(800, 850);
                ally = objectManager.MakeObj("Ally5");
                break;
        }

        ally.transform.position = new Vector3(xpoint/100, ypoint / 100, 0);

        Ally allyLogic = ally.GetComponent<Ally>();
        allyLogic.gameManager = this;
        allyLogic.objectManager = objectManager;
    }


    private void Check_Timer()
    {
        time_current = Time.time- time_start;
        if (time_current >10)
        {
            objectManager.level++;
            round++;
            time_start = Time.time;
        }
    }
}
