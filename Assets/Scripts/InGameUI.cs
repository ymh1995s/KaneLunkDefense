using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{

    public Text[] texts;
    public GameObject info;
    public Text BOSSHP_Text;
    public Image BOSSHP_Image;
    public GameObject BoSSInfo;
    public GameObject[] life;

    AudioSource theAudio;

    [SerializeField] AudioClip[] Audio_PowerUPSound;
    [SerializeField] AudioClip[] Audio_DrawSound;

    void Start()
    {
        theAudio = GetComponent<AudioSource>();

        texts = GetComponentsInChildren<Text>(true); //트루까지 넣어야 인액티브(보스체력) 까지 찾아냄
        texts[0].text = "뽑기[10]";          //뽑기
        texts[1].text = "공업[4]";           //파워업
        texts[2].text = "ReGame"; //재시작 버튼
        texts[3].text = "목숨"; //목숨(더미)
        texts[4].text = "20"; //돈(실제)
        texts[9].text = "8\n11\n15\n20\n30\n32"; //딜
        texts[10].text = "36%\n44%\n17%\n2.3%\n0.65%\n0.05%"; // 확률
        texts[11].text = (1) + "단계\n체력 : " + GameManager.instance.EnemySpecHP[0] + "\n" + "방어력 : " + GameManager.instance.EnemySpecArmor[0]; //적 스펙
    }

    void Update()
    {
        texts[4].text = GameManager.instance.money.ToString(); //돈(실제)
        if (GameManager.instance.life == 5) { }
        else if (GameManager.instance.life == 4) life[4].SetActive(false);
        else if (GameManager.instance.life == 3) life[3].SetActive(false); 
        else if (GameManager.instance.life == 2) life[2].SetActive(false); 
        else if (GameManager.instance.life == 1) life[1].SetActive(false);
    }

    public void InfoClick()
    {
        if(info.activeSelf==false) info.SetActive(true);
        else info.SetActive(false);
    }

    public void enemyLevelUp(int round)
    {
        texts[11].text = (round+1)+"단계\n체력 : "+ GameManager.instance.EnemySpecHP[round]+ "\n" + "방어력 : "+ GameManager.instance.EnemySpecArmor[round];
    }

    public void PowerUp()
    {
        if (GameManager.instance.money < GameManager.instance.PowerUpCost) return;
        else
        {
            PowerUPClickSound();
            GameManager.instance.money -= GameManager.instance.PowerUpCost;
            GameManager.instance.PowerUpCost += 2;
            GameManager.instance.powerUp += 2;
            texts[4].text = GameManager.instance.money.ToString(); //돈
            texts[1].text = "공업[" + GameManager.instance.PowerUpCost + "]";
            texts[9].text = (GameManager.instance.AllySpec[0] + GameManager.instance.powerUp) + "\n" +
                (GameManager.instance.AllySpec[1] + GameManager.instance.powerUp) + "\n" +
                (GameManager.instance.AllySpec[2] + GameManager.instance.powerUp) + "\n" +
                (GameManager.instance.AllySpec[3] + GameManager.instance.powerUp) + "\n" +
                (GameManager.instance.AllySpec[4] + GameManager.instance.powerUp) + "\n" +
                (GameManager.instance.AllySpec[5] + GameManager.instance.powerUp) + "\n";
        }
    }

    public void Draw()
    {
        if (GameManager.instance.money < 10) return;
        else
        {
            SpawnAlly();
            texts[4].text = GameManager.instance.money.ToString(); //돈
        }
    }

    public void Retry()
    {
        SceneManager.LoadScene(0);
    }

    void SpawnAlly()
    {

        float ypoint = Random.Range(-500, -100);
        float xpoint = 200; //= Random.Range(400, 800);
        float maxShotDelay = 0; //default1

        GameObject ally = null;
        int AllyLevel = Random.Range(0, 10000); //0~9999
        if( AllyLevel>9500) print(AllyLevel);

        GameManager.instance.money -= 10;

        Ally allyLogic = null; //일단 초기화

        if (AllyLevel < 3600)
        {
            xpoint = Random.Range(450, 490);
            maxShotDelay = Random.Range(800f, 900f) / 1000f;
            ally = GameManager.instance.objectManager.MakeObj("Ally1");
            allyLogic = ally.GetComponent<Ally>();
            allyLogic.bulletNo = "Bullet1";
            allyLogic.bulletspeed = 10;
            DrawClickSound(0);
        }
        else if (AllyLevel < 8000)
        {
            xpoint = Random.Range(530, 570);
            ally = GameManager.instance.objectManager.MakeObj("Ally2");
            maxShotDelay = Random.Range(600f, 750f) / 1000f;
            allyLogic = ally.GetComponent<Ally>();
            allyLogic.bulletNo = "Bullet2";
            allyLogic.bulletspeed = 12;
            DrawClickSound(0);
        }
        else if (AllyLevel < 9700)
        {
            xpoint = Random.Range(610, 650);
            ally = GameManager.instance.objectManager.MakeObj("Ally3");
            maxShotDelay = Random.Range(300f, 400f) / 1000f;
            allyLogic = ally.GetComponent<Ally>();
            allyLogic.bulletNo = "Bullet3";
            allyLogic.bulletspeed = 14;
            DrawClickSound(0);
        }
        else if (AllyLevel < 9930)
        {
            xpoint = Random.Range(720, 740);
            ally = GameManager.instance.objectManager.MakeObj("Ally4");
            maxShotDelay = Random.Range(125f, 200f) / 1000f;
            allyLogic = ally.GetComponent<Ally>();
            allyLogic.bulletNo = "Bullet4";
            allyLogic.bulletspeed = 18;
            DrawClickSound(1);
        }
        else if (AllyLevel < 9995)
        {
            xpoint = Random.Range(800, 820);
            ally = GameManager.instance.objectManager.MakeObj("Ally5");
            maxShotDelay = Random.Range(50f, 100f) / 1000f;
            allyLogic = ally.GetComponent<Ally>();
            allyLogic.bulletNo = "Bullet5";
            allyLogic.bulletspeed = 22;
            DrawClickSound(2);
        }
        //뉴 캐릭터 오메가 케인
        else if (AllyLevel < 10000)
        {
            ypoint = Random.Range(50, 80);
            xpoint = Random.Range(500,800);
            ally = GameManager.instance.objectManager.MakeObj("Ally6");
            maxShotDelay = Random.Range(125, 180) / 1000f;
            allyLogic = ally.GetComponent<Ally>();
            allyLogic.bulletNo = "Bullet6";
            allyLogic.bulletspeed = 12;
            DrawClickSound(3);
        }

        allyLogic.maxShotDelay = maxShotDelay;
        allyLogic.objectManager = GameManager.instance.objectManager;

        ally.transform.position = new Vector3(xpoint / 100, ypoint / 100, 0);
    }

    public void DrawClickSound(int SoundNo)
    {
        /* 데모버전을 위한 주석
        float soundVolumn = 1.0f;
        if (SoundNo != 0) soundVolumn = 0.5f; //뽈롱만 키우고 나머지는 작게
        theAudio.PlayOneShot(Audio_DrawSound[SoundNo], soundVolumn);
        */
    }

    public void PowerUPClickSound()
    {
        /* 데모버전을 위한 주석
        int SoundNo = Random.Range(0, 4);
        float soundVolumn = 0.5f;
        if (SoundNo == 0) soundVolumn = 0.25f; //가람이 시계
        theAudio.PlayOneShot(Audio_PowerUPSound[SoundNo], soundVolumn);
        */
    }
}
