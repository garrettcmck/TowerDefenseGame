using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
 

    public int currentHealth;
    public int maxHealth;
    public int experience;
    public int gold;
    public Type type;
    public float currentSpeed;
    public float maxSpeed;
    public float spawnDelay;
    public bool isFlying;

    public Utility utility;
    public List<GameObject> path /*= new List<GameObject>*/;
    public LevelManager levelManager;
    public bool pathComplete = false;

    public int targetIndex = 0;
    public Transform target;


    public enum Type
    {
        Synthetic,
        Carbon,
        Silicon,
        None,
    }

    public void Awake()
    {
        utility = new Utility();       
        levelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();       
    }

    public void Start()
    {
        path = levelManager.path;
        target = path[0].transform;
    }
    public void FixedUpdate()
    {
        if (currentHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Enemy")
        {
            Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
        }
        if (collision.collider.tag == "Turret")
        {
            Debug.Log("HELOO");
            Physics.IgnoreCollision(collision.collider, GetComponentInChildren<Collider>());
        }

    }
    public void Update()
    {
        Vector3 direction = target.position - transform.position;

        var lookPos = target.position - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 6);

        transform.Translate(direction.normalized * currentSpeed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) <= 0.2f)
        {
            GetNextTarget();
        } 

    }

    void GetNextTarget()
    {
        if (targetIndex >= path.Count - 2)
        {
            Debug.Log("End of Path");
            Destroy(gameObject);
        }       
        targetIndex++;
        target = path[targetIndex].transform;           
    }





    public EnemyBehaviour()
    {
        maxHealth = 100;
        currentHealth = maxHealth;
        experience = 2;
        gold = 1;
        type = Type.None;
        currentSpeed = 2;
        maxSpeed = 20;
        spawnDelay = 1;
        isFlying = false;
    }

    public EnemyBehaviour(int MaxHealth, int Experience, int Gold, Type Type, float CurrentSpeed, float MaxSpeed, float SpawnDelay, bool IsFlying)
    {
        maxHealth = MaxHealth;
        currentHealth = MaxHealth;
        experience = Experience;
        gold = Gold;
        type = Type;
        currentSpeed = CurrentSpeed;
        maxSpeed = MaxSpeed;
        spawnDelay = SpawnDelay;
        isFlying = IsFlying;
    }  
}
