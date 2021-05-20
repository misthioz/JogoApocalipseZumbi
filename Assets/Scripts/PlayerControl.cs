using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerControl : MonoBehaviour, IMatavel, ICuravel
{
    Rigidbody rb;
    Animator anim;
    public GameObject texto;
    
    public UIControl scriptUI;

    public Status status;
    

    public AudioClip damageSound;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        status = GetComponent<Status>();

    }

    void Update()
    {
        float eixoX = Input.GetAxis("Horizontal");
        float eixoZ = Input.GetAxis("Vertical");

        if(status.health>0)
        {
            Movimento(eixoX, eixoZ);
        }
    
    }
    
    void Movimento(float x, float z)
    {
        Vector3 direcao = new Vector3(x,0,z);
        
        if(direcao!=Vector3.zero)
        {
            if(anim.GetCurrentAnimatorStateInfo(0).IsName("Sentado"))
            {
                anim.SetBool("Levantando", true);
                anim.SetBool("Levantou", true);
            }
            else
            {
                transform.Translate(direcao*status.speed*Time.deltaTime, Space.Self);
                anim.SetBool("isMoving", true);
            } 
        }
        else{
            anim.SetBool("isMoving", false);
        }

        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up,Vector3.zero);
        float rayLenght;

        if(groundPlane.Raycast(cameraRay, out rayLenght))
        {
            Vector3 pointToLook = cameraRay.GetPoint(rayLenght);
            Debug.DrawLine(cameraRay.origin, pointToLook, Color.green);

            transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
        }
    }

    public void Damage(int dano)
    {
        status.health-=dano;
        scriptUI.AtualizaSliderHP();
        AudioControl.instance.PlayOneShot(damageSound);

        if(status.health<=0)
        {
              Die();
        }
    }
    public void Die()
    {
        Time.timeScale = 0;
        scriptUI.GameOver();
    }

    public void Heal(int vida)
    {        
        status.health += vida;
        if(status.health>status.Totalhealth)
        {
            status.health = status.Totalhealth;
        }
        scriptUI.AtualizaSliderHP();
    }
  
}
