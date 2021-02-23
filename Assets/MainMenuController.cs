using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] GameObject settingsObject;
    [SerializeField] GameObject quitButton;
    [SerializeField] GameObject mainMenuCanvas;

    // Start is called before the first frame update
    void Start()
    {
#if !UNITY_STANDALONE
        quitButton.SetActive(false);
#else
        quitButton.SetActive(true); // making sure the player can quit if it's a standalone build
#endif

    }

    // Update is called once per frame
    void Update()
    {
        mainMenuCanvas.SetActive(!settingsObject.activeSelf); // this is kinda bad I'm sorry x-x -Geneva
    }

    public void GoToGame()
    {
        SceneManager.LoadScene("Prototype");
    }

/*    public void GoToTutorial() // keeping this here in case we do make the tutorial separate at some point -Geneva
    {
        
    }*/

    public void GoToSettings()
    {
        settingsObject.SetActive(true);
    }


    public void Quit()
    {
        Application.Quit();
    }
}
