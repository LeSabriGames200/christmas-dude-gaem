using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public static bool GameIsPaused = false;

    public GameObject PauseMenuThing;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            if (GameIsPaused) 
            {
                Resume();
            }else
            {
                Pause();
            }
        }
    }

    public void Resume() 
    {
      Cursor.lockState = CursorLockMode.Locked;
      Cursor.visible = false;
      PauseMenuThing.SetActive(false);
      Time.timeScale = 1f;
      GameIsPaused = false;
    }

    void Pause() 
    {
      Cursor.lockState = CursorLockMode.None;
      Cursor.visible = true;
      PauseMenuThing.SetActive(true);
      Time.timeScale = 0f;
      GameIsPaused = true;
    }

    public void Quit()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Title");
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Game");
    }
}
