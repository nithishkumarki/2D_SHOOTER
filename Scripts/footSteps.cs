using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class footSteps : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject player;
    public GameObject footStep;
    public playerController playerController;

    public bool pressedHorizontalInput2=false;
    void Start()
    {
        footStep.SetActive(false);
        playerController=player.GetComponent<playerController>();
    }

    // Update is called once per frame
    void Update()
    {
        pressedHorizontalInput2=playerController.pressedHorizontalInput;
        if(playerController.pressedHorizontalInput && !playerController.isCrouching&& playerController.isOnGround)
        {
          footStep.SetActive(true);
          
        }
        else{
            footStep.SetActive(false);
           
        }
    }
}
