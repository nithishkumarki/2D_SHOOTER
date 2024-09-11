using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;


public class playerController : MonoBehaviour
{
    private Rigidbody playerRB;
    private Animator playerA;
    public float glidingSpeed = 10f;
    public float speed=7.5f;
    public float jumpSpeed=7.5f;
    public float jumpForce = 7.5f;
    public bool rightSide = true;
    public float HorizontalInput;
    public bool isOnGround = true;
    public float acceleration = 0.1f;
    public float deceleration = 0.5f;
    float velocity = 0f;
    float maxvelocity = 1f;
    float minVeclocity = 0f;
    
    bool hasGun = false; 
    public GameObject currentGun;
    public float pistolAmmo=10;
    public float shotGunAmmo=100;
    public float rifleAmmo=10;
    public float M4A1Ammo=10;
    public float rocketAmmo=5;

    public float currentGunAmmo=0;
    public GameObject pistolBullet;
    public GameObject shotGunBullet;
    public GameObject rifleBullet;
    public GameObject M4A1Bullet;
    public Vector3 bulletoffset=new Vector3(-0.6f,0.1f,0);
    public float shortGunCDT=3;//3
    public float pistolCDT=2;//2
    public float M4A1CDT=1;//1
    public float rifleCDT=4;//4
    public float dropGunCDT=2;
    // private gameManager GameManager;
            public float rocketGunCDT=5;
            public float rocketGunAmmo=5;
           public GameObject rocketBullet;
    public int health=100;
    
    public Slider healthSlider;
    public int currentHealthHit=0;
       public float avoidFloorDistance=1f;
       public float ladderGrabDistance=0.5f;

       public string currentGunAnimation="";
       public float positionX;
       public float positionY;
       public float VerticalalInput;
             public float deathTime=3;
             public bool deatStarted=false;

                public Quaternion leftBulletRotation=Quaternion.Euler(0,180,0);
        public bool isCrouching=false;
        float timePassed = 0f;
        public AudioSource audioSource;
         public AudioClip shortGunSound;
         public AudioClip pistolSound;
         public AudioClip rifleSound;
         public AudioClip rocketGunSound;
         public AudioClip M4A1Sound;
         public AudioClip deathSound;
        //  public AudioClip footStepsSound;
         public AudioClip jumpSound;
         public bool pressedHorizontalInput=false;
        float gunLayerWeight=1;
        bool pickWepon=false;
        public GameObject player;
        private float flying=0;
        public float speedRb=10f;
      
    public float gravityModifier = 1;
//   public float normalGravityScale = 1f; // Original gravity scale
    public float flyingGravityScale = 0.5f; // Gravity scale when flying
        

    public Vector3 realGravityVector=new Vector3(0,-9.8f,0);
    public Vector3 gliderGravityVector=new Vector3(0,0.0f,0);
    private Vector3 currentGravityVector;
    public bool isGlyding=false;

    public float gliderActivated=1;// when glying gravity is activated still it accelerates due to the regular gravityy 0f -9.8 for 3 seconds to have imediated impact when glyding starts at beging i have set a upward force

     private Transform originalParent;
       private Transform currentTile;
    private bool onMovingTile = false;
    private bool inTest=false;

    public ParticleSystem deathExplosion;
     public bool deathSoundStarted=false;
     public bool deathSoundStarted2=false;

    public TextMeshProUGUI gameOver;
    public  Button restartButton;

    public AudioClip EnemydeathSound;
    
   

    

      
         
