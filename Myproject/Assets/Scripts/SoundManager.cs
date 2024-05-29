using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }
    public AudioClip backgroundMusic;
    public AudioClip goalSound;
    private AudioSource backgroundMusicSource;
    private AudioSource goalSoundSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // Создаем аудиоисточники для фоновой музыки и звука гола
            backgroundMusicSource = gameObject.AddComponent<AudioSource>();
            backgroundMusicSource.clip = backgroundMusic;
            backgroundMusicSource.loop = true;

            goalSoundSource = gameObject.AddComponent<AudioSource>();
            goalSoundSource.clip = goalSound;

            // Подписываемся на событие смены сцены
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == 2)
        {
            backgroundMusicSource.Play();
        }
        else
        {
            backgroundMusicSource.Stop();
        }
    }

    // Метод для проигрывания звука
    public void PlaySound(AudioClip clip)
    {
        goalSoundSource.PlayOneShot(clip);
    }

    // Методы для управления громкостью фоновой музыки
    public void PauseBackgroundMusic()
    {
        backgroundMusicSource.Pause();
    }

    public void ResumeBackgroundMusic()
    {
        backgroundMusicSource.UnPause();
    }

    public float GetVolume()
    {
        return backgroundMusicSource.volume;
    }

    public void SetVolume(float volume)
    {
        backgroundMusicSource.volume = volume;
        goalSoundSource.volume = volume;
    }
}
