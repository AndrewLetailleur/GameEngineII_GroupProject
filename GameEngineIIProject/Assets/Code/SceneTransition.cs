using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;//for scenes

public class SceneTransition : MonoBehaviour {

    public string scene_to_load;
    public string end_game_trig;

    public bool endTrig = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && endTrig) {
            SceneManager.LoadScene(end_game_trig, LoadSceneMode.Single);
        }
        else if (other.tag == "Player") {
            SceneManager.LoadScene(scene_to_load, LoadSceneMode.Single);
        }
    }
}
