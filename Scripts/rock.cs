using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class rock : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject rockobject;
    public Rigidbody rockRb;
    public float rockforce=8000f;
    public float rockPerforce=100f;
    public Vector3 rockDirection=new Vector3(1,0,0);
    public bool rockTriggerStarted=false;
        public AudioSource audioSource;
         public AudioClip rockSound;


    void Start()
    {
      audioSource=GetComponent<AudioSource>();

        // rockRb=rockobject.GetComponent<Rigidbody>();
        if (rockobject != null)
        {
            rockRb = rockobject.GetComponent<Rigidbody>();
            if (rockRb == null)
            {
                Debug.LogError("rockObject does not have a Rigidbody component.");
            }
        }
        else
        {
            Debug.LogError("rockObject is not assigned.");
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(rockTriggerStarted)
        {
            // rockRb.AddForce(rockDirection*3000*Time.deltaTime,ForceMode.Impulse);
        }
    }
      private void OnTriggerEnter(Collider player)
    {
        if(player.CompareTag("Player"))
        {
                audioSource.PlayOneShot(rockSound,1.0f);

            Debug.Log("rockTriggered");
            rockRb.AddForce(rockDirection * rockforce, ForceMode.Impulse);
            rockTriggerStarted=true;
           
            
        }
    }

    
}
