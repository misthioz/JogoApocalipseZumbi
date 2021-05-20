using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedKit : MonoBehaviour
{   
    public int vida;
    private int destroyTime = 5;

    void Start()
    {
        Destroy(gameObject, destroyTime); //segundo parametro: tempo para destruir o obj
    }
    void OnTriggerEnter(Collider col)
    {
       if(col.CompareTag("Player"))
       {
           if(col.GetComponent<PlayerControl>().status.health < col.GetComponent<PlayerControl>().status.Totalhealth)
           {
               col.GetComponent<PlayerControl>().Heal(vida);
               Destroy(gameObject);
           }
        
       }
    }
}
