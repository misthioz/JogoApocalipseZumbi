using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{
    public int Totalhealth;
    [HideInInspector]
    public int health;
    public float speed;
 
    void Awake()
    {
        health = Totalhealth;
    }
}
