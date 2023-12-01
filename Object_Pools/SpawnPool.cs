using System.Collections.Generic;
using UnityEngine;

public class SpawnPool : MonoBehaviour
{
    public static SpawnPool instance;
    [SerializeField] private EnemyController _enemyPrefab;
    private Queue<EnemyController> enemyPool = new Queue<EnemyController>();
    [SerializeField] private int enemyPoolSize = 30;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < enemyPoolSize; i++)
        {
            EnemyController enemy = Instantiate(_enemyPrefab);
            enemy.gameObject.SetActive(false);
            enemyPool.Enqueue(enemy);
        }
    }

    public EnemyController GetFromPool()
    {
        if (enemyPool.Count > 0)
        {
            EnemyController enemy = enemyPool.Dequeue();
            enemy.transform.position = Vector3.zero; // You can set the initial position you want here
            enemy.transform.rotation = Quaternion.identity; // Reset rotation
            enemy.transform.localScale = Vector3.one;
            enemy.gameObject.SetActive(true);
            return enemy;
        }
        else
        {
            EnemyController enemy = Instantiate(_enemyPrefab);
            return enemy;
        }
    }

    public void ReturnToPool(EnemyController enemy)
    {
        enemy.transform.position = Vector3.zero; // You can set the initial position you want here
        enemy.transform.rotation = Quaternion.identity; // Reset rotation
        enemy.transform.localScale = Vector3.one; // Reset scale

        enemy.gameObject.SetActive(false);
        enemyPool.Enqueue(enemy);
    }
}