using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public Canvas pauseCanvas;
    public Mouse mouse;

    private bool menuEnabled = false;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (menuEnabled)
            {
                mouse.enabled = true;
                Time.timeScale = 1;
                pauseCanvas.gameObject.SetActive(false);
                menuEnabled = false;
            } else
            {
                mouse.enabled = false;
                Time.timeScale = 0;
                pauseCanvas.gameObject.SetActive(true);
                menuEnabled = true;
            }

        }
    }
}
