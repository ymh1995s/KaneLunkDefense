using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int dmg;

    void OnTriggerEnter2D(Collider2D collision) //충돌 발생 이벤트인듯?
    {
        if (collision.gameObject.tag == "Border_BulletDeleteLine")
        {
            gameObject.SetActive(false);
        }
    }
}
