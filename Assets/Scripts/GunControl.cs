using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunControl : MonoBehaviour
{
    public Transform gunPoint;
    public GameObject bullet;
    private UIControl scriptUI;

    public int totalAmmo = 30;
    public int currentAmmo;

    public AudioClip shootSound;
    // Start is called before the first frame update
    void Start()
    {
        currentAmmo = totalAmmo;
        scriptUI = GameObject.FindObjectOfType(typeof(UIControl)) as UIControl;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentAmmo>0)
        {
            if(Input.GetButtonDown("Fire1"))
            {
                Instantiate(bullet, gunPoint.position, gunPoint.rotation);
                AudioControl.instance.PlayOneShot(shootSound);
                currentAmmo--;
                scriptUI.AtualizaQtdBalas();
            }
           
        }
      
    }

    public void Recarregar(int ammo)
    {
        currentAmmo+=ammo;
        if(currentAmmo>totalAmmo)
        {
            currentAmmo = totalAmmo;
        }
        scriptUI.AtualizaQtdBalas();
    }
}
