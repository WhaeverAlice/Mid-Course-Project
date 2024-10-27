using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGParallax : MonoBehaviour
{
    private float length, startPos;
    [SerializeField] private GameObject cam;
    [SerializeField] private float parallaxEffect;
    void Start()
    {
        startPos = transform.position.x;
        length = GetComponentInChildren<SpriteRenderer>().bounds.size.x;
    }
    void FixedUpdate()
    {
        //move background layer relative to the camera
        float temp = (cam.transform.position.x * (1 - parallaxEffect));
        float dist = (cam.transform.position.x * parallaxEffect);
        transform.position = new Vector3 (startPos + dist, transform.position.y, transform.position.z);

        //transform position of background to ensure it progresses in the level along with the camera
        if (temp > (startPos + length) - 1)
        {
            startPos += length;
        }
    }
}
