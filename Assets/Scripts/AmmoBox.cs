using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBox : MonoBehaviour
{
    public GameObject ammo;
    public float timeSpawn;
    private float timer = 0;
    public int qtdMaxAmmo = 3;
    public int qtdAtualAmmo;

    public LayerMask layerAmmo;
    void Start()
    {
        timeSpawn = Random.Range(8,17);
    }

    // Update is called once per frame
    void Update()
    {
        if(qtdAtualAmmo<qtdMaxAmmo)
        {
            timer+=Time.deltaTime;
            if(timer>=timeSpawn)
            {
                GenerateAmmo();
                timer = 0;
            }
           
        }
    }

    void GenerateAmmo()
    {
        Vector3 posicao = RandomPos();
        Collider[] colliders = Physics.OverlapSphere(posicao, 2,layerAmmo);

        while(colliders.Length>0)
        {
           posicao = RandomPos();
           colliders = Physics.OverlapSphere(posicao, 2,layerAmmo);
        }
        Municao municao = Instantiate(ammo, posicao, transform.rotation).GetComponent<Municao>();
        municao.ammoBoxScript = this;
        qtdAtualAmmo++;
        
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, 3);
    }

    Vector3 RandomPos()
    {
        Vector3 posicao = Random.insideUnitSphere * 3;
        posicao+= transform.position;
        posicao.y = 0;
        return posicao;
    }

    public void DiminuirQtdMunicoes()
    {
        qtdAtualAmmo--;
    }
}
