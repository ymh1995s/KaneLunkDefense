using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Ally : MonoBehaviour
{
    [Header("# 타 스크립트 객체")]
    public ObjectManager objectManager;
    public GameObject enemytest;

    [Header("# Ally Info")]
    public float maxShotDelay;
    public float curShotDelay;
    public string bulletNo;
    public int bulletspeed;
    bool isshoouting=false; //재장전


    // Update is called once per frame
    void Update()
    {
        Fire();
        Reload();
    }

    void Fire()
    {
        isshoouting = false;
        if (curShotDelay < maxShotDelay)  return; 
        if (!isshoouting) bulletHead(GameManager.instance.objectManager.enemy1);
        if (!isshoouting) bulletHead(GameManager.instance.objectManager.enemy2);
        if (!isshoouting) bulletHead(GameManager.instance.objectManager.enemy3);
        if (!isshoouting) bulletHead(GameManager.instance.objectManager.enemy4);
        if (!isshoouting) bulletHead(GameManager.instance.objectManager.enemy5);
        if (!isshoouting) bulletHead(GameManager.instance.objectManager.enemy6);
        if (!isshoouting) bulletHead(GameManager.instance.objectManager.enemy7);
        if (!isshoouting) bulletHead(GameManager.instance.objectManager.enemy8);
    }

    //적 타겟팅 알고리즘 - 맨 앞 열 우선 사격
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

        float maxValue = EnemyXValue.Max(); // 가장 앞열 타게팅
        int maxIndex = EnemyXValue.ToList().IndexOf(maxValue);

        if (!targetPos[maxIndex].activeSelf) return; //넣어줘야 다른 레밸 애니미한테는 안쏨

        if (bulletNo == "Bullet6")
        {
            GameObject bullet1 = GameManager.instance.objectManager.MakeObj(bulletNo);
            GameObject bullet2 = GameManager.instance.objectManager.MakeObj(bulletNo);
            GameObject bullet3 = GameManager.instance.objectManager.MakeObj(bulletNo);
            GameObject bullet4 = GameManager.instance.objectManager.MakeObj(bulletNo);

            bullet1.transform.position = transform.position + Vector3.left * 1 + Vector3.up * 0.5f;
            bullet2.transform.position = transform.position + Vector3.right * 1 - Vector3.up * 0.5f;
            bullet3.transform.position = transform.position + Vector3.left * 1 + Vector3.down * 0.5f;
            bullet4.transform.position = transform.position + Vector3.right * 1 - Vector3.down * 0.5f;

            Rigidbody2D rigid1 = bullet1.GetComponent<Rigidbody2D>();
            Rigidbody2D rigid2 = bullet2.GetComponent<Rigidbody2D>();
            Rigidbody2D rigid3 = bullet3.GetComponent<Rigidbody2D>();
            Rigidbody2D rigid4 = bullet4.GetComponent<Rigidbody2D>();

            Vector3 dirVec1 = (targetPos[maxIndex].transform.position) - (transform.position + Vector3.left * 0.3f); //목표물로 방향 = 목표물 위치 - 자신의 위치
            Vector3 dirVec2 = (targetPos[maxIndex].transform.position) - (transform.position + Vector3.left * 0.3f); //목표물로 방향 = 목표물 위치 - 자신의 위치
            Vector3 dirVec3 = (targetPos[maxIndex].transform.position) - (transform.position + Vector3.left * 0.3f); //목표물로 방향 = 목표물 위치 - 자신의 위치
            Vector3 dirVec4 = (targetPos[maxIndex].transform.position) - (transform.position + Vector3.left * 0.3f); //목표물로 방향 = 목표물 위치 - 자신의 위치

            rigid1.AddForce(dirVec1.normalized * bulletspeed, ForceMode2D.Impulse);
            rigid2.AddForce(dirVec2.normalized * bulletspeed, ForceMode2D.Impulse);
            rigid3.AddForce(dirVec3.normalized * bulletspeed, ForceMode2D.Impulse);
            rigid4.AddForce(dirVec4.normalized * bulletspeed, ForceMode2D.Impulse);
            curShotDelay = 0;
            isshoouting = true;
            return;
        }
        GameObject bullet = GameManager.instance.objectManager.MakeObj(bulletNo);
        bullet.transform.position = transform.position;
        Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
        Vector3 dirVec = (targetPos[maxIndex].transform.position) - (transform.position + Vector3.left * 0.3f); //목표물로 방향 = 목표물 위치 - 자신의 위치
        rigid.AddForce(dirVec.normalized * bulletspeed, ForceMode2D.Impulse);
        curShotDelay = 0;
        isshoouting = true;
    }

    void Reload()
    {
        curShotDelay += Time.deltaTime;
    }
}
