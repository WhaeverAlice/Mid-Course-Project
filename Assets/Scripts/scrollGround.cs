using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrollGround : MonoBehaviour
{
    private float length, startPos;
    [SerializeField] private float scrollSpeed;
    [SerializeField] private SpriteRenderer oneTick;
    [SerializeField] private GameObject cam;
    

    
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position.x;

        length = (oneTick.bounds.size.x * 3) / 2;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float temp = (cam.transform.position.x * (1 - scrollSpeed));
        float amtToMove = scrollSpeed  * Time.deltaTime;
        transform.Translate(Vector3.left * amtToMove, Space.World);

        //if (temp > startPos + length) startPos += length;
        //else if (temp < startPos + length) startPos -= length;


        if (transform.position.x < - 18f)
        {
            Debug.Log("if entered");
            transform.position = new Vector3(startPos, transform.position.y, transform.position.z);
        }
    }
}
