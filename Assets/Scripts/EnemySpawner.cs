using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    private GameObject enemyOnePrefab;
    private GameObject enemyTwoPrefab;
    private GameObject enemyThreePrefab;

	private float EnemyOneSpawnSpeed = 1f;
    private float EnemyTwoSpawnSpeed = 1f;
    private float EnemyThreeSpawnSeed = 1f;

    private int numEnemyOneKilled;
    private int numEnemyTwoKilled;
    private int numEnemyThreeKiled;


    private List<GameObject> enemies = new List<GameObject>();

    [HideInInspector]
	public int difficultyMultiplier;
    public gameDifficulty curDifficulty;

    private Vector2 minValues;
    private Vector2 maxValues;
	// Use this for initialization
	void Start () 
	{
        switch (curDifficulty)
	    {
	          case gameDifficulty.EASY:
	            EnemyOneSpawnSpeed = 1.5f;
                EnemyTwoSpawnSpeed = 2.0f;
                EnemyThreeSpawnSeed = 3.0f;
                break;
              case gameDifficulty.MEDIUM:
                EnemyOneSpawnSpeed = 1.0f;
                EnemyTwoSpawnSpeed = 1.5f;
                EnemyThreeSpawnSeed = 2.5f;

                break;
              case gameDifficulty.HARD:
                EnemyOneSpawnSpeed = 0.5f;
                EnemyTwoSpawnSpeed = 1.0f;
                EnemyThreeSpawnSeed = 2.0f;
                break;
	    }
	    minValues = Camera.main.GetComponent<CameraController>().minValues;
	    maxValues = Camera.main.GetComponent<CameraController>().maxValues;

		enemyOnePrefab = Resources.Load ("EnemyOne") as GameObject;
        enemyTwoPrefab = Resources.Load("EnemyTwo") as GameObject;

		Invoke("SpawnEnemyOne", EnemyOneSpawnSpeed);
	    Invoke("SpawnEnemyTwo", EnemyTwoSpawnSpeed);
	}
	
	void SpawnEnemyOne()
	{
		GameObject tempEnemy = Instantiate (enemyOnePrefab, new Vector3 (Random.Range (minValues.x + 0.32f, maxValues.x - 0.32f), 
                                Random.Range (minValues.y + 0.32f, maxValues.y - 0.32f), 0f), Quaternion.identity) as GameObject;
	    enemies.Add(tempEnemy);
        Invoke("SpawnEnemyOne", EnemyOneSpawnSpeed);
    }

    void SpawnEnemyTwo()
    {
        GameObject tempEnemy = Instantiate(enemyTwoPrefab, new Vector3(Random.Range(minValues.x + 0.32f, maxValues.x - 0.32f),
                                Random.Range(minValues.y + 0.32f, maxValues.y - 0.32f), 0f), Quaternion.identity) as GameObject;
        Debug.Log(tempEnemy.transform.position);
        enemies.Add(tempEnemy);
        Invoke("SpawnEnemyTwo", EnemyTwoSpawnSpeed);
    }

    void SpawnEnemyThree()
    {

    }

    public void IncrementNumOneKilled()
    {
        numEnemyOneKilled++;
        if (numEnemyOneKilled%10 == 0)
        {
            Debug.Log("Enemy One spawns faster");
            EnemyOneSpawnSpeed -= .05f;
        }
    }

    public void IncrementNumTwoKilled()
    {
        numEnemyTwoKilled++;
        if (numEnemyTwoKilled%10 == 0)
        {
            EnemyTwoSpawnSpeed -= .03f;
        }
    }

    public void IncrementNumThreeKilled()
    {
        numEnemyThreeKiled++;
        if (numEnemyThreeKiled%10 == 0)
        {
            EnemyThreeSpawnSeed -= .025f;
        }
    }

    public void DestroyEnemies()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            Destroy(enemies[i]);
        }
    }
}
