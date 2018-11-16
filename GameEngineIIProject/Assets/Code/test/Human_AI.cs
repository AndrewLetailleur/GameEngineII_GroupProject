using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;//navmesh wise

public class Human_AI : MonoBehaviour {

    public GameObject FireTrig, Shot;
    private GameObject player;          private Transform target;//the player target, hack wise
    public float max_HP = 100;          public float health;

    public enum AI_State { IDLE, SEARCH, CHASE, STRAFE, SHOOT, SWIPE, LUNGE, RANDO };//needs to be public ATM
    public AI_State Cur_State;
    
    public enum AI_Type { HUMAN, ZOMBIE};//needs to be public ATM
    public AI_Type Cur_Type;
//  public bool isZombie;   // Not needed, as Cur_Type can do the function for me

    private float Max_Timer_S = 8f;     private float Max_Timer_A = 3.5f;
    /// should =           7 min        4 min        Max_Timer_S   Max_Timer_A
    public float Distance, Search_Dist, Attack_Dist, Min_Dist, Search_Timer, Attack_Timer;    //dist & timers!
    private Rigidbody RB;               private NavMeshAgent agent;/// nav mesh
    private float GunVelo = 1500;
    private float Shot_LifeSpan = 1;


    
    void Awake()
    {
        health = max_HP;

        player = GameObject.FindGameObjectWithTag("Player");
        target = player.transform;
        RB = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
    }
    void Start()// Use this for initialization
    {
        //Cur_Type = AI_Type.HUMAN;//needs to be public ATM
        Cur_State = AI_State.IDLE;

        Search_Timer = Max_Timer_S;
        Attack_Timer = Max_Timer_A;
    }
    void Update()/* Update is called once per frame */{
        Distance = Vector3.Distance(player.transform.position, transform.position);//first, the actual distance

        switch (Cur_Type)
        {
            case AI_Type.HUMAN:
                Soldier_ACT();
                break;
            case AI_Type.ZOMBIE:
                Zombie_ACT();
                break;
            default://if all else fails
                Cur_Type = AI_Type.HUMAN;//JNC Valv
                break;
        }//end switch case
        
    }

    //states of actions, depending on AI_Type
    public void Soldier_ACT() {
        switch (Cur_State)
        {
            case AI_State.IDLE:
                Idle_State();
                break;
            case AI_State.SEARCH:
                Search_State();
                break;
            case AI_State.CHASE:
                Chase_State();
                break;
            case AI_State.STRAFE:
                Strafe_State();
                break;
            case AI_State.SHOOT:
                Shoot_State();
                break;
            default://if all else fails
                Cur_State = AI_State.IDLE;//JNC Valv
                break;
        }//end switch case
    }
    public void Zombie_ACT() {
        switch (Cur_State)
        {
            case AI_State.IDLE:
                Idle_State();
                break;
            case AI_State.SEARCH:
                Search_State();
                break;
            case AI_State.CHASE:
                Chase_State();
                break;
            case AI_State.SWIPE:
                Swipe_State();
                break;
            default://if all else fails
                Cur_State = AI_State.IDLE;//JNC Valv
                break;
        }//end switch case

    }

    //the actual actions themselves
    public void Idle_State(){
        //rando path
        if (Distance < Search_Dist) { Chase_Trig(); }
    }
    public void Search_State(){
        if (Distance < Search_Dist) { Chase_Trig(); }
        else if (Search_Timer <= 0) { Cur_State = AI_State.IDLE; Search_Timer = 0; }
        else { Search_Timer -= Time.deltaTime; } //end if
    }
    public void Chase_Trig()/*GOTO CHASE*/{
        Cur_State = AI_State.CHASE;
        Search_Timer = Max_Timer_S;
    }

    public void Chase_State() {//idle
        agent.SetDestination(target.position);
        if (Distance < Min_Dist) {
            switch (Cur_Type) {
                case AI_Type.HUMAN:
                    Cur_State = AI_State.STRAFE;
                    break;
                default:/*ZOMBIE*/
                    Cur_State = AI_State.SWIPE;
                    break;
            }
        }
        if (Distance > Search_Dist) { Cur_State = AI_State.SEARCH; }
        switch (Cur_Type) {
            case AI_Type.HUMAN://if a soldier, do X as well
                AttackCheck();
                break;
        }
    }
    public void Strafe_State() //if at peak, boundary/space wise...
    {//idle
        if (Distance > (Min_Dist + 1)) { Cur_State = AI_State.CHASE; }
        RotateAround(target);//if around before towards makes for accurate shots
        RotateTowards(target);
        AttackCheck();//better in an "AttackCheck" Code instead?
        Shoot_State();//check if can attack
    }

