using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Trap : MonoBehaviour, IDamageable, IAnimated
{
    private Animator anim;
    protected Collider2D trigger;

    private void OnEnable()
    {
        StartCoroutine(DeleteAfterTime());
    }
    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        //apply damage to char
        PlayableCharacter player = other.gameObject.GetComponent<PlayableCharacter>();
        player.ApplyDamage();
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        PlayableCharacter player = collision.gameObject.GetComponent<PlayableCharacter>();
        player.isTouchingTrap = true;
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        PlayableCharacter player = collision.gameObject.GetComponent<PlayableCharacter>();
        player.isTouchingTrap = false;
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

    public IEnumerator WaitForAnimation()//waits for animation to end
    {
        yield return new WaitWhile(() => anim.GetCurrentAnimatorStateInfo(0).normalizedTime <= 1.0f);
    }

    IEnumerator DeleteAfterTime()
    {
        yield return new WaitForSeconds(60);
        Die();
    }
}
