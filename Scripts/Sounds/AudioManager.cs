using System.Collections;
using UnityEngine;


public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    private AudioSource music1;
    private AudioSource music2;
    private AudioSource sfxSource;

    [SerializeField] private float musicVolume = 1;
    private bool firstMusicSourceActive;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this.gameObject);

        music1 = gameObject.AddComponent<AudioSource>();
        music2 = gameObject.AddComponent<AudioSource>();
        sfxSource = gameObject.AddComponent<AudioSource>();

        music1.loop = true;
        music2.loop = true;

    }

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

    public void PlaySFX(AudioClip clip, float volume)
    {
        sfxSource.PlayOneShot(clip, volume);
    }

    public void PlayMusicWithXFade(AudioClip musicClip, float transitionTime = 1f)
    {
        // Determine which source is active
        AudioSource activeSource = firstMusicSourceActive ? music1 : music2;
        AudioSource newSource = firstMusicSourceActive ? music2 : music1;

        // Change bool accordingly
        firstMusicSourceActive = !firstMusicSourceActive;

        StartCoroutine(UpdateMusicWithXFade(activeSource, newSource, musicClip, transitionTime));
    }

    private IEnumerator UpdateMusicWithXFade(AudioSource activeSource, AudioSource newSource, AudioClip music, float transitionTime)
    {
        // Make sure the source is active and playing
        if (!activeSource.isPlaying)
        {
            activeSource.Play();
        }

        newSource.Stop();
        newSource.clip = music;
        newSource.Play();

        float t = 0f;

        for (t = 0f; t < transitionTime; t += Time.deltaTime)
        {
            // decreasing activeSource volume over transitionTime down to 0
            activeSource.volume = musicVolume - (t / transitionTime * musicVolume);

            // increase newSource volume over transition time up to a musicVolume value
            newSource.volume = t / transitionTime * musicVolume;

            // return null, as our increase/decrease volume logic is above
            yield return null;
        }

        // Make sure volumes are set correctly after loop
        newSource.volume = musicVolume;
        activeSource.volume = 0f;

        // stop previously active audioSource
        activeSource.Stop();
    }
}
