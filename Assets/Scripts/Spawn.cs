using UnityEngine;

public class Spawn : MonoBehaviour
{
    bool isBossSpawn = false;

    public void SpawnEnemy(int round)
    {
        if (!isBossSpawn)
        {
            int ranPoint = Random.Range(0, 8);
            string enemyround = "Enemy" + round;
            GameObject enemy = GameManager.instance.objectManager.MakeObj(enemyround);
            enemy.transform.position = GameManager.instance.spawnPoints[ranPoint].position;
            Rigidbody2D rigid = enemy.GetComponent<Rigidbody2D>();
            if (enemyround == "Enemy4") //¾ê°¡ º¸½ºÀÓ
            {
                GameManager.instance.inGameUI.texts[12].gameObject.SetActive(true);
                rigid.velocity = new Vector2(0.2f, 0);
                isBossSpawn = true;
            }
            else rigid.velocity = new Vector2(1, 0);

            Enemy enemyLogic = enemy.GetComponent<Enemy>();
            enemyLogic.isAlive = true;
            enemyLogic.spriter.color = new Color(255, 255, 255, 255);
        }
    }
}
