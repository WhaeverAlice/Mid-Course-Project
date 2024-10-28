using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class BashChar : PlayableCharacter
{
    [SerializeField] AudioSource abilityVoice;
    public override void SpecialAbility() 
    {
        //play ability animation
        anim.SetTrigger("abilityActive");

        //play voice
        abilityVoice.Play();

        //set active ability on
        abilityActive = true;

        //wait for animation of ability to end
        WaitForAnimation();
    }
}
