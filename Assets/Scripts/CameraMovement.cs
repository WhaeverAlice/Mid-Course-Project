using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Transform target;
    [SerializeField] private float offset;
    [SerializeField] private CharSwitcher charSwitcher;

    void Start()
    {
        target = charSwitcher.GetActiveCharacter().transform;
    }

    // Update is called once per frame
    void Update()
    {
        target = charSwitcher.GetActiveCharacter().transform;
        transform.position = new Vector3(target.position.x + offset, transform.position.y, -10);
    }
}
