using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

	GameObject enemyPrefab;

	float spawnSpeed = 1f;
	float spawnVariance = 0.5f;

	// Use this for initialization
	void Start () 
	{		
		enemyPrefab = Resources.Load ("Enemy") as GameObject;

		Invoke("SpawnEnemy", Random.Range(spawnSpeed-spawnVariance, spawnSpeed+spawnVariance));
	
	}
	
	void SpawnEnemy()
	{
		GameObject tempEnemy = Instantiate (enemyPrefab,
			                       new Vector3 (Random.Range (2f, 8f), Random.Range (4f, -4f), 0f),
								   Quaternion.identity) as GameObject;
		
		Invoke("SpawnEnemy", Random.Range(spawnSpeed-spawnVariance, spawnSpeed+spawnVariance));
	}
}
