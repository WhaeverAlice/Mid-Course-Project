using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class BashChar : PlayableCharacter
{
    //public void Awake()
    //{
    //    rb = GetComponent<Rigidbody2D>();
    //    anim = GetComponent<Animator>();
    //    currentHP = maxHP;
    //}

    //void FixedUpdate()
    //{
    //    score += 1;
    //    Debug.Log("bash char increase");
    //}

    //public override void OnCollisonEnter2D(Collider2D collider)
    //{
    //    return;
    //}

    //public override void OnTriggerEnter2D(Collider2D col) 
    //{
    //    //destroy wall traps if active ability on
    //    if (abilityActive && col.CompareTag("WallTrap"))
    //    {
    //        //method for destroying trap (animation and destroy?)

    //        //extra score for destroying traps
    //        score += 500;
    //    }
    //    else //take damage from all other traps or if ability off
    //    {
    //        //apply damage to char
    //        this.ApplyDamage();
    //    }
    //}

    public override void SpecialAbility() //might not need an override method - if all animations are called the special ability....
    {
        //play ability animation
        anim.SetTrigger("abilityActive");

        //set active ability on
        abilityActive = true;

        //wait for animation of ability to end
        WaitForAnimation();
    }
}
