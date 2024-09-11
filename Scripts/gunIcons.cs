using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class gunIcons : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    public GameObject pistolGun;
    public GameObject M4Gun;
    public GameObject rifleGun;
    public GameObject rocketGun;
    public GameObject shortGun;
    private playerController playerControllerScript;
     public TextMeshProUGUI Ammo; 

    
    //    gameManager=GameObject.Find("GameManager").GetComponent<GameManager>();
    
    void Start()
    {
        playerControllerScript=GameObject.Find("character").GetComponent<playerController>();
        if (playerControllerScript == null)
        {
            Debug.LogError("playerController script not found on the player object!");
        }
        else {

            Debug.LogError("playerController script found PASSED!!!");
        }
       
    }

    // Update is called once per frame
    void Update()
    {
         
        Ammo.text="Ammo: "+playerControllerScript.currentGunAmmo;

        if(playerControllerScript.currentGun.tag!=null)
        {
            
            
          if(playerControllerScript.currentGun.tag=="M4A1")
        {
         Debug.Log("M4a1Icon");
            pistolGun.GetComponent<SpriteRenderer>().enabled=false;
         shortGun.GetComponent<SpriteRenderer>().enabled=false;
         M4Gun.GetComponent<SpriteRenderer>().enabled=true;
         rocketGun.GetComponent<SpriteRenderer>().enabled=false;
         rifleGun.GetComponent<SpriteRenderer>().enabled=false;

        }
        else if(playerControllerScript.currentGun.tag=="pistol")
        {
         Debug.Log("pistolicon");
         pistolGun.GetComponent<SpriteRenderer>().enabled=true;
         shortGun.GetComponent<SpriteRenderer>().enabled=false;
         M4Gun.GetComponent<SpriteRenderer>().enabled=false;
         rocketGun.GetComponent<SpriteRenderer>().enabled=false;
         rifleGun.GetComponent<SpriteRenderer>().enabled=false;
        
        

        }
        else if(playerControllerScript.currentGun.tag=="rifle")
        {
         Debug.Log("rifleicon");
          pistolGun.GetComponent<SpriteRenderer>().enabled=false;
         shortGun.GetComponent<SpriteRenderer>().enabled=false;
         M4Gun.GetComponent<SpriteRenderer>().enabled=false;
         rocketGun.GetComponent<SpriteRenderer>().enabled=false;
         rifleGun.GetComponent<SpriteRenderer>().enabled=true;


        }
        else if(playerControllerScript.currentGun.tag=="shotGun")
        {
         Debug.Log("shortgunicon");
            pistolGun.GetComponent<SpriteRenderer>().enabled=false;
         shortGun.GetComponent<SpriteRenderer>().enabled=true;
         M4Gun.GetComponent<SpriteRenderer>().enabled=false;
         rocketGun.GetComponent<SpriteRenderer>().enabled=false;
         rifleGun.GetComponent<SpriteRenderer>().enabled=false;

        }
        else if(playerControllerScript.currentGun.tag=="rocketGun")
        {
         Debug.Log("rocketgunicon");
            pistolGun.GetComponent<SpriteRenderer>().enabled=false;
         shortGun.GetComponent<SpriteRenderer>().enabled=false;
         M4Gun.GetComponent<SpriteRenderer>().enabled=false;
         rocketGun.GetComponent<SpriteRenderer>().enabled=true;
         rifleGun.GetComponent<SpriteRenderer>().enabled=false;

        }

        }
        else
        {
             pistolGun.GetComponent<SpriteRenderer>().enabled=false;
         shortGun.GetComponent<SpriteRenderer>().enabled=false;
         M4Gun.GetComponent<SpriteRenderer>().enabled=false;
         rocketGun.GetComponent<SpriteRenderer>().enabled=false;
         rifleGun.GetComponent<SpriteRenderer>().enabled=false;
        }
       
    }
}
