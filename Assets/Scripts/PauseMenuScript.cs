using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuScript : MonoBehaviour
{

    public GameObject pauseMenu;

    public GameObject androidUI;


    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible= false;

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(!pauseMenu.activeSelf)
            {
                Time.timeScale = 0f;
                androidUI.SetActive(false);
                pauseMenu.SetActive(true);
                Cursor.visible = true;
            
            }else{
                Time.timeScale = 1f;
                pauseMenu.SetActive(false);
                androidUI.SetActive(true);
                Cursor.visible = false;
            }
        }
    }

    public void quit()
    {
        Application.Quit();
    }
    public void resume()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        androidUI.SetActive(true);
        Cursor.visible = false;
    }    
}
