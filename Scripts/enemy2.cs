using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
// using UnityEngine.UIElements;
using UnityEngine.UI;


public class enemy2 : MonoBehaviour
{
    public GameObject Player;
    private Rigidbody playerRB;
    private Animator playerA;
    public float speed = 10f;
    public float jumpForce = 300f;
    public bool rightSide = true;
    public float HorizontalInput;
    public float gravityModifier = 1;
    public bool isOnGround = true;
    public float acceleration = 0.1f;
    public float deceleration = 0.5f;
    float velocity = 0f;
    float maxvelocity = 1f;
    float minVeclocity = 0f;
    
    bool hasGun = false; 
    private GameObject currentGun;
    public float pistolAmmo=1000;
    public float shotGunAmmo=1000;
    public float rifleAmmo=10;
    public float M4A1Ammo=100;
    public float rocketAmmo=5;
    public GameObject pistolBullet;
    public GameObject shotGunBullet;
    public GameObject rifleBullet;
    public GameObject M4A1Bullet;
    public Vector3 bulletoffset=new Vector3(-0.6f,0.1f,0);
    public float shortGunCDT=3;//3
    public float pistolCDT=2;//2
    public float M4A1CDT=1;//1
    public float rifleCDT=4;//4
    // private gameManager GameManager;
            public float rocketGunCDT=5;
            public float rocketGunAmmo=5;
           public GameObject rocketBullet;
    public int health=10;
    public Slider healthSlider;
    public int currentHealthHit=0;
       public float avoidFloorDistance=1f;
       public float ladderGrabDistance=0.5f;

       public string currentGunAnimation="";
       public float positionX;
       public float positionY;
       public float SearchDistance=5f;
       public float eyeDistance=10f;

                public Quaternion leftBulletRotation=Quaternion.Euler(0,180,0);
        public float originTransformX;
        bool hasPlayedWalkAnimation = false;
        bool playerNearBy=false;
        public AudioSource audioSource;
        
         public AudioClip pistolSound;
    public ParticleSystem deathExplosion;
     public bool deathSoundStarted=false;
         public AudioClip deathSound;
          playerController playerControllerScript;







    void Start()
    {
         playerControllerScript=GameObject.Find("character").GetComponent<playerController>();

      audioSource=GetComponent<AudioSource>();

        originTransformX=gameObject.transform.position.x;
        healthSlider.maxValue=health;
        healthSlider.value=0;
        healthSlider.fillRect.gameObject.SetActive(false);
        playerRB = GetComponent<Rigidbody>();
        playerA = GetComponent<Animator>();
        // GameManager=GameObject.Find("gameManager").GetComponent<gameManager>();
    }

    void LateUpdate()
    {
        


  
      shooting();
        playerMovements();
  

      


       
    }
 
    //healthSider and death of the player
   
