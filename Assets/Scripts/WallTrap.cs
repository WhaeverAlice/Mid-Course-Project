using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallTrap : Trap
{
    [SerializeField] GameObject _particleSystem;
    [SerializeField] private AudioSource audioSource;
    private void Awake()
    {
        trigger = GetComponent<Collider2D>();
    }
    public override void OnTriggerEnter2D(Collider2D other)
    {
        PlayableCharacter player = other.gameObject.GetComponent<PlayableCharacter>();
        
        //destroy trap if correct active ability on
        if (player.abilityActive && other.CompareTag("BashPlayer"))
        {
            player.abilityActive = false;
            this.ApplyDamage();
            audioSource.Play();
            _particleSystem.SetActive(true);

            //extra score for destroying traps
            player.scoreTracker.IncreaseScore(500);
        }
        else //make player take damage from trap if ability is off
        {
            player.ApplyDamage();
        }
    }
}
