using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;
    private bool isPaused = false;
    public bool IsPaused { get { return isPaused;}} 
    private float loadingProgress;
    public float LoadingProgress { get { return loadingProgress; } }

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
        else {
            Destroy(gameObject);
        }

        loadingProgress = 0.0f;

        //Ensures object remains present between scenes.
        DontDestroyOnLoad(this.gameObject);
    }

    void Start() {

    }

    void Update() {

    }


    #region Pause Handling
    public void PauseUnpase() {
        if (isPaused) {
            Unpause();
        }
        else {
            Pause();
        }
        isPaused = !isPaused;
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

    #region Scene Transition Handling
    public void LoadScene(int sceneID) {
        //loads a small loading screen scene
        SceneManager.LoadSceneAsync("LoadingScreen");
        StartCoroutine(LoadSceneAsync(sceneID));
    }

    public void LoadScene(string sceneName) {
        Scene toLoad = SceneManager.GetSceneByName(sceneName);
        LoadScene(toLoad.buildIndex);
    }

    private IEnumerator LoadSceneAsync(int sceneID) {

        //starts loading the desired scene in the background, setting it not to display.
        var toLoad = SceneManager.LoadSceneAsync(sceneID);
        toLoad.allowSceneActivation = false;

        //Updates load progress value for loading bars etc.
        while (toLoad.isDone != true) {
            loadingProgress = Mathf.Clamp01(toLoad.progress / 0.9f / 100f);

            //NOTE: progress only reaches 0.9f and no higher as allowSceneActivation is set to false.
            if (toLoad.progress >= 0.9f) {
                toLoad.allowSceneActivation = true;
            }
        }
        yield return null;
    }

    #endregion
}
