using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventAnimation : MonoBehaviour
{
    private Animator amt;

    private void Start()
    {
        amt = GetComponent<Animator>();
    }

    void AttackEnd()
    {
        //amt.SetInteger("Attack", 0);
        var num = amt.GetInteger("Action");
        num ^= 32;
        //var info = amt.GetCurrentAnimatorStateInfo(0);
        //int tag = info.tagHash;
        amt.SetInteger("Action", num);
    }
    
}
