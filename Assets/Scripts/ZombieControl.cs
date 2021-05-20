using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieControl : MonoBehaviour, IMatavel
{
    public GameObject player;
    public GameObject particulaSangue;
    //Rigidbody rb;
    int damage;
    public AudioClip attackSound;
    public AudioClip dieSound;
    private float wanderTimer;
    private float totalWanderTime = 4;
    private MoveCharacter Move;
    private AnimCharacter Animate;
    private Status status;
    private Vector3 randomPos;
    private Vector3 direcao;
    Quaternion novaRotacao;

    [HideInInspector]
    public ZombieSpawner scriptSpawner;
    private UIControl scriptUI;

    //medkit vars
    private float porcentagemGeraMeKit = 0.1f;
    public GameObject MedKit;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Move = GetComponent<MoveCharacter>();
        Animate = GetComponent<AnimCharacter>();
        status = GetComponent<Status>();
        RandomZombie();   

        scriptUI = GameObject.FindObjectOfType(typeof(UIControl)) as UIControl;
    }

    void FixedUpdate()
    {
        float distancia = Vector3.Distance(transform.position, player.transform.position);
        
        if(distancia>10)
        {
            Wander();
        }
        else if(distancia > 2)
        {
            direcao = player.transform.position - transform.position;
            novaRotacao = Quaternion.LookRotation(direcao);
            Animate.Attack(false);
            Move.Move(direcao, status.speed, novaRotacao);
        }
        else
        {
            Animate.Attack(true);
        }
    }

    void Wander()
    {
        wanderTimer -=Time.deltaTime;
        if(wanderTimer<=0)
        {
            randomPos = GenerateRandomPos();
            direcao = randomPos - transform.position;
            novaRotacao = Quaternion.LookRotation(direcao);
            wanderTimer += totalWanderTime + Random.Range(-1f, 1f);
        }

        if(Vector3.Distance(transform.position, randomPos)!=0)
        {
            //direcao = randomPos - transform.position;
            Move.Move(direcao,status.speed,novaRotacao);
        }
        
    }

    Vector3 GenerateRandomPos()
    {
        Vector3 position = Random.insideUnitSphere * 10; 
        position+= transform.position;
        position.y = transform.position.y;

        return position;
    }

    void AttackPlayer()
    {
        int dano = Random.Range(20,30);
        player.GetComponent<PlayerControl>().Damage(dano);
        AudioControl.instance.PlayOneShot(attackSound);
    }   

    void RandomZombie()
    {
        int zumbiGerado = Random.Range(1,transform.childCount);
        transform.GetChild(zumbiGerado).gameObject.SetActive(true);
    }

    public void Damage(int dano)
    {
        status.health-=dano;
        if(status.health<=0)
        {
            Die();
        }
    }
    public void ParticulaSangue(Vector3 posicao, Quaternion rotacao)
    {
        Instantiate(particulaSangue, posicao, rotacao);
    }

    public void Die()
    {
        Destroy(gameObject, 2);
        AudioControl.instance.PlayOneShot(dieSound);
        //Destroy(gameObject);
        Animate.Morrer();
        CheckIfMedKit(porcentagemGeraMeKit);
        scriptUI.AtualizaQtdZUmbisMortos();
        scriptSpawner.DiminuirQtdZumbisVivos();
        Move.Morrer();
        this.enabled = false;
    }

    void CheckIfMedKit (float percent)
    {
        if(Random.value <= percent)
        {
            Instantiate(MedKit, transform.position, Quaternion.identity); //rotacao gerada
        }
    }

}
