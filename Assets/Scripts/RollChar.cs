using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollChar : PlayableCharacter
{
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
