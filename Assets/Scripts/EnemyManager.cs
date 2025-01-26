using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;
    void Awake (){
        if (instance == null)instance = this;
        else Destroy (this);
    }

    public Enemy currentEnemy, enemyPrefab;
    public Transform enemySpawnPoint;
    void Update()
    {
        if (Core.running && currentEnemy == null && GameManager.instance.gameOver == false)
        {
            Enemy spawnedEnemy = Instantiate (enemyPrefab.gameObject, enemySpawnPoint.position, Quaternion.identity).GetComponent <Enemy>();
        }
    }

    public void SetEnemy (Enemy enemy){
        currentEnemy = enemy;
    }
}
