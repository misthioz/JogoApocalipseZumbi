using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Municao : MonoBehaviour
{
    int ammo = 5;
    [HideInInspector]
    public AmmoBox ammoBoxScript;
    
   void Start()
   {
       //ammoBoxScript = GameObject.FindObjectOfType(typeof(AmmoBox)) as AmmoBox;
   }
   void OnTriggerEnter(Collider other)
   {
       if(other.CompareTag("Player"))
       {
           GunControl scriptgun = other.GetComponent<GunControl>();
           if(scriptgun.currentAmmo<scriptgun.totalAmmo)
           {
               scriptgun.Recarregar(ammo);
               Destroy(gameObject);
               ammoBoxScript.DiminuirQtdMunicoes();
               
           }
       }
   }

   void selfDestroy()
   {
       Destroy(gameObject, 10);
       ammoBoxScript.qtdAtualAmmo--;
   }
}
