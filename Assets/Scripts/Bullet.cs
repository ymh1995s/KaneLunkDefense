using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int dmg;
    public int[] dmgarr = new int[5];
    public string bulletName;
    private void OnEnable()
    {
        switch (bulletName)
        {
            case "A":
                dmgarr[0] = 3;
                dmg = dmgarr[0];
                break;
            case "B":
                dmgarr[1] = 5;
                dmg = dmgarr[1];
                break;
            case "C":
                dmgarr[2] = 15;
                dmg = dmgarr[2];
                break;
            case "D":
                dmgarr[3] = 20;
                dmg = dmgarr[3];
                break;
            case "E":
                dmgarr[4] = 30;
                dmg = dmgarr[4];
                break;
        }
    }
    private void Update()
    {
        dmg += 1;
        print(dmg);
        switch (bulletName)
        {
            case "A":
                dmg = dmgarr[0];
                break;
            case "B":
                dmg = dmgarr[1];
                break;
            case "C":
                dmg = dmgarr[2];
                break;
            case "D":
                dmg = dmgarr[3];
                break;
            case "E":
                dmg = dmgarr[4];
                break;
        }
    }
    void OnTriggerEnter2D(Collider2D collision) //충돌 발생 이벤트인듯?
    {
        if (collision.gameObject.tag == "Border_BulletDeleteLine")
        {
            gameObject.SetActive(false);
        }
    }
}