   public void reduceSliderHealth(int hit)
    {
          if (healthSlider != null)
    {
          currentHealthHit+=hit;
        healthSlider.fillRect.gameObject.SetActive(true);
        healthSlider.value = currentHealthHit;
    }
    else
    {
        Debug.LogError("healthSlider is not assigned!");
    }
        //   healthSlider.fillRect.gameObject.SetActive(true);
        //   healthSlider.value=currentHealthHit;
          if(currentHealthHit>=health)
          {
             playerControllerScript.EnemydeathSoundFun();
            //  Debug.Log("playing death explosin and death");
           Instantiate(deathExplosion,transform.position,deathExplosion.transform.rotation);
            Destroy(gameObject);
          }

    }
  

public void playerMovements()
{
      
//player detection
       
        if(Player.transform.position.x<=  originTransformX+eyeDistance && Player.transform.position.x>=  originTransformX-eyeDistance)
        {
         
            playerA.SetBool("enemyWalk",false);
            Debug.Log("enemy is Near");
            hasPlayedWalkAnimation=true;

            
           if(Player.transform.position.x>=gameObject.transform.position.x && rightSide)
           {
               transform.Rotate(0, 180, 0);
                rightSide = false;
           }
           if(Player.transform.position.x<gameObject.transform.position.x && !rightSide)
           {
                transform.Rotate(0, 180, 0);
               rightSide = true;
           }
        if (rightSide == true)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * 0);

        }
        if (rightSide == false)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * 0 );
        }
        playerNearBy=true;
        } 
        else
        
        {
        playerNearBy=false;
            // Debug.Log("enemy is not near");

        playerA.SetBool("enemyWalk",true);
       
       
       if (rightSide == true)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);

        }
        if (rightSide == false)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed );
        }

        if (originTransformX+SearchDistance < gameObject.transform.position.x && rightSide == false)
        {
            transform.Rotate(0, 180, 0);
            rightSide = true;
        }
        if (originTransformX-SearchDistance >gameObject.transform.position.x && rightSide == true)
        {
            transform.Rotate(0, 180, 0);
            rightSide = false;
        }
        

        }

        float gunLayerWeight=1;
          if(currentGunAnimation=="rifleGunAnimation")
          {
           gunLayerWeight= Mathf.Abs(HorizontalInput)>0?0.7f:1f;    //run=0.7 standing1       //1f:0.8f;
           playerA.SetBool("rifle",true);
           playerA.SetBool("pistol",false);

          }
          else if(currentGunAnimation=="pistolGunAnimation")
          {
           gunLayerWeight= Mathf.Abs(HorizontalInput)>0?1f:0.8f;    //run=0.7 standing1       //1f:0.8f;
           playerA.SetBool("rifle",false);
           playerA.SetBool("pistol",true);
            
          }

          playerA.SetLayerWeight(1,gunLayerWeight);// 1 is the index of the layer for baselayer=0 for gun= 1 
          
          
           
     
       



       
}
     void shooting()
    {
        if(hasGun&& playerNearBy)
        {

            if(currentGun.tag=="pistol")
            {
                // currentAnimation="pistolAnimation";
                // currentAnimation="rifleAnimation";
            if(pistolCDT<=0&&pistolAmmo>0)
            {
                audioSource.PlayOneShot(pistolSound,1.0f);

            if(rightSide)
            {
            Instantiate(M4A1Bullet,currentGun.transform.position+new Vector3(-0.9f,0.2f,0f),rifleBullet.transform.rotation);
            }
            else{
            Instantiate(M4A1Bullet,currentGun.transform.position+new Vector3(1f,0.2f,0),rifleBullet.transform.rotation);

            }
            pistolAmmo-=1;
            pistolCDT=0.5f;
            }
            }

           
        }
        
        if(pistolCDT>0)
        {
            pistolCDT-=Time.deltaTime;
        }
       
    }
    void OnCollisionEnter(Collision collision)
    {

        if(collision.gameObject.CompareTag("M4A1Bullet"))
        {
       
           reduceSliderHealth(1);
           Destroy(collision.gameObject);
        }
        if(collision.gameObject.CompareTag("shotGunBullet"))
        {
      
           reduceSliderHealth(5);
           Destroy(collision.gameObject);
        }
        if(collision.gameObject.CompareTag("rifleBullet"))
        {
    
           reduceSliderHealth(10);
           Destroy(collision.gameObject);
        }
        if(collision.gameObject.CompareTag("rocketBullet"))
        {
        //    health=health-10;
           reduceSliderHealth(20);
           Destroy(collision.gameObject);
        }
        
           
        if (collision.gameObject.CompareTag("ground"))
        {
            isOnGround = true;
            playerA.SetBool("jump", false);
            playerA.SetBool("runjumping", false);
        }

        if(collision.gameObject.CompareTag("pistolAmmoPack"))
        {
                

            Destroy(collision.gameObject);
            pistolAmmo+=10;
        }
        if(collision.gameObject.CompareTag("M4A1AmmoPack"))
        {
            Destroy(collision.gameObject);
            M4A1Ammo+=10;
        }
        if(collision.gameObject.CompareTag("shotGunAmmoPack"))
        {
            Destroy(collision.gameObject);
            shotGunAmmo+=10;
        }
        if(collision.gameObject.CompareTag("rifleAmmoPack"))
        {
            Destroy(collision.gameObject);
            rifleAmmo+=10;
        }
        if(collision.gameObject.CompareTag("rocketAmmoPack"))
        {
            Destroy(collision.gameObject);
            rocketAmmo+=10;
        }


      
       if (collision.gameObject.CompareTag("pistol"))
        {
        currentGunAnimation="pistolGunAnimation";
          if(hasGun)
          {
            Destroy(currentGun);
          }
         // Find the right hand bone of the player's rig
        Transform rightHand = FindRightHandBone(transform);
        Vector3 riflePosition = new Vector3(-0.097f, -0.091f,0.009f);
        Vector3 rifleRotation = new Vector3(-54.887f, 72.895f, -76.414f);

        if (rightHand != null)
        {
        collision.transform.SetParent(rightHand);
        collision.transform.localPosition = riflePosition;
        collision.transform.localRotation = Quaternion.Euler(rifleRotation);
        hasGun = true;
        currentGun=collision.gameObject;
        }
        }
       

        

    }

    // Recursively find the right hand bone in the player's rig
    Transform FindRightHandBone(Transform parent)
    {
        if (parent == null)
            return null;

        if (parent.name == "mixamorig:RightHand" ) // Adjust this according to the naming convention in your rig
            return parent;
        else  if(parent.name == "RightHand" ) // Adjust this according to the naming convention in your rig
            return parent;
        

        foreach (Transform child in parent)
        {
            Transform result = FindRightHandBone(child);
            if (result != null)
                return result;
        }

        return null;
    }

}
