using UnityEngine;

public class Spawn : MonoBehaviour
{
    public bool isABossSpawn = false;
    public bool isBBossSpawn = false;
    public bool isLade = false;

    public void SpawnEnemy(int round)
    {
        if(!isLade)
        {
            int ranPoint = Random.Range(0, 8);
            string enemyround = "Enemy" + round;
            GameObject enemy = GameManager.instance.objectManager.MakeObj(enemyround);
            enemy.transform.position = GameManager.instance.spawnPoints[ranPoint].position;
            Rigidbody2D rigid = enemy.GetComponent<Rigidbody2D>();

            if (enemyround == "Enemy4"&& !isABossSpawn) //¤·¤¶
            {
                rigid.velocity = new Vector2(0.2f, 0);
                isLade = true;
                isABossSpawn = true;
                enemy.transform.position = GameManager.instance.spawnPoints[0].position;
                GameManager.instance.inGameUI.BoSSInfo.SetActive(true);
            }
            else if (enemyround == "Enemy7" && !isBBossSpawn) //Å¸ÇÏ
            {
                rigid.velocity = new Vector2(0.2f, 0);
                isLade = true;
                isBBossSpawn = true;
                enemy.transform.position = GameManager.instance.spawnPoints[0].position;
                GameManager.instance.inGameUI.BoSSInfo.SetActive(true);
            }
            else rigid.velocity = new Vector2(1, 0);

            Enemy enemyLogic = enemy.GetComponent<Enemy>();
            enemyLogic.isAlive = true;
        }       
    }
}