    public void AttackCheck() {
        if (Distance > Attack_Dist) { Attack_Timer = Max_Timer_A; Cur_State = AI_State.CHASE; }
        Attack_Timer -= Time.deltaTime;
    }
    //slows/negates momentum, RB Addforce velocity wise

    
    //swipe should 'pace' the lunging. While STRAFE Should check and fire
    public void Swipe_State() {
        RotateTowards(target); 
        Velo_Descale(); 
        AttackCheck();
        if (Attack_Timer > 0) { /*Attack_Timer -= Time.deltaTime;*/ }
        else { Lunge(); Attack_Timer = Max_Timer_A; }
    }
    public void Velo_Descale()
    {
        if (RB.velocity.magnitude > 1) {//= 80 out of 100
                          RB.velocity = RB.velocity / 5 * 4;
            RB.angularVelocity = RB.angularVelocity / 5 * 4;
        }
        else {
            RB.velocity = Vector3.zero;
            RB.angularVelocity = Vector3.zero;
        }
    }
    public void Shoot_State()
    {//idle
        RotateTowards(target);
        ShootCheck();
        if (Attack_Timer > 0) { Attack_Timer -= Time.deltaTime; }
        else { FireBullet(); Attack_Timer = Max_Timer_A / 2; }
        //CHASE
    }

    //call when at min distance, for max attack

    public void ShootCheck()
    {
        RotateTowards(target);
        if (Distance <= Attack_Dist) {
            Attack_Timer -= Time.deltaTime;//consider turning to function, to fix timer variables?
            if (Attack_Timer <= 0) { FireBullet(); Attack_Timer = Max_Timer_A; }
        } else { Cur_State = AI_State.CHASE; Attack_Timer = Max_Timer_A; }
        //no need, as AttackCheck resets variables
    }
    //Human Attack Code
    public void FireBullet() {//fire a bullet, hacked edition
        GameObject Bullet = Instantiate(Shot, FireTrig.transform.position, FireTrig.transform.rotation) as GameObject;
        Rigidbody BulletRB = Bullet.GetComponent<Rigidbody>();
        BulletRB.AddForce(FireTrig.transform.forward * GunVelo);
        //BulletRB.AddForce(FireTrig.transform.up * (GunVelo / 2));
        Destroy(Bullet, Shot_LifeSpan);
        //	Debug.Log ("Open Fire!");

        switch (Cur_State)
        {
            case AI_State.STRAFE://strafe some more
                Cur_State = AI_State.SHOOT;//strafe some more
                break;
            case AI_State.SHOOT:
                Cur_State = AI_State.STRAFE;//strafe some more
                break;
            default://if all else fails
                Cur_State = AI_State.STRAFE;//strafe some more//JNC Valv
                break;
        }//end switch case

        //Cur_State = AI_State.STRAFE;//strafe some more
    }

    public void SwipeCheck()
    {
        if (Distance <= Min_Dist)
        {
            Attack_Timer -= Time.deltaTime;
            if (Attack_Timer <= 0) { Cur_State = AI_State.SWIPE; Attack_Timer = Max_Timer_A; Lunge(); }
        } else { Cur_State = AI_State.CHASE; Attack_Timer = Max_Timer_A; }
    }

    private void Lunge() {//lazy zombie attack
        //put in velocity check code, to bar excessive velocity ping pong
        RB.velocity = Vector3.zero;
        RB.angularVelocity = Vector3.zero;
        RB.AddForce(transform.forward * GunVelo);
    }
    //Condensed variables, function repeato wise
    private void RotateTowards(Transform target)
    {
        Vector3 dir = (target.position - transform.position).normalized;
        Quaternion faceRotation = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Slerp(transform.rotation, faceRotation, Time.deltaTime * 10);
    }
    private void RotateAround(Transform target)
    {
        Vector3 offset = target.position - transform.position;
        Vector3 dir = Vector3.Cross(offset, Vector3.up);
        agent.SetDestination(transform.position - dir);//fires just off, rotation facing wise. Consider flipping - for alt direction?
    }

    //lunge later

}

    //Lazy Inventory Management
    //bias here, is to get colliders.
        //Then grab an incremental player variable, to update/show on a GUI
    //CollisionEnter (trig or col)
    //if (collide.tag = "Medicine")
    //player.Medicine++; //so you get your loot
    //destroy.("Medicine");// so you enjoy witches burning houses down?



