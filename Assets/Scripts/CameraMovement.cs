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
        //set the active character as the target to follow
        target = charSwitcher.activeChar.transform;
    }

    // Update is called once per frame
    void Update()
    {
        //make sure the camera is always following the character that's currently active
        target = charSwitcher.activeChar.transform;
        transform.position = new Vector3(target.position.x + offset, transform.position.y, -10);
    }
}
