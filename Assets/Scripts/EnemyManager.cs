using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    float timer;
    float difficultyMod;
    public GameObject[] enemies;
    public GameObject[] bossEnemies;


    // Update is called once per frame
    void Update()
    {
        timer+= Time.deltaTime;
        if(timer >= 5)
        {
            SpawnEnemy();
            rollOverTimer();
            
        }
    }

    void rollOverTimer()
    {
        timer = 0;
        difficultyMod += 0.5f;
    }

    public void SpawnEnemy()
    {
        Vector3 spawnposition = GetSpawnPosition();
       int spawnPool = GetSpwanPool();
        int SpawnIndex;
        SpawnIndex = GetSpawnIndex();
        GameObject newEnemy;
        switch(spawnPool)
        {
            case 1:
           newEnemy = Instantiate(enemies[SpawnIndex], spawnposition, Quaternion.identity);
            HitManager health = newEnemy.GetComponent<HitManager>();
            health.maxHealth = health.maxHealth * GetDifficultyMod();
            EnemyScript dmg = newEnemy.GetComponent<EnemyScript>();
            dmg.dmg = dmg.dmg * GetDifficultyMod();
            dmg.moveSpeed = dmg.moveSpeed * GetDifficultyMod();
            break;
            case 2:
           newEnemy = Instantiate(bossEnemies[SpawnIndex],spawnposition,Quaternion.identity);
            HitManager health1 = newEnemy.GetComponent<HitManager>();
            health1.maxHealth = health1.maxHealth * GetDifficultyMod();
            EnemyScript dmg1 = newEnemy.GetComponent<EnemyScript>();
            dmg1.dmg = dmg1.dmg * GetDifficultyMod();
            dmg1.moveSpeed = dmg1.moveSpeed * GetDifficultyMod();
            break;
            default:
                break;
           
        }



    }

    Vector3 GetSpawnPosition()
    {
        Transform[] childTransforms = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            childTransforms[i] = transform.GetChild(i);
        }

        // Check if there are any child objects
     
        
            // Choose a random child index
            int randomChildIndex = Random.Range(0, childTransforms.Length);

            // Get the position of the randomly selected child
            Vector3 randomChildPosition = childTransforms[randomChildIndex].position;

            // Now, you can use randomChildPosition as the position of the selected child.
           return randomChildPosition;
        
      
    }

    int GetSpwanPool()
    {
        float randomEnemyIndex = Random.Range(0, 11);
        randomEnemyIndex = randomEnemyIndex * GetDifficultyMod();

        if (randomEnemyIndex >= 18)
        {
            return 2;
        }
        else
        {
            return 1;
        }
    }

    int GetSpawnIndex()
    {
        int randomEnemyIndex = Random.Range(0, 11);
       if (randomEnemyIndex == 10)
        {
            return 4;
        }
       else if (randomEnemyIndex <10 && randomEnemyIndex >= 7)
        {
            
            return 3;
        }
       else 
        {
            int RandomEnemy = Random.Range(1, 3);
            return RandomEnemy;
        }
    }
    float GetDifficultyMod()
    {
        return (1 + (difficultyMod / 10));
    }
}
