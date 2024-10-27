using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class BashChar : PlayableCharacter
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
