using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class gameManager : MonoBehaviour
{
    // Start is called before the first frame update
    private int score=0;
    // public TextMeshProUGUI currentAmmoText;
    private int lives=50;
    public GameObject player;
    private playerController playerController;
    public AudioSource audioSource;
    // public TextMeshProUGUI gameOver;
    public GameObject pauseScreen;
    private bool paused;
        public  Button mainMenu;
    public  Button options;
    public  Button reStartChapter;

    int flag=0;

    
    
    void Start()
    {

        // currentAmmoText.text="Ammo: ";
        playerController=player.GetComponent<playerController>();
        audioSource=GetComponent<AudioSource>();
        // gameOver.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if(flag==0)
        {
            ChangePaused();
            flag=1;
        }
        if(flag==1)
        {
            ChangePaused();
            flag=2;
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            ChangePaused();
        }
    }
    public void reduceLife(int hit)
    {
         lives=lives-hit;
         
    }
    public void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void mainMenuFunction()
    {
        SceneManager.LoadScene("StaringScene");
    }  
void ChangePaused()
{
if (!paused)
{
paused = true;
pauseScreen.SetActive(true);

mainMenu.gameObject.SetActive(true);
// options.gameObject.SetActive(true);
reStartChapter.gameObject.SetActive(true);
Time.timeScale = 0;
}
else
{
paused = false;
reStartChapter.gameObject.SetActive(false);
pauseScreen.SetActive(false);
mainMenu.gameObject.SetActive(false);
options.gameObject.SetActive(false);
Time.timeScale = 1;
}
}

  
}
