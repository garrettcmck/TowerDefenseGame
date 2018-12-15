using System.Collections;
using System.Collections.Generic;
using UnityEngine;


   
public class LevelManager : MonoBehaviour
{
    public List<GameObject> enemies;    // List in spawn order
    public List<GameObject> path;
    
    private void Awake()
    {
    }
  
   public void StartWave()
   {
        StartCoroutine(SpawnWave());
   }

    private IEnumerator SpawnWave()
    {

        for (int i = 0; i < enemies.Count; i++)
        {
            Instantiate(enemies[i], path[0].transform.position, Quaternion.identity);
            
            yield return new WaitForSeconds(enemies[i].GetComponent<EnemyBehaviour>().spawnDelay);

        }
    }
}


