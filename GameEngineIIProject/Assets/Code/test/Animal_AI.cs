using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal_AI : MonoBehaviour {

    //due to hassle, this code can be considered depricated for now.

    private GameObject player;
    public enum AI_State {IDLE, CHASE, ATTACK, RANDO};//needs to be public ATM
    public AI_State Cur_State;

    public float Max_Timer = 3.5f;
    public float Distance, Search_Dist, Attack_Dist, Search_Timer, Attack_Timer;    //dist & timers!

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        Cur_State = AI_State.IDLE;
	}

	// Update is called once per frame
	void Update () {

            //first, the actual distance
        Distance = Vector3.Distance(player.transform.position, transform.position);

        //get lowest value first, then "in between" values check wiseSearch_Timer = Max_Timer;
        if (Distance <= Attack_Dist) {
            Attack_Timer -= Time.deltaTime;
            if (Attack_Timer <= 0) { Cur_State = AI_State.ATTACK; Attack_Timer = 0; }
        } else { Attack_Timer = Max_Timer; }

        if ( (Distance > Attack_Dist) && (Distance <= Search_Dist) ) {
            Cur_State = AI_State.CHASE;
            Search_Timer = Max_Timer;
        } else if (Cur_State != AI_State.IDLE) {
            Search_Timer -= Time.deltaTime;
            if (Search_Timer <= 0) { Cur_State = AI_State.IDLE; Search_Timer = 0; }
            //end if
        }

        //do an ACTION, depending on current state
        switch (Cur_State)
        {
        case AI_State.IDLE:
            //Wander();
            break;
        case AI_State.CHASE:
            //Chase();
            break;
        case AI_State.ATTACK:
            //Lunge();
            break;
        default://if all else fails
            Cur_State = AI_State.IDLE;//JNC Valv
            break;
        }//end switch case
	}
}
