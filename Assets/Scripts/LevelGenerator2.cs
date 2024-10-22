using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class LevelGenerator2 : MonoBehaviour
{
    private float playerDistSpawnTrap = 40f;
    [SerializeField] private Transform[] lanes;
    [SerializeField] private Transform[] basicParts;
    [SerializeField] private Transform[] levelParts;
    [SerializeField] private CharSwitcher characterSwitcher;
    private PlayableCharacter activeChar;

    private Vector3 lastPartPosition;

    private void Awake()
    {
        activeChar = characterSwitcher.activeChar;
        lastPartPosition = lanes[1].position;
        
        int startingSpawnParts = 5;
        for (int i = 0; i < startingSpawnParts; i++)
        {
            SpawnPart();
        }
    }

    private void Update()
    {
        activeChar = characterSwitcher.activeChar;
        if (Vector3.Distance(activeChar.transform.position, lastPartPosition) < playerDistSpawnTrap)
        {
            SpawnPart();
        }
    }

    private void SpawnPart()
    {
        Transform lastPartTransform = SetRandomPart(GetRandomPart(), lastPartPosition);
        lastPartPosition = new Vector3(lastPartTransform.Find("Gap").position.x, lanes[Random.Range(0, lanes.Length)].position.y, lastPartTransform.Find("Origin").position.z);
    }
   

    private Transform SetRandomPart(Transform part, Vector3 spawnPosition)
    {
        Transform partTransform = Instantiate(part, spawnPosition, Quaternion.identity);
        return partTransform;
    }

    private Transform GetRandomPart()
    {
        float rand = Random.value;
        if (rand <= 0.2)
        {
            return levelParts[Random.Range(0, levelParts.Length)];
        }
        else
        {
            return basicParts[Random.Range(0, basicParts.Length)];
        }
    }
}
