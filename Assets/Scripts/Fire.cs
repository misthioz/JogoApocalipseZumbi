using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Inimigo"))
        {
           other.GetComponent<ZombieControl>().Damage(1);
        }
    }
}
