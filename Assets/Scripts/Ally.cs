using System.Collections;
using System.Collections.Generic;
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
        if (curShotDelay < maxShotDelay)
        {
            return;
        }
        //여기에 ally 레밸 별로 fire 다르게 나가는 로직 쳐 넣어야함
        bulletHead(objectManager.enemy1);
        bulletHead(objectManager.enemy2);
        bulletHead(objectManager.enemy3);
        bulletHead(objectManager.enemy4);
        bulletHead(objectManager.enemy5);
    }

    void bulletHead(GameObject[] targetPos)
    {
        for (int i = 0; i < targetPos.Length; i++)
        {
            if (targetPos[i].activeSelf)
            {

                GameObject bullet = objectManager.MakeObj(bulletNo);
                bullet.transform.position = transform.position;
                Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();

                Vector3 dirVec = (targetPos[i].transform.position) - (transform.position + Vector3.left * 0.3f); //목표물로 방향 = 목표물 위치 - 자신의 위치

                rigid.AddForce(dirVec.normalized * bulletspeed, ForceMode2D.Impulse);

                curShotDelay = 0;
                return;
            }
        }
    }


    void Reload()
    {
        curShotDelay += Time.deltaTime;
    }
}
