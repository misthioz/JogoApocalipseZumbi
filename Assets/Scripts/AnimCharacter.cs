using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimCharacter : MonoBehaviour
{
    private Animator myAnim;

    void Start()
    {
        myAnim = GetComponent<Animator>();
    }
    public void Attack(bool state)
    {
        myAnim.SetBool("isAttacking", state);
    }
    public void Walk(bool state)
    {
        myAnim.SetBool("Moving", state);
    }

    public void Morrer()
    {
        myAnim.SetTrigger("Die");
    }
}
