using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float offset;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(target.position.x + offset, transform.position.y, -10);
    }
}
