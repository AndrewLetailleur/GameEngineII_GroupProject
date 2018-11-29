using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasementDoor : MonoBehaviour {

    public GameObject doorA, doorB;//the doors

    public bool opened = true;

    public Transform fromDoorA, fromDoorB, toDoorA, toDoorB;

    public float toOpen = 1850f;

    public void Start()
    {
        fromDoorA = doorA.transform;
        toDoorA = doorA.transform;
        toDoorA.transform.localEulerAngles = new Vector3(toOpen, fromDoorA.transform.rotation.y, fromDoorA.transform.rotation.z);
        
        fromDoorB = doorB.transform;
        toDoorB = doorB.transform;
        toDoorB.transform.localEulerAngles = new Vector3(toOpen, fromDoorB.transform.rotation.y, fromDoorB.transform.rotation.z);
    }



    private void OnTriggerStay(Collider other)
    {
        float timeCount = 5f;

        if (other.tag == "Player" && !opened) //and say, has key, and if it's opened hack wise
        {
            doorA.transform.rotation = Quaternion.Slerp(fromDoorA.rotation, toDoorA.rotation, timeCount);
            doorB.transform.rotation = Quaternion.Slerp(fromDoorB.rotation, toDoorB.rotation, timeCount);

            opened = !opened;
        } else if (opened) //and say, has key, and if it's opened hack wise
        {
            doorA.transform.rotation = Quaternion.Slerp(toDoorA.rotation, fromDoorA.rotation, timeCount);
            doorB.transform.rotation = Quaternion.Slerp(toDoorB.rotation, fromDoorB.rotation, timeCount);

            opened = !opened;
        }
    }


}
