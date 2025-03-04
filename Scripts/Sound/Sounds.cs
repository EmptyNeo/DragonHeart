using System.Collections;
using UnityEngine;

public class Sounds : MonoBehaviour
{
    private static AudioSource[] _audio;

    private void Start()
    {
        _audio = GetComponents<AudioSource>();
    }

    public static void Play(AudioClip clip, float volume = 1, float p = 1f)
    {
        foreach (AudioSource sound in _audio)
        {
            if (sound.clip ==null)
            {
                sound.pitch = p;
                sound.PlayOneShot(clip, volume);
                sound.clip = null;
                return;
            }
        }
    }
}