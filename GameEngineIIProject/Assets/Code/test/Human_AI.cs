using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human_AI : MonoBehaviour {

    private GameObject player;
    public enum AI_State { IDLE, SEARCH, CHASE, STRAFE, ATTACK, RANDO };//needs to be public ATM
    public AI_State Cur_State;

    public float Max_Timer = 3.5f;
    public float Distance, Search_Dist, Attack_Dist, Search_Timer, Attack_Timer;    //dist & timers!

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Cur_State = AI_State.IDLE;

        Search_Timer = Max_Timer;
        Attack_Timer = Max_Timer;
    }

    // Update is called once per frame
    void Update()
    {

        //first, the actual distance
        Distance = Vector3.Distance(player.transform.position, transform.position);

        //attack first,
        //else search next
        //else idlecheck if no idle

        //do an ACTION, depending on current state
        switch (Cur_State)
        {
            case AI_State.IDLE:
                Wander();
                break;
            case AI_State.SEARCH:
                Search();
                break;
            case AI_State.CHASE:
                Chase();
                break;
            case AI_State.ATTACK:
                Shoot();
                break;
            default://if all else fails
                Cur_State = AI_State.IDLE;//JNC Valv
                break;
        }//end switch case
    }

    public void Wander() {//idle
        if (Distance < Search_Dist) { Cur_State = AI_State.CHASE; Search_Timer = Max_Timer; }
    }
    public void Search() {//idle
        if (Distance < Search_Dist) { Cur_State = AI_State.CHASE; Search_Timer = Max_Timer; }
        else if (Search_Timer <= 0) { Cur_State = AI_State.IDLE; Search_Timer = 0; }
        else { Search_Timer -= Time.deltaTime; } //end if
    }
    public void Chase() {//idle
        if (Distance > Search_Dist) { Cur_State = AI_State.SEARCH; }
        else if (Distance <= Attack_Dist) {
            Attack_Timer -= Time.deltaTime;
            if (Attack_Timer <= 0) { Cur_State = AI_State.ATTACK; Attack_Timer = 0; }
        } else { Attack_Timer = Max_Timer; }
    }
    public void Strafe() //if at peak, boundary/space wise...
    {//idle

    }
    public void Shoot()
    {//idle
        if (Distance > Attack_Dist) { Attack_Timer = Max_Timer; Cur_State = AI_State.CHASE; }
    }
}
