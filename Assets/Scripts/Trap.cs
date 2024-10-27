using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Trap : MonoBehaviour, IDamageable
{
    private Animator anim;
    protected Collider2D trigger;

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        //apply damage to player
        PlayableCharacter player = other.gameObject.GetComponent<PlayableCharacter>();
        player.ApplyDamage();
    }

    public void ApplyDamage()
    {
        //disable colliders
        trigger.enabled = false;
        Die();
    }

    public void Die()
    {
        //destroy object 
        Destroy(this.gameObject);
    }
}
