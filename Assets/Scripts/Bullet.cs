using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int dmg;

    void OnTriggerEnter2D(Collider2D collision) //�浹 �߻� �̺�Ʈ�ε�?
    {
        if (collision.gameObject.tag == "Border_BulletDeleteLine")
        {
            gameObject.SetActive(false);
        }
    }
}
