using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : Singleton<Pause>
{
    [SerializeField] GameObject pauseUI;

    bool isPaused = false;

    public bool paused
    {
        get { return isPaused; }
        set
        {

            isPaused = value;
            pauseUI.SetActive(isPaused);
            Time.timeScale = (isPaused) ? 0 : 1;
        }
    }

    public void PauseGame()
    {
        paused = !paused;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        GameObject player = GameObject.Find("FPSPlayer");
        player.TryGetComponent(out PlayerController controller);
        controller.camControl = false;
    }

    public void ResumeGame()
    {
        paused = !paused;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        GameObject player = GameObject.Find("FPSPlayer");
        player.TryGetComponent(out PlayerController controller);
        controller.camControl = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !paused) PauseGame();
        else if (Input.GetKeyDown(KeyCode.Escape) && paused) ResumeGame();
    }
}
