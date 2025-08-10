using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : Singleton<AudioManager>
{
    [Header("Sounds")]
    [SerializeField] private Sound brickBreakClip;
    [SerializeField] private Sound ballLostClip;
    [SerializeField] private Sound backgroundMusicClip;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    [System.Serializable]
    private class Sound
    {
        [SerializeField] private AudioClip clip;
        [SerializeField] private float volume;

        public AudioClip Clip => clip;
        public float Volume => volume;
    }

    protected override void Awake()
    {
        base.Awake();

        if (Instance == this)
        {
            UnitySceneManager.OnGameSceneLoad += OnGameSceneLoad;
            GameManager.OnGameOver += OnGameOver;
            BottomBorder.OnBallLost += OnBallLost;
            Brick.OnBrickDestroyed += OnBrickDestroyed;
        }
    }

    private void OnGameSceneLoad()
    {
        PlayBackgroundMusic();
    }

    private void OnGameOver()
    {
        StopBackgroundMusic();
    }

    private void OnBallLost()
    {
        PlayBallLost();
    }

    private void OnBrickDestroyed()
    {
        PlayBrickBreak();
    }

    private void PlayBackgroundMusic()
    {
        musicSource.clip = backgroundMusicClip.Clip;
        musicSource.loop = true;
        musicSource.volume = backgroundMusicClip.Volume;
        musicSource.Play();
    }

    private void StopBackgroundMusic()
    {
        musicSource.Stop();
    }

    private void PlayBrickBreak()
    {
        sfxSource.volume = brickBreakClip.Volume;
        sfxSource.PlayOneShot(brickBreakClip.Clip);
    }

    private void PlayBallLost()
    {
        sfxSource.volume = ballLostClip.Volume;
        sfxSource.PlayOneShot(ballLostClip.Clip);
    }
}

 