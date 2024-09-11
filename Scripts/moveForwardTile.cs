using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class movingForwardTile : MonoBehaviour
{
   
    public float xRange = 5f;
    public float speed = 3f;
    private Vector3 originPosition;
    private Vector3 tileDirection;
    private Vector3 lastPosition;

    void Start()
    {
        originPosition = gameObject.transform.position;
        tileDirection = Vector3.left;
        lastPosition = transform.position;
    }

    void Update()
    {
        transform.Translate(tileDirection * Time.deltaTime * speed);

        if (gameObject.transform.position.x <= originPosition.x - xRange)
        {
            tileDirection = Vector3.right;
        }
        else if (gameObject.transform.position.x > originPosition.x + xRange)
        {
            tileDirection = Vector3.left;
        }
    }

    public Vector3 GetTileMovement()
    {
        Vector3 movement = transform.position - lastPosition;
        lastPosition = transform.position;
        return movement;
    }
}
