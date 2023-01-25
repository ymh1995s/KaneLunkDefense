using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int dmg;
    public int plusdmg=0;
    public string bulletName;

    private void OnEnable()
    {
        switch (bulletName)
        {
            case "A":
                dmg = 8;
                break;
            case "B":
                dmg = 11;
                break;
            case "C":
                dmg = 15;
                break;
            case "D":
                dmg = 20;
                break;
            case "E":
                dmg = 30;
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
