using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bluecarFollow : MonoBehaviour
{
     public GameObject refcam;
    
    // Start is called before the first frame update
    public Vector3 offset =new Vector3(0,1,5);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position= refcam.transform.position+offset; 
    }
}
