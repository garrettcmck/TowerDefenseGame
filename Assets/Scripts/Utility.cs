using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utility
{
    public int damageMultiplier;
    public GameObject[] path;
    public GameObject enemySpawn;
    public GameObject enemyFinish;

     public Utility()
     {
         damageMultiplier = 1;
     }

    public void Attack(TurretBehaviour turretBehaviour, EnemyBehaviour enemyBehaviour)
    {
        FindWeaknesses(turretBehaviour, enemyBehaviour);
        enemyBehaviour.currentHealth = enemyBehaviour.currentHealth - turretBehaviour.attackDamage * damageMultiplier;
    }

    public int FindWeaknesses(TurretBehaviour TurretBehaviour, EnemyBehaviour EnemyBehaviour)
    {
        int numberOfWeaknesses= 1;

        if (TurretBehaviour.attackType == TurretBehaviour.AttackType.Bullet && EnemyBehaviour.type == EnemyBehaviour.Type.Carbon)
        {
            numberOfWeaknesses++;
        }
        if (TurretBehaviour.attackType == TurretBehaviour.AttackType.Plasma && EnemyBehaviour.type == EnemyBehaviour.Type.Synthetic)
        {
            numberOfWeaknesses++;
        }
        if (TurretBehaviour.attackType == TurretBehaviour.AttackType.Heat && EnemyBehaviour.type == EnemyBehaviour.Type.Silicon)
        {
            numberOfWeaknesses++;
        }
        if (TurretBehaviour.attackType == TurretBehaviour.AttackType.Energy && EnemyBehaviour.type == EnemyBehaviour.Type.Carbon)
        {
            numberOfWeaknesses++;
        }

        return numberOfWeaknesses;
    }
}
