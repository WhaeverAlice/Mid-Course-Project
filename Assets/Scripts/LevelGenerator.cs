using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class LevelGenerator : MonoBehaviour
{
    private float playerDistSpawnTrap = 40f;
    [SerializeField] private Transform topTraps;
    [SerializeField] private Transform midTraps;
    [SerializeField] private Transform bottTraps;
    [SerializeField] private Transform viewBoundsStart;
    [SerializeField] private Transform viewBoundsEnd;
    [SerializeField] private List<Transform> traps;
    [SerializeField] private CharSwitcher characterSwitcher;
    [SerializeField] private PlayableCharacter waterChar;
    [SerializeField] private PlayableCharacter TeleportChar;
    [SerializeField] private PlayableCharacter BashChar;
    private PlayableCharacter activeChar;

    private Vector3 lastTrapPositionTop;
    private Vector3 lastTrapPositionMid;
    private Vector3 lastTrapPositionBott;

    private void Awake()
    {
        activeChar = characterSwitcher.activeChar;
        lastTrapPositionTop = topTraps.Find("OriginTransform").position;
        lastTrapPositionMid = midTraps.Find("OriginTransform").position;
        lastTrapPositionBott = bottTraps.Find("OriginTransform").position;

        int startingSpawnTraps = 5;
        for (int i = 0; i < startingSpawnTraps; i++)
        {
            SpawnTrapTop();
            SpawnTrapMid();
            SpawnTrapBott();
        }
    }

    private void Update()
    {
        if (waterChar.dead)
        {
            traps[1] = null;
        }
        
        activeChar = characterSwitcher.activeChar;
        if (Vector3.Distance(activeChar.transform.position, lastTrapPositionTop) < playerDistSpawnTrap) 
        {
            SpawnTrapTop();
        }
        if (Vector3.Distance(activeChar.transform.position, lastTrapPositionMid) < playerDistSpawnTrap) 
        {
            SpawnTrapMid();
        }
        if (Vector3.Distance(activeChar.transform.position, lastTrapPositionBott) < playerDistSpawnTrap) 
        {
            SpawnTrapBott();
        }
    }

    //private void SpawnTrap(ref Vector3 spawnPoint)
    //{
    //    Transform chosenTrap = traps[Random.Range(0, traps.Length)];
    //    Transform lastTrapTransform = GetTrap(chosenTrap, spawnPoint);
    //    spawnPoint = new Vector3(lastTrapTransform.Find("MinGap").position.x, lastTrapTransform.Find("OriginTransform").position.y, chosenTrap.Find("OriginTransform").position.z);
    //}
     private void SpawnTrapTop()
    {
        Transform chosenTrap = GetRandomTrap();
        Transform lastTrapTransform = SetTrap(chosenTrap, lastTrapPositionTop);
        lastTrapPositionTop = new Vector3(lastTrapTransform.Find("MinGap").position.x, lastTrapTransform.Find("OriginTransform").position.y, chosenTrap.Find("OriginTransform").position.z);
    }
     private void SpawnTrapMid()
    {
        Transform chosenTrap = GetRandomTrap();
        Transform lastTrapTransform = SetTrap(chosenTrap, lastTrapPositionMid);
        lastTrapPositionMid = new Vector3(lastTrapTransform.Find("MinGap").position.x, lastTrapTransform.Find("OriginTransform").position.y, chosenTrap.Find("OriginTransform").position.z);
    }
     private void SpawnTrapBott()
    {
        Transform chosenTrap = GetRandomTrap();
        Transform lastTrapTransform = SetTrap(chosenTrap, lastTrapPositionBott);
        lastTrapPositionBott = new Vector3(lastTrapTransform.Find("MinGap").position.x, lastTrapTransform.Find("OriginTransform").position.y, chosenTrap.Find("OriginTransform").position.z);
    }

    private Transform SetTrap(Transform trap, Vector3 spawnPosition)
    {
        Transform trapTransform = Instantiate(trap, spawnPosition, Quaternion.identity);
        return trapTransform;
    }

    private Transform GetRandomTrap()
    {
        Transform chosenTrap = traps[GetRandomValue()];
        Collider2D[] objects = Physics2D.OverlapAreaAll(viewBoundsStart.position, viewBoundsEnd.position);
        List<GameObject> blocks = new List<GameObject>();
        foreach (Collider2D obj in objects)
        {
            if (obj.CompareTag("Block")) blocks.Add(obj.gameObject);
            else continue;
        }
       
        while (chosenTrap.CompareTag("Block") && blocks.Count > 0)
        {
            objects = Physics2D.OverlapAreaAll(viewBoundsStart.position, viewBoundsEnd.position);
            foreach (Collider2D obj in objects)
            {
                if (obj.CompareTag("Block")) blocks.Add(obj.gameObject);
                else continue;
            }
            chosenTrap = traps[GetRandomValue()];
            blocks.Clear();
        }

        return chosenTrap;
    }

    private int GetRandomValue()
    {
        float rand = Random.value;
        if (rand <= 0.05f) 
        {
            return Random.Range(5, traps.Count);
        }
        else if (rand <= 0.3)
        {
            return 4;
        }
        else 
        {
            return Random.Range(0, 4);
        }
    }
}
