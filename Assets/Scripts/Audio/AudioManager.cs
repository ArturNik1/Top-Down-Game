using UnityEngine.Audio;
using UnityEngine;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    private Dictionary<string, int> dict = new Dictionary<string, int>();

    public static AudioManager instance;

    // Start is called before the first frame update
    void Awake()
    {

        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        int i = 0;
        foreach (Sound s in sounds) {

            dict[s.name] = i;
            i++;

            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }   
    }

    private void Start()
    {
        Play("background");
    }

    public Sound GetSound(string name) {
        int index = dict[name];
        if (sounds[index] == null) {
            Debug.Log("Sound " + name + " not found!");
            return null;
        }
        return sounds[index];
    }

    public void Play(string name)
    {
        int index = dict[name];
        if (sounds[index] == null) {
            Debug.Log("Sound " + name + " not found!");
            return;
        }
        sounds[index].source.Play();
    }

}
