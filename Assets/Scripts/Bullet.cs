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
                dmg = GameManager.instance.AllySpec[0];
                break;
            case "B":
                dmg = GameManager.instance.AllySpec[1];
                break;
            case "C":
                dmg = GameManager.instance.AllySpec[2];
                break;
            case "D":
                dmg = GameManager.instance.AllySpec[3];
                break;
            case "E":
                dmg = GameManager.instance.AllySpec[4];
                break;
            case "F":
                dmg = GameManager.instance.AllySpec[5];
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
