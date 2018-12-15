using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    public GameObject bulletGO;
    public GameObject projectileOriginGO;
    public Vector3 projectileOrigin;
    public TargetType targetType;

    //ENUMERATIONS
    public enum TargetType
    {
        Single,
        Aoe
    }
    //END ENUMERATIONS

    //CONSTRUCTORS
    public ProjectileBehaviour()
    {
        targetType = TargetType.Single;
    }

    public ProjectileBehaviour(TargetType TargetType)
    {
        targetType = TargetType;
    }
    //END CONSTRUCTORS
 
    public void Fire(Vector3 EnemyLocation)
    {
        GameObject bullet = Instantiate(bulletGO, projectileOriginGO.transform.position, transform.rotation) as GameObject;
      
        bullet.GetComponent<Rigidbody>().AddForce(projectileOriginGO.transform.forward * 920);
        Destroy(bullet, 4.0f);
      
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Enemy")
        {
           Destroy(bulletGO);
        }
    }


}
