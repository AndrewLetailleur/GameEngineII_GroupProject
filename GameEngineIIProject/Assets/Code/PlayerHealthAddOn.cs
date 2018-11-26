using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; //for the game over trig
public class PlayerHealthAddOn : MonoBehaviour {
    
    private int protoHP = 3;

    private float endTime = 4f;

    public Image GUI_HP;
	// Use this for initialization
	void Start () {
        GUI_HP.color.a = 0;
	}
	
	// Update is called once per frame
	void Update () {

        if (protoHP <= 0)
        {
            gameOver();
        }
        else if { }


	}

    void gameOver() {

        



        if (endTime > 0)
            endTime -= Time.deltaTime;
        else
           SceneManager.LoadScene("EndGame", LoadSceneMode.Single);
        //end if

    }



}
