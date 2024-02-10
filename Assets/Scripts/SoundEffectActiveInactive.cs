
using UnityEngine;
using UnityEngine.Events;

public class SoundEffectActiveInactive : MonoBehaviour
{

    public bool active;

    
    private void Start()
    {
        
    }
    // Start is called before the first frame update

    private void OnEnable()
    {
        BGMusic.sfxChanged += EnableSound;

        active = BGMusic.Instance.sfx;

        EnableSound();
    }
    private void OnDisable()
    {
        BGMusic.sfxChanged -= EnableSound;
    }


    [ContextMenu("changeSfx")]
    void EnableSound()
    {
        if(BGMusic.Instance.sfx)
        {
            GetComponent<AudioSource>().volume = 1;  
            active = true;
        }
        else
        {
            GetComponent<AudioSource>().volume = 0;
            active = false;
        }
    }
}
