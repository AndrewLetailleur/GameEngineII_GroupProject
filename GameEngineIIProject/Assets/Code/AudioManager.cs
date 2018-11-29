using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public static AudioManager instance = null;
/* this code is buggy, so REDACTED for now. - AIL
    private 

    private float masterVolume;
    [SerializedField]
    [Range(0, 1)]
    public float MasterVolume { get { return masterVolume; } set { masterVolume = Mathf.Clamp01(value); } }
    private float uiVolume;
    [SerializedField]
    [Range(0, 1)]
    public float UIVolume { get { return uiVolume; } set { uiVolume = Mathf.Clamp01(value); } }
    private float sfxVolume;
    [SerializedField]
    [Range(0, 1)]
    public float SfxVolume { get { return sfxVolume; } set { sfxVolume = Mathf.Clamp01(value); } }
    private float musicVolume;
    [SerializedField]
    [Range(0, 1)]
    public float MusicVolume { get { return musicVolume; } set { musicVolume = Mathf.Clamp01(value); } }
*/
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        //Ensures object remains present between scenes.
        DontDestroyOnLoad(this.gameObject);
    }

    void PlayUISound()
    {
        //play ui sound at camera location.
    }

    void PlaySFX(string effect)
    {
        
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
