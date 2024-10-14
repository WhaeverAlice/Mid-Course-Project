using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeSensor : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D other)
    {
        PlayableCharacter player = other.gameObject.GetComponent<PlayableCharacter>();
        //add points to score if the player dodged
        if(!player.isTouchingTrap && !player.isInvulnerable)
        {
            player.IncreaseScore(150);
        }
    }
}