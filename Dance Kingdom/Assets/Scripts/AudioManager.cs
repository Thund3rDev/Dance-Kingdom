using System;
using UnityEngine;

//Class AudioManager, that controls all audio in the game.
public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public Sound[] music;
    public Sound[] sounds;

    //On Awake, instance itselft and if there is another, destroy it.
    //Then, set to don't destroy on load.
    //Also gets all the data from the sounds.
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(this);

        foreach (Sound s in music)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    //If we need to do something on a sound, we call this function.
    public void ManageAudio(string name, string type, string action)
    {
        if (type == "music")
        {
            Sound s = Array.Find(music, sound => sound.name == name);
            if (s == null)
            {
                Debug.LogWarning("Sound: " + name + " not found!");
                return;
            }
            if (action == "play")
                s.source.Play();
            else if (action == "stop")
                s.source.Stop();
            else if (action == "pause")
                s.source.Pause();
            else if (action == "unpause")
                s.source.UnPause();
        }
        else if (type == "sound")
        {
            Sound s = Array.Find(sounds, sound => sound.name == name);
            if (s == null)
            {
                Debug.LogWarning("Sound: " + name + " not found!");
                return;
            }
            if (action == "play")
                s.source.Play();
            else if (action == "stop")
                s.source.Stop();
            else if (action == "pause")
                s.source.Pause();
            else if (action == "unpause")
                s.source.UnPause();
        }
    }
}
