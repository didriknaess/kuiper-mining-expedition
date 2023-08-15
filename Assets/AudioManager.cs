using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance;
    private AudioSource[] Audio;

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
    }

    public void Start()
    {
        Awake();
        PlayMainTrack();
    }

    public void Awake()
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
    }

    public void PlayMainTrack()
    {
        if (Audio == null) Audio = GetComponents<AudioSource>();
        Audio[0].Play(); // main track
    }

    public void PlayActionTrack()
    {
        if (Audio == null) Audio = GetComponents<AudioSource>();
        Audio[1].Play(); // action track
    }

    public void PlayAlternativeTrack()
    {
        if (Audio == null) Audio = GetComponents<AudioSource>();
        Audio[2].Play(); // alternative track
    }

    public void PauseMusic()
    {
        foreach (AudioSource ae in Audio) ae.Pause();
    }

    public void UnpauseMusic()
    {
        foreach (AudioSource ae in Audio) ae.UnPause();
    }

    public void ItemPickup()
    {
        if (Audio == null) Audio = GetComponents<AudioSource>();
        Audio[3].Play(); // gem sfx
    }

    public void GameOver()
    {
        if (Audio == null) Audio = GetComponents<AudioSource>();
        Audio[4].Play(); // game over sfx
    }

    public void ButtonClicked()
    {
        if (Audio == null) Audio = GetComponents<AudioSource>();
        Audio[5].Play(); // button sfx
    }
}
