using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class movingUpwardTile : MonoBehaviour
{
    // Start is called before the first frame update
    // public float xRange=5f;
    public float yRange=5f;
    public float speed=3f;
    private Vector3 originPosition;
    private Vector3 tileDirection;
    void Start()
    {
        originPosition=gameObject.transform.position;
        tileDirection =Vector3.up;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(tileDirection*Time.deltaTime*speed);
        if(gameObject.transform.position.y< originPosition.y-yRange)
        { 
            tileDirection=Vector3.up;
        }
        else if(gameObject.transform.position.y>originPosition.y+yRange)
        {
        
            tileDirection=Vector3.down;
            
        }
    }
}
