using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause_Menu : MonoBehaviour {

    public GameObject PauseUI;

    private bool paused = false;

    private void Start()
    {
        PauseUI.SetActive(false);   
    }

    private void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            paused = !paused;
        }

        if (paused)
        {
            PauseUI.SetActive(true);
            Time.timeScale = 0;
        }
        if (!paused)
        {
            PauseUI.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void Resume()
    {
        paused = false;
    }

    public void Restart()
    {
        //Application.LoadLevel(Application.loadedLevel); //vanha malli tehda. Ala kayta ellei ole pakko. Antaa varoituksen mutta toimii.
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        //Application.LoadLevel(0); //vanha malli tehda. Ala kayta ellei ole pakko. Antaa varoituksen mutta toimii.
        SceneManager.LoadScene(0);
    }
    public void Quit()
    {
        Application.Quit();
    }

}

