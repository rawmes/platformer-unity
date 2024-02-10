
using UnityEngine;
using UnityEngine.Events;

[RequireComponent (typeof(AudioSource))]
public class BGMusic : MonoBehaviour
{
    public static UnityAction sfxChanged;

    private static BGMusic instance = null;
    public static BGMusic Instance { get { return instance; } }

    public bool music;
    public bool sfx;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(instance.gameObject);
            return;
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        if (!music)
        {
            Stop();
        }
    }

    public void Play()
    {
        GetComponent<AudioSource>().Play();
    }

    public void Stop()
    {
        GetComponent<AudioSource>().Stop();
    }


    public void ChangeSfx(bool a)
    {
        sfx = !sfx;
        sfxChanged?.Invoke();
        
    }

}
