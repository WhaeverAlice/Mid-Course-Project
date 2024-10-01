using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BashChar : PlayableCharacter
{
    public void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    public override void OnCollisonEnter2D(Collider2D collider)
    {
        throw new System.NotImplementedException();
    }

    public override void OnTriggerEnter2D(Collider2D col)
    {
        throw new System.NotImplementedException();
    }

    public override void SpecialAbility()
    {
        //throw new System.NotImplementedException();
        Debug.Log("need to implement special ability : break wall");
    }
}
