using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject zombie;
    public float timeZombie;
    private GameObject player;
    float timer;
    private float generationRadius = 2;
    private float distanceFromPlayer = 15;
    public LayerMask layerZUmbi;
    //qtd zumbis
    private int qtdMaxZumbisVivos = 3;
    public int qtdZumbisVivosAtual; 
    //tempo
    float tempoProxNivel = 30;
    float contadorAumentarNivel;
    
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        
        contadorAumentarNivel = tempoProxNivel;

        for(int i = 0; i<qtdMaxZumbisVivos;i++)
        {
            //StartCoroutine(GenerateZombie());
            GenerateZombie();
        }

    }

    void Update()
    {
        bool distanciaPermitida = Vector3.Distance(transform.position, player.transform.position)>distanceFromPlayer;

        if(distanciaPermitida && qtdZumbisVivosAtual<qtdMaxZumbisVivos)
        {
            timer+=Time.deltaTime;
            if(timer>=timeZombie)
            {
                GenerateZombie();
                timer = 0;
            }
        }
        
        if(Time.timeSinceLevelLoad > contadorAumentarNivel)
        {
            qtdMaxZumbisVivos++;
            contadorAumentarNivel = Time.timeSinceLevelLoad+tempoProxNivel;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, generationRadius);
    }

    void GenerateZombie()
    {
        Vector3 pos = RandomPos();
        Collider[] colliders = Physics.OverlapSphere(pos, 1, layerZUmbi);

        while(colliders.Length>0)
        {
            pos = RandomPos();
            colliders = Physics.OverlapSphere(pos, 1, layerZUmbi);
        }
        ZombieControl zumbi = Instantiate(zombie, pos, transform.rotation).GetComponent<ZombieControl>();
        zumbi.scriptSpawner = this;
        qtdZumbisVivosAtual++;
    }

    Vector3 RandomPos()
    {
        Vector3 position = Random.insideUnitSphere * generationRadius;
        position += transform.position;
        position.y = 0;

        return position;
    }

    public void DiminuirQtdZumbisVivos()
    {
        qtdZumbisVivosAtual--;
    }

  
}
