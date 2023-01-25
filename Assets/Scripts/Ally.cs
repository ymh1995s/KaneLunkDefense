using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Ally : MonoBehaviour
{

    public float maxShotDelay;
    public float curShotDelay;
    public ObjectManager objectManager;
    public GameManager gameManager;
    public string bulletNo;
    public int bulletspeed;

    public GameObject enemytest;

    bool isshoouting=false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Fire();
        Reload();
    }

    void Fire()
    {
        isshoouting = false;
        if (curShotDelay < maxShotDelay)
        {
            return;
        }
        if (!isshoouting)
            bulletHead(objectManager.enemy1);
        if (!isshoouting)
            bulletHead(objectManager.enemy2);
        if (!isshoouting)
            bulletHead(objectManager.enemy3);
        if (!isshoouting)
            bulletHead(objectManager.enemy4);
        if (!isshoouting)
            bulletHead(objectManager.enemy5);
    }

    void bulletHead(GameObject[] targetPos)
    {
        float[] EnemyXValue = new float[targetPos.Length];
        for (int i = 0; i < targetPos.Length; i++)
        {
            if (!targetPos[i].activeSelf)
            {
                EnemyXValue[i] = -100;
                continue;
            }
            EnemyXValue[i] = targetPos[i].transform.position.x;
        }

        float maxValue = EnemyXValue.Max(); // ���� �տ� Ÿ����
        int maxIndex = EnemyXValue.ToList().IndexOf(maxValue);

        if (!targetPos[maxIndex].activeSelf) return; //�־���� �ٸ� ���� �ִϹ����״� �Ƚ�

        GameObject bullet = objectManager.MakeObj(bulletNo);
        bullet.transform.position = transform.position;
        Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
        Vector3 dirVec = (targetPos[maxIndex].transform.position) - (transform.position + Vector3.left * 0.3f); //��ǥ���� ���� = ��ǥ�� ��ġ - �ڽ��� ��ġ
        rigid.AddForce(dirVec.normalized * bulletspeed, ForceMode2D.Impulse);
        curShotDelay = 0;
        isshoouting = true;
    }

    void Reload()
    {
        curShotDelay += Time.deltaTime;
    }
}
