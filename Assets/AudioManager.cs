using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource[] Songs;
    private AudioSource[] SoundEffects;

    /*private static AudioManager _instance;
    

    public static AudioManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<AudioManager>();

                // Do not destroy this object when loading a new scene!
                DontDestroyOnLoad(_instance.gameObject);
            }

            return _instance;
        }
    }*/

    /*public void Awake()
    {
        if (_instance == null)
        {
            //If I am the first instance, make me the Singleton
            _instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            // If a Singleton already exists and you find
            // another reference in scene, destroy it!
            if (this != _instance)
                Destroy(gameObject);
        }
    }*/

    public void Start()
    {
        //Awake();
        PlayMainTrack();
    }

    

    public void PlayMainTrack()
    {
        if (Songs == null) Songs = GetComponents<AudioSource>()[0..3];
        Songs[0].Play(); // main track
    }

    public void PlayActionTrack()
    {
        if (Songs == null) Songs = GetComponents<AudioSource>()[0..3];
        Songs[1].Play(); // action track
    }

    public void PlayAlternativeTrack()
    {
        if (Songs == null) Songs = GetComponents<AudioSource>()[0..3];
        Songs[2].Play(); // alternative track
    }

    public void PauseMusic()
    {
        foreach (AudioSource ae in Songs) ae.Pause();
    }

    public void UnpauseMusic()
    {
        foreach (AudioSource ae in Songs) ae.UnPause();
    }

    public void ItemPickup()
    {
        if (SoundEffects == null) SoundEffects = GetComponents<AudioSource>()[3..];
        SoundEffects[0].Play(); // gem sfx
    }

    public void GameOver()
    {
        if (SoundEffects == null) SoundEffects = GetComponents<AudioSource>()[3..];
        SoundEffects[1].Play(); // game over sfx
    }

    public void ButtonClicked()
    {
        if (SoundEffects == null) SoundEffects = GetComponents<AudioSource>()[3..];
        SoundEffects[2].Play(); // button sfx
    }

    public void Explosion()
    {
        if (SoundEffects == null) SoundEffects = GetComponents<AudioSource>()[3..];
        SoundEffects[3].Play(); // explosion sfx
    }
}