     public void EnemydeathSoundFun()
     {
        Debug.Log("enemydeath called");
        audioSource.PlayOneShot(EnemydeathSound,1.0f);
        
     }
    void Start()
    {
        // gameOver.gameObject.SetActive(true);
         
         originalParent = transform.parent;
      audioSource=GetComponent<AudioSource>();
   
         Physics.gravity=realGravityVector;
        healthSlider.maxValue=health;
        healthSlider.value=0;
        healthSlider.fillRect.gameObject.SetActive(false);
        playerRB = GetComponent<Rigidbody>();
        playerA = GetComponent<Animator>();
        // GameManager=GameObject.Find("gameManager").GetComponent<gameManager>();
    }
     void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("movingTile"))
        {
            Debug.Log("exited the tile");
            currentTile = null;
            onMovingTile = false;
        }
         if (collision.gameObject.CompareTag("test"))
        {
            Debug.Log("came out of test");
           inTest=false;
        }
    }
    void LateUpdate()
    {
      
         if (onMovingTile && currentTile != null)
        {
            Debug.Log("on a tile");
            movingForwardTile tileScript = currentTile.GetComponent<movingForwardTile>();
            Vector3 tileMovement = tileScript.GetTileMovement();
            transform.position += tileMovement;
        }

         if (inTest)
        {
            Debug.Log("inTest in update");
           
        }

        //    Physics.gravity=gravityVector;
        if(isOnGround||isGlyding)
        {
            speed=glidingSpeed;
        }
        else{
            speed=jumpSpeed;
        }
        
        
        
        if(flying>=2)
        {
            isGlyding=true;
           if(gliderActivated==1)
           {
            playerRB.AddForce(Vector3.up * (jumpForce/2), ForceMode.Impulse);
            gliderActivated=2;
             
           }
        Physics.gravity = gliderGravityVector;
        
        }
        else{
        Physics.gravity = realGravityVector;
        isGlyding=false;

        }
        if(Input.GetKey(KeyCode.Space)&&!isOnGround)
        {
           
            //    flying=+(Time.deltaTime);
               flying+=(Time.deltaTime)*2;
          

                
               if(flying>=3)
               {
                flying=3;
               }
            //    if(flying>=25)
            //    {

            //      playerRB.mass= flyingGravityScale;
            //    }
               
        }
        else{
            //  flying=-Time.deltaTime*4;
             flying-=Time.deltaTime*5;
            //  playerRB.mass=gravityModifier;
        
            if(flying<=0)
            {
                flying=0;
            }
        }
        // Debug.Log(flying);
               playerA.SetFloat("flying",flying);
          ladderFun();
  
           shooting();
        playerMovements();
        //Destroying player after 3 seconds
         
            if(deatStarted )
            {
                deathTime=deathTime-3*Time.deltaTime;
                Debug.Log("deathtime"+deathTime);
                deathSoundStarted=true;




            }
            if(deathSoundStarted&& !deathSoundStarted2)
            {
            audioSource.PlayOneShot(deathSound,1.0f);
            deathSoundStarted2=true;

            }
             if(deathTime<=0)
             {
               restartButton.gameObject.SetActive(true);
            Instantiate(deathExplosion,transform.position,deathExplosion.transform.rotation);
            gameOver.gameObject.SetActive(true);
                Destroy(gameObject);
             }

        //Gun detaching
         if(Input.GetKeyDown(KeyCode.E)&&hasGun)
         {
            if(dropGunCDT<=0)
            {
              dropGun();

            Debug.Log("Pressed drop gun");
               
            }
        }
        if(dropGunCDT>0)
        {
            dropGunCDT-=Time.deltaTime;
        }
       
       

       
    }
   

   

    public void ladderFun()
    {
        RaycastHit hit;
    if (Physics.Raycast(transform.position + Vector3.up * avoidFloorDistance, transform.forward, out hit, ladderGrabDistance))
    {
    //   Debug.Log(hit.transform);  
      if(hit.transform.TryGetComponent(out ladder ladder))
      {
              if(Input.GetKey(KeyCode.W))
              {
                transform.position=transform.position+Vector3.up*speed*Time.deltaTime;
              }
              if(Input.GetKey(KeyCode.S))
              {
                transform.position=transform.position-Vector3.down*speed*Time.deltaTime;
              }
             
      }
    }
        
    }
 
    //healthSider and death of the player
   
    public void reduceSliderHealth(int hit)
    {
          currentHealthHit+=hit;
          healthSlider.fillRect.gameObject.SetActive(true);
          healthSlider.value=currentHealthHit;
          if(currentHealthHit>=health)
          {
            playerA.SetBool("death",true);
             Destroy(currentGun);
             deatStarted=true;
                // audioSource.PlayOneShot(deathSound,1.0f);
            
            //  Destroy(gameObject);
          }

    }

