using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BossSpawner : MonoBehaviour
{
    public GameObject Bosszombie;
    public float timeZombie;
    private Transform player;
    float timer;
    private float generationRadius = 20;
    private float distanceFromPlayer = 15;
    public LayerMask layerZUmbi;
    //qtd zumbis
    //private int qtdMaxZumbisVivos = 3;
    public int qtdZumbisVivosAtual; 
    //tempo
    float tempoProxNivel = 30;
    float contadorAumentarNivel;
    private UIControl scriptUI;

    public Transform[] posicoesGeracao;
    
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        
        contadorAumentarNivel = tempoProxNivel;
        scriptUI = GameObject.FindObjectOfType(typeof(UIControl)) as UIControl;

    }

    void Update()
    {
        bool distanciaPermitida = Vector3.Distance(transform.position, player.transform.position)>distanceFromPlayer;

        if(distanciaPermitida)
        {
            timer+=Time.deltaTime;
            if(timer>=timeZombie)
            {
                GenerateZombie();
                timer = 0;
            }
        }
        
        /*if(Time.timeSinceLevelLoad > contadorAumentarNivel)
        {
            qtdMaxZumbisVivos++;
            contadorAumentarNivel = Time.timeSinceLevelLoad+tempoProxNivel;
        } */
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, generationRadius);
    }

    void GenerateZombie()
    {
        Vector3 pos = CalcularPosicaoMaisDistanteDoPlayer();
        //Collider[] colliders = Physics.OverlapSphere(pos, 10, layerZUmbi);

        /*while(colliders.Length>3)
        {
            pos = RandomPos();
            colliders = Physics.OverlapSphere(pos, 15, layerZUmbi);
        } */
        
        Zumbizao zumbi = Instantiate(Bosszombie, pos, transform.rotation).GetComponent<Zumbizao>();
        scriptUI.ApareceTextoChefe();
     
    }

    Vector3 RandomPos()
    {
        Vector3 position = Random.insideUnitSphere * generationRadius;
        position += transform.position;
        position.y = 0;

        return position;
    }

    Vector3 CalcularPosicaoMaisDistanteDoPlayer()
    {
        Vector3 posicaoMaisDistante = Vector3.zero;
        float maiorDistancia = 0;
        foreach(Transform posicao in posicoesGeracao)
        {
            float distanciaDoJogador = Vector3.Distance(posicao.position, player.position);
            if(distanciaDoJogador>maiorDistancia)
            {
                maiorDistancia = distanciaDoJogador;
                posicaoMaisDistante = posicao.position;
            }
            
        }

        return posicaoMaisDistante;
    }
}
