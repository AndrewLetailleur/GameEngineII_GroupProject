using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameMenu : MonoBehaviour {

    public void Start()//if it ain't already, re-enable cursors, JNC
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void PlayGame() { SceneManager.LoadScene("Inside", LoadSceneMode.Single); }
    public void BackToMenu() { SceneManager.LoadScene("MainMenu", LoadSceneMode.Single); }
}
