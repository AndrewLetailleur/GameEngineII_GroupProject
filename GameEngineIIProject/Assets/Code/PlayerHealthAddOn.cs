using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; //for the game over trig
public class PlayerHealthAddOn : MonoBehaviour {

    public int protoHP = 3;
    public float endTime = 4f;

    public float GUI_Alpha = 0f;
    public Image GUI_HP; Color GUI_COL;


    bool got = false;
	// Use this for initialization
	void Start () {
        GUI_COL = GUI_HP.color;
        GUI_COL.a = GUI_Alpha;
        GUI_HP.color = GUI_COL;
	}
	
	// Update is called once per frame
	void Update () {
        if (protoHP <= 0) //if HP below zero, trigger the death trigger
        { gameOver(); }
        else if (GUI_Alpha > 0)
        { GUI_Alpha -= Time.deltaTime; }//cut down on alpha if false
        GUI_COL.a = GUI_Alpha;
        GUI_HP.color = GUI_COL;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Hazard") { Damage(); }//inflict damage hack
        
    }

    void Damage()
    {
        if (protoHP > 0) { 
            protoHP--;
            if (protoHP > 0)
                GUI_Alpha = 1;
            //end if
        }//end double checker
    }


    void gameOver() {
        GUI_Alpha += (Time.deltaTime / 2);

        if (endTime > 0)
            endTime -= Time.deltaTime;
        else
           SceneManager.LoadScene("EndGame", LoadSceneMode.Single);
        //end if

    }



}
