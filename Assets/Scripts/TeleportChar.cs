using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportChar : PlayableCharacter
{
    public void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    public override void SpecialAbility()
    {
        //throw new System.NotImplementedException();
        Debug.Log("need to implement special ability : teleport");
    }
}
