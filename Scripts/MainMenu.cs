using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;



public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public  Button start;
    public  Button options;
    public  Button exit;
    public  Button chapter1;
    public  Button chapter2;
    public  Button chapter3;
    public  Button back;
    public  Button backToOptions;
    public  Button controls;
    public TextMeshProUGUI main;
    public TextMeshProUGUI controlText;
    public TextMeshProUGUI menu;
    public TextMeshProUGUI Continue;
    public TextMeshProUGUI chapters;
    public TextMeshProUGUI settingsText;
    public TextMeshProUGUI storyText;
    public  Button storyBack;
    public  Button storyLine;
    
    void Start()
    {
        storyText.gameObject.SetActive(false);
        storyBack.gameObject.SetActive(false);
        storyLine.gameObject.SetActive(true);
        settingsText.gameObject.SetActive(false);
        controls.gameObject.SetActive(false);
        main.gameObject.SetActive(true);
        menu.gameObject.SetActive(true);
        Continue.gameObject.SetActive(false);
        chapters.gameObject.SetActive(false);

        start.gameObject.SetActive(true);
        options.gameObject.SetActive(true);
        exit.gameObject.SetActive(true);
        chapter1.gameObject.SetActive(false);
        chapter2.gameObject.SetActive(false);
        chapter3.gameObject.SetActive(false);
        back.gameObject.SetActive(false);
        controlText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startFunction()
    {
         main.gameObject.SetActive(false);
        menu.gameObject.SetActive(false);
        Continue.gameObject.SetActive(true);
        chapters.gameObject.SetActive(true);
        settingsText.gameObject.SetActive(false);


        start.gameObject.SetActive(false);
        storyLine.gameObject.SetActive(false);
        options.gameObject.SetActive(false);
        exit.gameObject.SetActive(false);
        chapter1.gameObject.SetActive(true);
        chapter2.gameObject.SetActive(true);
        chapter3.gameObject.SetActive(true);
        back.gameObject.SetActive(true);
        backToOptions.gameObject.SetActive(false);

    }
    public void optionsFunction()
    {
        settingsText.gameObject.SetActive(true);
        controls.gameObject.SetActive(true);
        main.gameObject.SetActive(false);
        menu.gameObject.SetActive(false);
        Continue.gameObject.SetActive(false);
        chapters.gameObject.SetActive(false);

        start.gameObject.SetActive(false);
        storyLine.gameObject.SetActive(false);
        options.gameObject.SetActive(false);
        exit.gameObject.SetActive(false);
        chapter1.gameObject.SetActive(false);
        chapter2.gameObject.SetActive(false);
        chapter3.gameObject.SetActive(false);
        back.gameObject.SetActive(true);
        backToOptions.gameObject.SetActive(false);
        
    }
    public void backFunciton()
    {
        settingsText.gameObject.SetActive(false);

        controls.gameObject.SetActive(false);
          main.gameObject.SetActive(true);
        menu.gameObject.SetActive(true);
        Continue.gameObject.SetActive(false);
        chapters.gameObject.SetActive(false);

          start.gameObject.SetActive(true);
           storyLine.gameObject.SetActive(true);
        options.gameObject.SetActive(true);
        exit.gameObject.SetActive(true);
        chapter1.gameObject.SetActive(false);
        chapter2.gameObject.SetActive(false);
        chapter3.gameObject.SetActive(false);
        back.gameObject.SetActive(false);
    }
    public void backToOptionsFunction()
    {
        settingsText.gameObject.SetActive(true);

        controls.gameObject.SetActive(true);
        back.gameObject.SetActive(true);

        main.gameObject.SetActive(false);
        menu.gameObject.SetActive(false);
        Continue.gameObject.SetActive(false);
        chapters.gameObject.SetActive(false);
        start.gameObject.SetActive(false);
         storyLine.gameObject.SetActive(false);
        options.gameObject.SetActive(false);
        exit.gameObject.SetActive(false);
        chapter1.gameObject.SetActive(false);
        chapter2.gameObject.SetActive(false);
        chapter3.gameObject.SetActive(false);
        backToOptions.gameObject.SetActive(false);
        controlText.gameObject.SetActive(false);
    }
    public void ControlsFunction()
    {
        settingsText.gameObject.SetActive(false);

        controlText.gameObject.SetActive(true);
        backToOptions.gameObject.SetActive(true);
          controls.gameObject.SetActive(false);
          back.gameObject.SetActive(false);
    }
    public void storyLineFunction()
    {
        main.gameObject.SetActive(false);
        menu.gameObject.SetActive(false);

        start.gameObject.SetActive(false);
        storyLine.gameObject.SetActive(false);
         settingsText.gameObject.SetActive(false);
        options.gameObject.SetActive(false);
        exit.gameObject.SetActive(false);

        storyBack.gameObject.SetActive(true);
        storyText.gameObject.SetActive(true);
      
      
    }
    public void storyBackFunction()
    {
          main.gameObject.SetActive(true);
        menu.gameObject.SetActive(true);

        start.gameObject.SetActive(true);
        storyLine.gameObject.SetActive(true);
         settingsText.gameObject.SetActive(false);
        options.gameObject.SetActive(true);
        exit.gameObject.SetActive(true);

        storyBack.gameObject.SetActive(false);
        storyText.gameObject.SetActive(false);
    }
    public void exitGame()
    {
        Application.Quit();
    }
    public void chapter1Function()
    {
        SceneManager.LoadScene("Chapter11");
    }
    public void chapter2Function()
    {
        SceneManager.LoadScene("chapter2");
    }
    public void chapter3Function()
    {
        SceneManager.LoadScene("Chapter3");
    }
    public void mainMenuFunction()
    {
        SceneManager.LoadScene("StartingScene");
    }

}
