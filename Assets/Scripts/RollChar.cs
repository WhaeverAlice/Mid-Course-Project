using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollChar : PlayableCharacter
{
    public void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentHP = maxHP;
    }

    //public override void OnCollisonEnter2D(Collider2D collider)
    //{
    //    throw new System.NotImplementedException();
    //}

    //public override void OnTriggerEnter2D(Collider2D col)
    //{
    //    //destroy wall traps if active ability on
    //    if (abilityActive && col.CompareTag("FireTrap"))
    //    {
    //        abilityActive = false;
    //        //method for destroying trap (animation and destroy?)
    //        Trap trap = col.gameObject.GetComponent<Trap>();
    //        trap.ApplyDamage();

    //        //extra score for destroying traps
    //        score += 500;
    //    }
    //    else //take damage from all other traps or if ability off
    //    {
    //        //apply damage to char
    //        this.ApplyDamage();
    //    }
    //}

    public override void SpecialAbility()
    {
        //play ability animation
        anim.SetTrigger("abilityActive");

        //set active ability on
        abilityActive = true;

        //wait for animation of ability to end
        WaitForAnimation();
       
    }
}
