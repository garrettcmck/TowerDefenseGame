using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBehaviour : MonoBehaviour
{
    // VARIABLES
    public int currentHealth;
    public int maxHealth;
    public int attackSpeed;
    public int attackDamage;
    public float range;
    public int currentExperience;
    public int maxExperience;
    public int experienceMultiplier; //takes enemy experience and multiplies it depending on turret level (higher lvl less exp)
    public int attributePoints; // Attribute points are used to increase turret stats. +1 point on level up
    public int currentLevel;
    public int maxLevel;
    public AttackTargetType attackTargetType;
    public AttackEffect attackEffect;
    public AttackType attackType;
    public int killCount;
    public int abilityPoints;

    public ProjectileBehaviour projectileBehaviour;
    public Utility utility;
    public bool isEnemyInRange = false;
    public GameObject pivotPoint;
    public List<GameObject> enemyGOs = new List<GameObject>();
    public List<EnemyBehaviour> enemies = new List<EnemyBehaviour>();

    public GameObject enemyTarget;
    public EnemyBehaviour enemyBehaviour;
    public bool isAttacking;


    public TurretBehaviour turretBehaviour;
    public SphereCollider rangeCollider;
    public BoxCollider bodyCollider;


    // END VARIABLES

    // ENUMERATIONS
    public enum AttackTargetType
    {
        Aoe,
        Single,
        None
    }

    public enum AttackEffect
    {
        Slow,
        Stun,
        Bleed,
        None
    }

    public enum AttackType
    {
        Energy,
        Plasma,
        Bullet,
        Heat,
        None
    }
    // END ENUMERATIONS

    //CONSTRUCTORS
    public TurretBehaviour()
    {
        maxHealth = 100;
        currentHealth = maxHealth;
        range = 1;
        attackDamage = 1;
        attackSpeed = 1;
        attackTargetType = AttackTargetType.Single;
        attackEffect = AttackEffect.None;
        killCount = 0;
        abilityPoints = 0;
    }

    public TurretBehaviour(int MaxHealth, float Range, int AttackDamage, int AttackSpeed,
    AttackTargetType AttackTargetType, AttackEffect AttackEffect, AttackType AttackType, int CurrentExperience, int MaxExperience,
    int ExperienceMultiplier, int CurrentLevel, int MaxLevel, int KillCount, int AbilityPoints)
    {
        maxHealth = MaxHealth;
        currentHealth = MaxHealth;
        range = Range;
        attackDamage = AttackDamage;
        attackSpeed = AttackSpeed;
        attackTargetType = AttackTargetType;
        attackEffect = AttackEffect;
        attackType = AttackType;
        currentExperience = CurrentExperience;
        maxExperience = MaxExperience;
        experienceMultiplier = ExperienceMultiplier;
        currentLevel = CurrentLevel;
        maxLevel = MaxLevel;
        killCount = KillCount;
        abilityPoints = AbilityPoints;
    }
    // END CONSTRUCTORS

    private void Awake()
    {
        utility = new Utility();
        turretBehaviour = GetComponent<TurretBehaviour>(); //might need to be moved if turrets dont work when you are built during gameplay
        GetComponentInChildren<SphereCollider>().radius = range;
        
    }


    private void OnTriggerEnter(Collider collider)
    {

        if (collider.tag == "Enemy")
        {
            enemyBehaviour = collider.GetComponent<EnemyBehaviour>();   // get enemy behaviour component of the enemy gameobject
                                                                        // get enemy created by enemybehaviour 
            enemyGOs.Add(collider.gameObject);                          //Add this enemy gameobject to the enemies gameobject list
            enemies.Add(enemyBehaviour);                                         //add the enemy script to the enemy list
         
         
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "Enemy")
        {
            enemyGOs.Remove(collider.gameObject);
            enemies.Remove(collider.GetComponent<EnemyBehaviour>());
        }
     
    }

    private void FixedUpdate()
    {
        for (int i = 0; i < enemyGOs.Count; i++)
        {
            if (enemyGOs[i] == null)                                                   
            {
                enemyGOs.Remove(enemyGOs[i]);
            }
        }
        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i] == null)                                                   
            {
                enemies.Remove(enemies[i]);
            }
        }

        if (/*enemyGOs[0] != null &&*/ enemyGOs.Count != 0)
       {
            RotateToTarget(enemyGOs[0].transform.position);

          
            utility.Attack(turretBehaviour, enemies[0]);            
            projectileBehaviour.Fire(enemyGOs[0].transform.position);
            if (enemies[0].currentHealth <= 0)
            {
                currentExperience = currentExperience + enemies[0].experience;

                if (currentExperience >= maxExperience)
                {
                    LevelUp(enemies[0].experience);
                }

                killCount++;
                enemies.Remove(enemies[0]);
                enemyGOs.Remove(enemyGOs[0]);                  
            }
       }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = FindObjectOfType<Camera>().ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider == bodyCollider)
                {
                    Debug.Log("Hit Turret");
                   
                }
            }
        }
    }

    // FUNCTIONS

    public void RotateToTarget(Vector3 enemyPosition)
    {
        pivotPoint =  transform.Find("PivotPoint").gameObject;
        pivotPoint.transform.LookAt(enemyPosition);
    }

    public void Fire()
    {
     
    }

    public void LevelUp(int enemyExp)
    {
        int rolloverExp = (currentExperience + enemyExp) - maxExperience;

        if (currentLevel + 1 <= maxLevel)
        {
            currentLevel++;
            abilityPoints++;
            currentExperience = 0 + rolloverExp;
        }
    }
    // END FUNCTIONS
}
