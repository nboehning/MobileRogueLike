using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    GameObject enemyPrefab;

	private float EnemyOneSpawnSpeed = 1f;
    private float EnemyTwoSpawnSpeed = 1f;
    private float EnemyThreeSpawnSeed = 1f;

    private int numEnemyOneKilled;
    private int numEnemyTwoKilled;
    private int numEnemyThreeKiled;

    [HideInInspector]
	public int difficultyMultiplier;
    public gameDifficulty curDifficulty;
	// Use this for initialization
	void Start () 
	{

	    switch (curDifficulty)
	    {
	          case gameDifficulty.EASY:
	            EnemyOneSpawnSpeed = 1.5f;
                EnemyOneSpawnSpeed = 2.0f;
                EnemyOneSpawnSpeed = 3.0f;
                break;
              case gameDifficulty.MEDIUM:
                EnemyOneSpawnSpeed = 1.0f;
                EnemyOneSpawnSpeed = 1.5f;
                EnemyOneSpawnSpeed = 2.5f;

                break;
              case gameDifficulty.HARD:
                EnemyOneSpawnSpeed = 0.5f;
                EnemyOneSpawnSpeed = 1.0f;
                EnemyOneSpawnSpeed = 2.0f;
                break;
	    }


		enemyPrefab = Resources.Load ("Enemy") as GameObject;

		//Invoke("SpawnEnemy", Random.Range(spawnSpeed-spawnVariance, spawnSpeed+spawnVariance));
	
	}
	
	void SpawnEnemyOne()
	{
		GameObject tempEnemy = Instantiate (enemyPrefab,
			                       new Vector3 (Random.Range (2f, 8f), Random.Range (4f, -4f), 0f),
								   Quaternion.identity) as GameObject;
		
		//Invoke("SpawnEnemy", Random.Range(spawnSpeed-spawnVariance, spawnSpeed+spawnVariance));
	}

    void SpawnEnemyTwo()
    {
        
    }


}
