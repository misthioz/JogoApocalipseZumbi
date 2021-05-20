using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
public class Zumbizao : MonoBehaviour, IMatavel
{
    //public Transform player;

    public GameObject particulaSangue;
    private Transform player;
    private NavMeshAgent agent;
    private Status statusBoss;
    private AnimCharacter animBoss;

    private MoveCharacter moveBoss;

    private UIControl scriptUI;
    public GameObject kitMed;
    public GameObject ammo;
    public AudioClip morre;

    public Slider sliderHPBoss;
    public Image imagemSlider;
    public Color corMaxHP, corMinHP;
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        statusBoss = GetComponent<Status>();
        animBoss = GetComponent<AnimCharacter>();
        moveBoss = GetComponent<MoveCharacter>();

        scriptUI = GameObject.FindObjectOfType(typeof(UIControl)) as UIControl;

        agent.speed = statusBoss.speed;

        sliderHPBoss.maxValue = statusBoss.Totalhealth;
        AtualizaInterface();

    }

    void Update()
    {
        agent.SetDestination(player.position);

        bool isWalking = agent.velocity.magnitude > 0;
        animBoss.Walk(isWalking);

        if(agent.hasPath == true)
        {
            bool nearPlayer = agent.remainingDistance <= agent.stoppingDistance;
        
            if(nearPlayer)
            {
                Vector3 direcao = player.position - transform.position;
                moveBoss.Rotacionar(direcao);
                animBoss.Attack(true);
            }
            else
                animBoss.Attack(false);
        }
       
    }

    void AttackPlayer()
    {
        int damage = Random.Range(30,40);
        player.GetComponent<PlayerControl>().Damage(damage);
    }

    public void Damage(int dano)
    {
        statusBoss.health -= dano;
        AtualizaInterface();

        if(statusBoss.health<=0)
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
        animBoss.Morrer();
        moveBoss.Morrer();
        AudioControl.instance.PlayOneShot(morre);

        Vector3 posMedKit = RandomPos();
        Instantiate(kitMed, posMedKit, transform.rotation);
        Vector3 posAmmo = RandomPos();
        Instantiate(ammo, posAmmo, transform.rotation);

        Destroy(gameObject, 2);

        scriptUI.AtualizaQtdChefesMortos();

        this.enabled = false;
        agent.enabled = false; //alem do script é necessario desativar o navmesh, ou ele ira continuar procurando um caminho
        //AudioControl.instance.PlayOneShot(dieSound);
    }

    Vector3 RandomPos()
    {
       Vector3 posicao = Random.insideUnitSphere*2;
       posicao += transform.position;
       posicao.y = 0;

       return posicao;
    }

    void AtualizaInterface()
    {
        sliderHPBoss.value = statusBoss.health;
        float porcentagemHP = (float)statusBoss.health / statusBoss.Totalhealth;
        Color corHP = Color.Lerp(corMinHP, corMaxHP, porcentagemHP);
        imagemSlider.color = corHP;
    }
}
