using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CTRL_Animation : MonoBehaviour
{
    public static CTRL_Animation instance;

    public Animator anim;
    
    void Start()
    {
        instance = this;
        //anim = gameObject.transform.GetChild(0).transform.GetComponent<Animator>();
    }

    public void Idle()
    {
        anim.SetInteger("Condition", 0);
    }
    public void Walk()
    {
        anim.SetInteger("Condition", 1);
        //anim.Play("CharecterRangeWgun03_walk");
    }
    public void Shoot()
    {
        anim.SetInteger("Condition", 2);
    }
}