public void playerMovements()
{
        //ray cast is a invisble line to detect

     if(Input.GetKey(KeyCode.F))
     {
         playerA.SetBool("dance",true);
     }
    else{
         playerA.SetBool("dance",false);
  }
 
        HorizontalInput = Input.GetAxis("Horizontal");
        if(HorizontalInput!=0)
        {
            pressedHorizontalInput=true;
        }
        else{
            pressedHorizontalInput=false;
        }
        VerticalalInput = Input.GetAxis("Vertical");

        if(VerticalalInput<0)
        {
            playerA.SetFloat("crouch",VerticalalInput);
        }

        if(VerticalalInput>-1 && VerticalalInput<0)
        {
            VerticalalInput=0;
            isCrouching=false;
        }
        else if(VerticalalInput<=-1)
        {
            VerticalalInput=-1;
            isCrouching=true;
        }
        
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

        //move leftand right
        if (rightSide == true&& !isCrouching)
        {
            transform.Translate(Vector3.forward * Time.fixedDeltaTime * speed * HorizontalInput);
            // playerRB.AddForce(Vector3.forward*Time.deltaTime *HorizontalInput* speedRb, ForceMode.Impulse);

            
            // audioSource.PlayOneShot(footStepsSound,1.0f);
        }
        if (rightSide == false&& !isCrouching)
        {
            // audioSource.PlayOneShot(footStepsSound,1.0f);
            transform.Translate(Vector3.back * Time.fixedDeltaTime * speed * HorizontalInput);
            // playerRB.AddForce(Vector3.back*Time.deltaTime *HorizontalInput* speedRb, ForceMode.Impulse);

        }
        //turn left or right
        if (HorizontalInput > 0 && rightSide == false)
        {
            transform.Rotate(0, 180, 0);
            rightSide = true;
        }
        if (HorizontalInput < 0 && rightSide == true)
        {
            transform.Rotate(0, 180, 0);
            rightSide = false;
        }
         //jumping
        bool isjumppressed = Input.GetKeyDown(KeyCode.Space);
        bool iswalking = (HorizontalInput > 0 || HorizontalInput < 0);

        if (isjumppressed && isOnGround && !iswalking&& !isCrouching)
        {
            
            playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
            audioSource.PlayOneShot(jumpSound,1.0f);
       
            playerA.SetBool("jump", true);
            gliderActivated=1;

        }

        if (iswalking && velocity < 1)
        {
            velocity += Time.deltaTime * acceleration;
        }
        if (!iswalking && velocity > 0.0)
        {
            velocity -= Time.deltaTime * deceleration;
        }
        playerA.SetFloat("velocity", velocity);
        if (iswalking && velocity > maxvelocity)
        {
            velocity = maxvelocity;
        }
        if (!iswalking && velocity < minVeclocity)
        {
            velocity = minVeclocity;
        }

        if (iswalking && velocity > 0.01 && isjumppressed && isOnGround)
        {
            playerA.SetBool("runjumping", true);
            gliderActivated=1;

            audioSource.PlayOneShot(jumpSound,1.0f);

            playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
        }
}
    private void shooting()
    {
        if(Input.GetKey(KeyCode.Mouse0)&&hasGun&& !isCrouching)
        {
            if(currentGun.tag=="M4A1")
            {
            if(M4A1CDT<=0&&M4A1Ammo>0)
            {
                audioSource.PlayOneShot(pistolSound,1.0f);
            if(rightSide)
            {
            Instantiate(M4A1Bullet,currentGun.transform.position+new Vector3(-0.9f,0.1f,0),rifleBullet.transform.rotation);
            }
            else{
            Instantiate(M4A1Bullet,currentGun.transform.position+new Vector3(0.9f,0.1f,0),rifleBullet.transform.rotation);

            }
             currentGunAmmo-=1;
             M4A1Ammo-=1;//ammo reducingg
             M4A1CDT=0.1f;//bullet time gap
            }
            }

            if(currentGun.tag=="pistol")
            {
                // currentAnimation="pistolAnimation";
                // currentAnimation="rifleAnimation";
            if(pistolCDT<=0&&pistolAmmo>0)
            {
             currentGunAmmo-=1;

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

            if(currentGun.tag=="rifle")
            {
                // currentAnimation="rifleAnimation";
            if(rifleCDT<=0&&rifleAmmo>0)
            {
             currentGunAmmo-=1;
                audioSource.PlayOneShot(rifleSound,1.0f);
            if(rightSide)
            {
            Instantiate(rifleBullet,currentGun.transform.position+new Vector3(-1.9f,0.15f,0),rifleBullet.transform.rotation);
            }
            else{
            Instantiate(rifleBullet,currentGun.transform.position+new Vector3(1.8f,0.15f,0),rifleBullet.transform.rotation);

            }
            rifleAmmo-=1;
            rifleCDT=4;

            }    
            }
            if(currentGun.tag=="shotGun")
            {
                // currentAnimation="rifleAnimation";
            if(shortGunCDT<=0&&shotGunAmmo>0)
            {
             currentGunAmmo-=1;

                audioSource.PlayOneShot(shortGunSound,1.0f);
            if(rightSide)
            {
            Instantiate(shotGunBullet,currentGun.transform.position+new Vector3(-1.5f,0.15f,0),rifleBullet.transform.rotation);
            }
            else
            {
            Instantiate(shotGunBullet,currentGun.transform.position+new Vector3(1.5f,0.15f,0),rifleBullet.transform.rotation);

            }
            shotGunAmmo-=1;
            shortGunCDT=1;

            }

            }
            
            if(currentGun.tag=="rocketGun")
            {
                // currentAnimation="pistolAnimation";
            if(rocketGunCDT<=0&&rocketAmmo>0)
            {
             currentGunAmmo-=1;
                audioSource.PlayOneShot(rocketGunSound,1.0f);
            if(rightSide)
            {
            Instantiate(rocketBullet,currentGun.transform.position+new Vector3(-1.8f,-0.05f,0),rocketBullet.transform.rotation);
            }
            else
            {
            Instantiate(rocketBullet,currentGun.transform.position+new Vector3(2,-0.05f,0),rocketBullet.transform.rotation);
            }
            rocketGunAmmo-=1;
            rocketGunCDT=5;
            

            }

            }
        }
        if(rifleCDT>0)
        {
            rifleCDT-=Time.deltaTime;
        }
        if(pistolCDT>0)
        {
            pistolCDT-=Time.deltaTime;
        }
        if(shortGunCDT>0)
        {
            shortGunCDT-=Time.deltaTime;
        }
        if(M4A1CDT>0)
        {
            M4A1CDT-=Time.deltaTime;
        }
        if(rocketGunCDT>0)
        {
            rocketGunCDT-=Time.deltaTime;
        }
        
    }
    private void OnTriggerStay(Collider collision)
    {
      

        if (collision.gameObject.CompareTag("M4A1") && Input.GetKeyDown(KeyCode.E)&& !hasGun)
        {
            currentGunAmmo=M4A1Ammo;
                Debug.Log("M4A1 Gun detected");

            // if( Input.GetKey(KeyCode.E))
            
                Debug.Log("M4A1 Gun detected and key pressed");
                 currentGunAnimation="rifleGunAnimation";
        Vector3 riflePosition = new Vector3(-0.105f,0.362f, -0.081f);
        Vector3 rifleRotation = new Vector3(30.221f, 17.016f, 99.066f);

          if(hasGun)
          {
            Destroy(currentGun);
          }
        Transform rightHand = FindRightHandBone(transform);

        if (rightHand != null)
        {
        collision.transform.SetParent(rightHand);
        collision.transform.localPosition = riflePosition;
        collision.transform.localRotation = Quaternion.Euler(rifleRotation);
        hasGun = true;
        currentGun=collision.gameObject;
        }
          dropGunCDT=2;  
       
        }
        
        if (collision.gameObject.CompareTag("rifle") && Input.GetKeyDown(KeyCode.E)&&!hasGun)
        {
            currentGunAmmo=rifleAmmo;
          if(hasGun)
          {
            Destroy(currentGun);
          }
         // Find the right hand bone of the player's rig
        currentGunAnimation="pistolGunAnimation";
        Transform rightHand = FindRightHandBone(transform);
        Vector3 riflePosition = new Vector3(-0.31f, -0.12f,0.04f);
        Vector3 rifleRotation = new Vector3(-57.818f, 69.729f, -83.446f);

        if (rightHand != null)
        {
        collision.transform.SetParent(rightHand);
        collision.transform.localPosition = riflePosition;
        collision.transform.localRotation = Quaternion.Euler(rifleRotation);
        hasGun = true;
        currentGun=collision.gameObject;
        }
          dropGunCDT=2;  

        }
        if (collision.gameObject.CompareTag("pistol") && Input.GetKeyDown(KeyCode.E)&&!hasGun)
        {
            currentGunAmmo=pistolAmmo;
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
          dropGunCDT=2;  

        }
        if (collision.gameObject.CompareTag("shotGun") && Input.GetKeyDown(KeyCode.E)&&!hasGun)
        {
            currentGunAmmo=shotGunAmmo;
        currentGunAnimation="rifleGunAnimation";
          if(hasGun)
          {
            Destroy(currentGun);
          }
         // Find the right hand bone of the player's rig
        Transform rightHand = FindRightHandBone(transform);
        Vector3 riflePosition = new Vector3(0.107f,0.103f,0.224f);
        Vector3 rifleRotation = new Vector3(-142.24f,18.325f,80.528f);

        if (rightHand != null)
        {
        collision.transform.SetParent(rightHand);
        collision.transform.localPosition = riflePosition;
        collision.transform.localRotation = Quaternion.Euler(rifleRotation);
        hasGun = true;
        currentGun=collision.gameObject;
        }
          dropGunCDT=2;  

        }
        if (collision.gameObject.CompareTag("rocketGun") && Input.GetKeyDown(KeyCode.E)&&!hasGun)
        {
            currentGunAmmo=rocketAmmo;
        currentGunAnimation="pistolGunAnimation";
          if(hasGun)
          {
            Destroy(currentGun);
          }
         // Find the right hand bone of the player's rig
        Transform rightHand = FindRightHandBone(transform);
        Vector3 riflePosition = new Vector3(-0.28f,-0.49f,0.11f);
        Vector3 rifleRotation = new Vector3(-173.106f,-12.05902f, 297.449f);

        if (rightHand != null)
        {
        collision.transform.SetParent(rightHand);
        collision.transform.localPosition = riflePosition;
        collision.transform.localRotation = Quaternion.Euler(rifleRotation);
        hasGun = true;
        currentGun=collision.gameObject;
        }
          dropGunCDT=2;  

        }
        
    }
    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("1to2"))
        {
     
        SceneManager.LoadScene("chapter2");
    } 
        if(collision.gameObject.CompareTag("2to3"))
        {
     
        SceneManager.LoadScene("Chapter3");
    } 
      if(collision.gameObject.CompareTag("3to1"))
        {
     
        SceneManager.LoadScene("StaringScene");
    } 
        
          if(collision.gameObject.CompareTag("deadLine"))
        {
            currentHealthHit=100;
            reduceSliderHealth(100);
        }
        if(collision.gameObject.CompareTag("test"))
        {
            Debug.Log("interacted with test");
            inTest=true;
        }
         if (collision.gameObject.CompareTag("movingTile"))
        {
            currentTile = collision.transform;
            onMovingTile = true;
            Debug.Log("interacted wiht a tiel");
        }
         
       if (collision.gameObject.CompareTag("movingTile"))
        {
            transform.SetParent(collision.transform);
        }

        if(collision.gameObject.CompareTag("M4A1Bullet"))
        {
        //    health=health-1;
           reduceSliderHealth(1);
           Destroy(collision.gameObject);
        }
        if(collision.gameObject.CompareTag("shotGunBullet"))
        {
        //    health=health-5;
           reduceSliderHealth(5);
           Destroy(collision.gameObject);
        }
        if(collision.gameObject.CompareTag("rifleBullet"))
        {
        //    health=health-10;
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


        //GunSwaping when player collides with the game object
       // pickWepon = ;
      /* if(Input.GetKeyDown(KeyCode.Alpha1))
       {
        Debug.Log("1 key pressed");
       }*/
       

        

    }

    // Recursively find the right hand bone in the player's rig
    private Transform FindRightHandBone(Transform parent)
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

    public void dropGun()
    {
        hasGun=false;
        if(currentGun!=null)
        {
            // currentGun.transform.SetParent(null);
             currentGun.transform.SetParent(null); // Detach the gun from the player
        // currentGun.gameObject.transform.Translate(currentGun.gameObject.transform.position.x,currentGun.gameObject.transform.position.y-1,currentGun.gameObject.transform.position.z);
        //  Rigidbody gunRB = currentGun.AddComponent<Rigidbody>(); 
        //   BoxCollider gunCollider = currentGun.AddComponent<BoxCollider>();// Add a Rigidbody component to the gun
        //  gunRB.interpolation = RigidbodyInterpolation.Interpolate; // Set interpolation for smoother movement
        //  gunRB.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic; // Set continuous collision detection for fast-moving objects
        // Optionally, you can apply some force to make the gun drop with some velocity:
        //  gunRB.AddForce(transform.forward * 2, ForceMode.VelocityChange);

        Vector3 oldGunPosition=player.transform.position;
        oldGunPosition.y=-1;
        oldGunPosition.x=+1;
        Debug.Log(player.transform.position+" "+oldGunPosition);

        currentGun.transform.position=player.transform.position;


        // Debug.Log("Position"+gameObject.transform.position.x+gameObject.transform.position.y+gameObject.transform.position.z);
        // Debug.Log("currentgunPosition"+currentGun.gameObject.transform.position.x+currentGun.gameObject.transform.position.y+currentGun.gameObject.transform.position.z);



        }
        if(currentGunAnimation=="pistolGunAnimation")
        {
            playerA.SetBool("pistol",false);
        }
        else{
            playerA.SetBool("rifle",false);
        }
        currentGunAnimation="";
        // playerA.SetTrigger("standingIdle");
    }

}
