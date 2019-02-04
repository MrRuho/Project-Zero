using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause_Menu : MonoBehaviour {

    public GameObject PauseUI;

    private bool paused = false;
    private bool pauseControl = false; // Kontrolloi sitä että pause ja ei pause toteutetaan vain kerran.

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

        if (paused && pauseControl == true)
        {          
            PauseUI.SetActive(true);
            Time.timeScale = 0;
            pauseControl = false;
        }
        if (!paused && pauseControl == false)
        {         
            PauseUI.SetActive(false);
            Time.timeScale = 1;
            pauseControl = true;
        }
    }

    public void Resume()
    {
        paused = false;
    }

    public void Restart()
    {     
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {   
        SceneManager.LoadScene(0);
    }
    public void Quit()
    {
        Application.Quit();
    }
}

