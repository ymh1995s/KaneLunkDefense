using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
    int[] AllySpec = new int[5] { 8, 11, 15, 20, 30 };
    int[] EnemySpecHP = new int[5] { 20, 375, 900, 1800, 200000 };
    int[] EnemySpecArmor = new int[5] { 0, 5, 10, 20, 30 };

    public Text[] texts;

    AudioSource theAudio;

    [SerializeField] AudioClip[] Audio_PowerUPSound;
    [SerializeField] AudioClip[] Audio_DrawSound;

    void Start()
    {
        theAudio = GetComponent<AudioSource>();

        texts = GetComponentsInChildren<Text>(true); //트루까지 넣어야 인액티브(보스체력) 까지 찾아냄
        texts[0].text = "뽑기 조이고[10]";          //뽑기
        texts[1].text = "공업 조이고[4]";           //파워업
        texts[2].text = "슈터 1 {공격력 : " + (AllySpec[0] ) + "}";
        texts[3].text = "슈터 2 {공격력 : " + (AllySpec[1] ) + "}";
        texts[4].text = "슈터 3 {공격력 : " + (AllySpec[2] ) + "}";
        texts[5].text = "슈터 4 {공격력 : " + (AllySpec[3] ) + "}";
        texts[6].text = "슈터 5 {공격력 : " + (AllySpec[4] ) + "}";
        texts[7].text = (1) + "단계 {체력 : " + EnemySpecHP[0] + "} " + "{방어력 : " + EnemySpecArmor[0] + "}"; //적 스펙
        texts[8].text = "돈"; //돈(더미)
        texts[9].text = "20"; //돈(실제)
        texts[10].text = "목슘"; //목숨(더미)
        texts[11].text = "5"; //목숨(실제)
        texts[12].text = "유 썩 체력 기입"; // ㅇㅆ
        texts[13].text = "쭈꾸다시"; //재시작 버튼
    }

    void Update()
    {
        texts[9].text = GameManager.instance.money.ToString(); //돈(실제)
        texts[11].text = GameManager.instance.life.ToString(); //목숨(실제)
    }

    public void enemyLevelUp(int round)
    {
        texts[7].text = (round+1)+"단계 {체력 : "+ EnemySpecHP[round]+ "} " + "{방어력 : "+ EnemySpecArmor[round] + "}";
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
            texts[9].text = GameManager.instance.money.ToString(); //돈
            texts[1].text = "공업 조이고[" + GameManager.instance.PowerUpCost + "]";
            texts[2].text = "슈터 1 {공격력 : " + (AllySpec[0] + GameManager.instance.powerUp) + "}";
            texts[3].text = "슈터 2 {공격력 : " + (AllySpec[1] + GameManager.instance.powerUp) + "}";
            texts[4].text = "슈터 3 {공격력 : " + (AllySpec[2] + GameManager.instance.powerUp) + "}";
            texts[5].text = "슈터 4 {공격력 : " + (AllySpec[3] + GameManager.instance.powerUp) + "}";
            texts[6].text = "슈터 5 {공격력 : " + (AllySpec[4] + GameManager.instance.powerUp) + "}";
        }
    }

    public void Draw()
    {
        if (GameManager.instance.money < 10) return;
        else
        {
            SpawnAlly();
            texts[9].text = GameManager.instance.money.ToString(); //돈
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
        int AllyLevel = Random.Range(0, 10000); //0~4

        GameManager.instance.money -= 10;

        Ally allyLogic = null; //일단 초기화맨

        if (AllyLevel < 3600)
        {
            xpoint = Random.Range(450, 490);
            maxShotDelay = Random.Range(850f, 1100f) / 1000f;
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
            maxShotDelay = Random.Range(650f, 800f) / 1000f;
            allyLogic = ally.GetComponent<Ally>();
            allyLogic.bulletNo = "Bullet2";
            allyLogic.bulletspeed = 12;
            DrawClickSound(0);
        }
        //else if (AllyLevel < 9700)
        else if (AllyLevel < 8500)
        {
            xpoint = Random.Range(610, 650);
            ally = GameManager.instance.objectManager.MakeObj("Ally3");
            maxShotDelay = Random.Range(400f, 600f) / 1000f;
            allyLogic = ally.GetComponent<Ally>();
            allyLogic.bulletNo = "Bullet3";
            allyLogic.bulletspeed = 14;
            DrawClickSound(0);
        }
        //else if (AllyLevel < 9930)
        else if (AllyLevel < 8800)
        {
            xpoint = Random.Range(720, 740);
            ally = GameManager.instance.objectManager.MakeObj("Ally4");
            maxShotDelay = Random.Range(125f, 250f) / 1000f;
            allyLogic = ally.GetComponent<Ally>();
            allyLogic.bulletNo = "Bullet4";
            allyLogic.bulletspeed = 18;
            DrawClickSound(1);
        }
        //else if (AllyLevel < 10000)
        else if (AllyLevel < 9500)
        {
            xpoint = Random.Range(800, 820);
            ally = GameManager.instance.objectManager.MakeObj("Ally5");
            maxShotDelay = Random.Range(50f, 100f) / 1000f;
            allyLogic = ally.GetComponent<Ally>();
            allyLogic.bulletNo = "Bullet5";
            allyLogic.bulletspeed = 22;
            DrawClickSound(2);
        }

        allyLogic.maxShotDelay = maxShotDelay;
        allyLogic.objectManager = GameManager.instance.objectManager;
        ally.transform.position = new Vector3(xpoint / 100, ypoint / 100, 0);
    }

    public void DrawClickSound(int SoundNo)
    {
        float soundVolumn = 1.0f;
        if (SoundNo != 0) soundVolumn = 0.5f; //뽈롱만 키우고 나머지는 작게
        theAudio.PlayOneShot(Audio_DrawSound[SoundNo], soundVolumn);
        //AudioSource.PlayClipAtPoint(Audio_DrawSound[SoundNo], transform.position,100); //1회성 효과음용,재생되면 알아서 삭제됨 메모리 이용 개꿀
    }

    public void PowerUPClickSound()
    {
        int SoundNo = Random.Range(0, 4);
        float soundVolumn = 0.5f;
        if (SoundNo == 0) soundVolumn = 0.25f; //가람이 시계
        theAudio.PlayOneShot(Audio_PowerUPSound[SoundNo], soundVolumn);
        //AudioSource.PlayClipAtPoint(Audio_PowerUPSound[SoundNo], transform.position, soundVolumn); //1회성 효과음용,재생되면 알아서 삭제됨 메모리 이용 개꿀
    }

}
