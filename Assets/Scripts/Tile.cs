using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
   public TileType type;   
    
   public enum TileType
   {
        Floor,
        Wall,
        Ramp,
   }

	void Start ()
    {
        if (tag == "Path")
        {
            GameObject childGo = transform.Find("Model").gameObject;
            Renderer rend = childGo.GetComponent<Renderer>();
            rend.material.SetColor("_Color", Color.green);
        }
        if (tag == "EnemySpawn")
        {
            GameObject childGo = transform.Find("Model").gameObject;
            Renderer rend = childGo.GetComponent<Renderer>();
            rend.material.SetColor("_Color", Color.black);


        }
        if (tag == "EnemyFinish")
        {
            GameObject childGo = transform.Find("Model").gameObject;
            Renderer rend = childGo.GetComponent<Renderer>();
            rend.material.SetColor("_Color", Color.red);
        }
    }
}
