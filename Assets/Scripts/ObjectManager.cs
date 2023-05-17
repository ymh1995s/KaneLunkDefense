using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{

    [Header("프리펩")]
    public GameObject Ally1Prefab;
    public GameObject Ally2Prefab;
    public GameObject Ally3Prefab;
    public GameObject Ally4Prefab;
    public GameObject Ally5Prefab;
    public GameObject Enemy1Prefab;
    public GameObject Enemy2Prefab;
    public GameObject Enemy3Prefab;
    public GameObject Enemy4Prefab;
    public GameObject Enemy5Prefab;
    public GameObject Bullet1Prefab;
    public GameObject Bullet2Prefab;
    public GameObject Bullet3Prefab;
    public GameObject Bullet4Prefab;
    public GameObject Bullet5Prefab;

    [Header("오브젝트 풀")]
    GameObject[] targetPool;
    public GameObject[] ally1;
    public GameObject[] ally2;
    public GameObject[] ally3;
    public GameObject[] ally4;
    public GameObject[] ally5;
    public GameObject[] enemy1;
    public GameObject[] enemy2;
    public GameObject[] enemy3;
    public GameObject[] enemy4;
    public GameObject[] enemy5;
    public GameObject[] bullet1;
    public GameObject[] bullet2;
    public GameObject[] bullet3;
    public GameObject[] bullet4;
    public GameObject[] bullet5;    

    [Header("오브젝트 풀의 위치")]
    [SerializeField]
    Transform tfPoolParent;

    //public int level;

    private void Awake()
    {
        ally1 = new GameObject[200];
        ally2 = new GameObject[200];
        ally3 = new GameObject[100];
        ally4 = new GameObject[50];
        ally5 = new GameObject[10];
        enemy1 = new GameObject[50];
        enemy2 = new GameObject[50];
        enemy3 = new GameObject[50];
        enemy4 = new GameObject[50];
        enemy5 = new GameObject[3];
        bullet1 = new GameObject[500];
        bullet2 = new GameObject[500];
        bullet3 = new GameObject[500];
        bullet4 = new GameObject[500];
        bullet5 = new GameObject[500];

        Generate();
    }

    public void Generate()
    {
        for (int index = 0; index < ally1.Length; index++)
        {
            ally1[index] = Instantiate(Ally1Prefab);
            ally1[index].SetActive(false);
        }

        for (int index = 0; index < ally2.Length; index++)
        {
            ally2[index] = Instantiate(Ally2Prefab);
            ally2[index].SetActive(false);
        }

        for (int index = 0; index < ally3.Length; index++)
        {
            ally3[index] = Instantiate(Ally3Prefab);
            ally3[index].SetActive(false);
        }

        for (int index = 0; index < ally4.Length; index++)
        {
            ally4[index] = Instantiate(Ally4Prefab);
            ally4[index].SetActive(false);
        }

        for (int index = 0; index < ally5.Length; index++)
        {
            ally5[index] = Instantiate(Ally5Prefab);
            ally5[index].SetActive(false);
        }

        for (int index = 0; index < enemy1.Length; index++)
        {
            enemy1[index] = Instantiate(Enemy1Prefab);
            enemy1[index].SetActive(false);
        }

        for (int index = 0; index < enemy2.Length; index++)
        {
            enemy2[index] = Instantiate(Enemy2Prefab);
            enemy2[index].SetActive(false);
        }

        for (int index = 0; index < enemy3.Length; index++)
        {
            enemy3[index] = Instantiate(Enemy3Prefab);
            enemy3[index].SetActive(false);
        }

        for (int index = 0; index < enemy4.Length; index++)
        {
            enemy4[index] = Instantiate(Enemy4Prefab);
            enemy4[index].SetActive(false);
        }

        for (int index = 0; index < enemy5.Length; index++)
        {
            enemy5[index] = Instantiate(Enemy5Prefab);
            enemy5[index].SetActive(false);
        }

        for (int index = 0; index < bullet1.Length; index++)
        {
            bullet1[index] = Instantiate(Bullet1Prefab);
            bullet1[index].SetActive(false);
            bullet1[index].transform.SetParent(tfPoolParent);
        }
        for (int index = 0; index < bullet2.Length; index++)
        {
            bullet2[index] = Instantiate(Bullet2Prefab);
            bullet2[index].SetActive(false);
            bullet2[index].transform.SetParent(tfPoolParent);
        }
        for (int index = 0; index < bullet3.Length; index++)
        {
            bullet3[index] = Instantiate(Bullet3Prefab);
            bullet3[index].SetActive(false);
            bullet3[index].transform.SetParent(tfPoolParent);
        }
        for (int index = 0; index < bullet4.Length; index++)
        {
            bullet4[index] = Instantiate(Bullet4Prefab);
            bullet4[index].SetActive(false);
            bullet4[index].transform.SetParent(tfPoolParent);
        }
        for (int index = 0; index < bullet5.Length; index++)
        {
            bullet5[index] = Instantiate(Bullet5Prefab);
            bullet5[index].SetActive(false);
            bullet5[index].transform.SetParent(tfPoolParent);
        }
    }


    public GameObject MakeObj(string type)
    {
        switch (type)
        {
            case "Ally1":
                targetPool = ally1;
                break;
            case "Ally2":
                targetPool = ally2;
                break;
            case "Ally3":
                targetPool = ally3;
                break;
            case "Ally4":
                targetPool = ally4;
                break;
            case "Ally5":
                targetPool = ally5;
                break;
            case "Enemy0":
                targetPool = enemy1;
                break;
            case "Enemy1":
                targetPool = enemy2;
                break;
            case "Enemy2":
                targetPool = enemy3;
                break;
            case "Enemy3":
                targetPool = enemy4;
                break;
            case "Enemy4":
                targetPool = enemy5;
                break;
            case "Bullet1":
                targetPool = bullet1;
                break;
            case "Bullet2":
                targetPool = bullet2;
                break;
            case "Bullet3":
                targetPool = bullet3;
                break;
            case "Bullet4":
                targetPool = bullet4;
                break;
            case "Bullet5":
                targetPool = bullet5;
                break;
        }

        for (int index = 0; index < targetPool.Length; index++)
        {
            if (!targetPool[index].activeSelf)
            {
                targetPool[index].SetActive(true);
                return targetPool[index];
            }
        }

        return null;
    }


}
