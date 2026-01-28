using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class girtuighrtughrt : MonoBehaviour
{
    
    void Start()
    {
        GameObject spawn = GameObject.Find("ForestSpawnPoint");
        if(spawn != null)
        {
            transform.position=spawn.transform.position;
        }
    }
}
