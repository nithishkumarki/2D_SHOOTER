using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveForward : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed=40f;
    public playerController player;
    private string bulletside="";
    private float minX=-30;
    private float maxX=30;
    void Start()
    {
        player=GameObject.Find("character").GetComponent<playerController>();
        if(player.rightSide)
        {    
        bulletside="rightSide";
        }
        else{
        bulletside="leftside";

        }
    }

    // Update is called once per frame
    void Update()
    {
      if(bulletside=="rightSide")
      {

        transform.Translate(Vector3.up*Time.deltaTime*speed);
      }
      else{
        transform.Translate(Vector3.down*Time.deltaTime*speed);

      }
      if(gameObject.transform.position.x<player.transform.position.x+minX||gameObject.transform.position.x>player.transform.position.x+maxX)
      {
        Destroy(gameObject);
      }

    }
}
