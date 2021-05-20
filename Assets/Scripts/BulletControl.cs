using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControl : MonoBehaviour
{
    Rigidbody rb;
    public float speed;

    public AudioClip dieSound;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.MovePosition(rb.position + transform.forward*speed*Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        /*switch(other.tag)
        {
            case "Inimigo": 
                other.GetComponent<ZombieControl>().Damage(1);
                break;
            
            case "Boss":
                other.GetComponent<Zumbizao>().Damage(1);
                break;
        }*/
        Quaternion rotacaoOposta = Quaternion.LookRotation(-transform.forward);
        if(other.CompareTag("Inimigo"))
        {
            ZombieControl zombieScript = other.GetComponent<ZombieControl>();
            zombieScript.Damage(1);
            zombieScript.ParticulaSangue(transform.position, rotacaoOposta);
        }
        else if(other.CompareTag("Boss"))
        {
            Zumbizao bossScript = other.GetComponent<Zumbizao>();
            bossScript.Damage(1);
            bossScript.ParticulaSangue(transform.position, rotacaoOposta);
        }
        else
        {
            Destroy(gameObject);
        } 
    }

}
