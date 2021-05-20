using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCharacter : MonoBehaviour
{
    private Rigidbody myRb;

    void Awake()
    {
        myRb = GetComponent<Rigidbody>();
    }
    public void Move(Vector3 direcao, float speed, Quaternion rotacao)
    {
        myRb.MovePosition(myRb.position + direcao.normalized * speed * Time.deltaTime);
        GetComponent<Rigidbody>().MoveRotation(rotacao);
    }

    /* public void Rotate(Quaternion rotacao)
    {
        GetComponent<Rigidbody>().MoveRotation(rotacao);
    } */
    public void Rotacionar (Vector3 direcao)
    {
        Quaternion novaRotacao = Quaternion.LookRotation(direcao);
        myRb.MoveRotation(novaRotacao);
    }

    public void Morrer()
    {
        myRb.constraints = RigidbodyConstraints.None;
        myRb.velocity = Vector3.zero;
        myRb.isKinematic = false;
        GetComponent<Collider>().enabled = false;
    }
}
