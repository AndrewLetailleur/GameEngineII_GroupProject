using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //there JNC of scene loads - AIL

public class GameHandler : MonoBehaviour {

    public static GameHandler instance = null;
    private bool isPaused = false;
    public bool IsPaused { get { return isPaused;}}

    private SceneHandler sh;
    private AudioHandler ah;

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
        else {
            Destroy(gameObject);
        }

        ah = FindObjectOfType<AudioHandler>();
        sh = FindObjectOfType<SceneHandler>();

        //Ensures object remains present between scenes.
        DontDestroyOnLoad(this.gameObject);
    }

    void Start() {
        sh.LoadScene("Inside");
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.P))
        {    //check pause condition hack < AIL
 //           Debug.Log("I can read this key");
            PauseUnpase();//pause the game
        }//endif
    }

    #region Pause Handling
    public void PauseUnpase() {
        if (isPaused) {
            Unpause();
        }
        else {
            Pause();
        }
///       isPaused = !isPaused;     //this part was buggy, so fixed. - AIL
    //   if you want that code still, then I'd remove the 'set isPaused' on Pause() and Unpause() functions
        //  reason, in a sequence logic. Setting and then flipping by a !val, means it's turnaround back to that set value instead. 
    }
    private void Pause() {
        isPaused = true;
        Time.timeScale = 0.0f;
    }
    private void Unpause() {
        isPaused = false;
        Time.timeScale = 1.0f;
    }
    #endregion

}
