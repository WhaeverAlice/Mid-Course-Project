using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTrap : Trap
{
    [SerializeField] GameObject smokeParticles;
    [SerializeField] AudioSource audioSource;
    private void Awake()
    {
        trigger = GetComponent<Collider2D>();
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        PlayableCharacter player = other.gameObject.GetComponent<PlayableCharacter>();
        //destroy trap if correct active ability on
        if (player.abilityActive && other.CompareTag("RollPlayer"))
        {
            player.abilityActive = false;
            //method for destroying trap (animation and destroy?)
            this.ApplyDamage();
            audioSource.Play();
            smokeParticles.SetActive(true);

            //extra score for destroying traps
            player.scoreTracker.IncreaseScore(500);
        }
        else //take damage from trap if ability is off
        {
            player.ApplyDamage();
        }
    }
}
