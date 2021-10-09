using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement2 : MonoBehaviour
{
    public static PlayerMovement2 Ins;

    public Animator animator1;
    public Animator animator2;
    public Animator animator1a;
    public Animator animator2a;

    private void Awake()
    {
        MakeSingleton();
    }

    public void AttackMove1()
    {
        animator1.SetTrigger("attack");
        animator2.SetTrigger("takehit");
        animator1a.SetTrigger("attack");
        animator2a.SetTrigger("takehit");
    }
    public void AttackMove2()
    {
        animator2.SetTrigger("attack");
        animator1.SetTrigger("takehit");
        animator2a.SetTrigger("attack");
        animator1a.SetTrigger("takehit");
    }

    public void MakeSingleton()
    {
        if (Ins == null)
        {
            Ins = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
