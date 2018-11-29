using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LazyInventoryTest : MonoBehaviour {

    /// <summary> this hack code is for test purposes only.
    ///  And is NOT meant to overtake in any major capacity, Kirstin's work. 
    ///  logic likely being, for later 'reference' on the exit gate, hack wise.
    /// REF Below
    /// https://answers.unity.com/questions/1229370/destroy-object-with-dontdestroyonload.html
    /// ===
    /// That said, it has DontDestroyOnLoad, for hack testing of "has Key".
    /// </summary>

    private static LazyInventoryTest _instance = null;
    public static LazyInventoryTest Instance { get { return _instance; } }
    //first, the "is it in game" list of strings
    string Inside = "Inside";
    string Outside = "Outside";

    //cue the bools of rainbow keys
    bool endKey = false;
    

    // Use this for initialization
    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(transform.root.gameObject);
            return;
        }
        _instance = this;
        DontDestroyOnLoad(transform.root.gameObject);

        //call when scene changes
        SceneManager.activeSceneChanged += ChangedActiveScene;
    }

    void ChangedActiveScene(Scene current, Scene next)
    {

        if (current.name == "Inside" || current.name == "Outside") {}
        else if (next.name == "Inside" || next.name == "Outside") {}
        else
            Destroy(this.gameObject);
        //endif

    }  
        //the lazy triggers
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "LastKey") {
            Destroy(other.gameObject);
            endKey = true;
        }
    }




}
