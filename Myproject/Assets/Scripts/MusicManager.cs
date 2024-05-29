using UnityEngine;
using System.Collections;


public class MusicManager : MonoBehaviour
{
    // Статическое свойство для доступа к единственному экземпляру MusicManager
    public static MusicManager Instance { get; private set; }
    public AudioClip[] musicTracks;
    private AudioSource audioSource;
    private int currentTrackIndex = 0;
    // Переменная для хранения времени воспроизведения при паузе
    private float pausedTime = 0f;

    private void Awake()
    {
        // Убедиться, что существует только один экземпляр MusicManager
        if (Instance == null)
        {
            // Сделать текущий объект экземпляром MusicManager
            Instance = this;
            // Убедиться, что он не будет уничтожен при загрузке новой сцены
            DontDestroyOnLoad(gameObject);
            // Получить компонент AudioSource
            audioSource = GetComponent<AudioSource>();
        }
        else
        {
            // Если экземпляр уже существует, уничтожить текущий объект
            Destroy(gameObject);
        }
    }

    // Метод для воспроизведения музыки
    public void PlayMusic(AudioClip musicClip)
    {
        // Установить аудиофайл для воспроизведения
        audioSource.clip = musicClip;
        // Если есть сохранённое время воспроизведения, устанавливаем его
        if (pausedTime > 0f)
        {
            audioSource.time = pausedTime;
            pausedTime = 0f; // Сбрасываем сохранённое время после использования
        }
        // Начать воспроизведение
        audioSource.Play();
    }

    // Метод для приостановки воспроизведения музыки
    public void PauseMusic()
    {
        // Сохраняем текущее время воспроизведения
        pausedTime = audioSource.time;
        audioSource.Pause();
        // Останавливаем автоматический переход на следующий трек
        StopCoroutine("PlayNextTrackRoutine");
    }

    // Метод для возобновления воспроизведения музыки
    public void ResumeMusic()
    {
        // Возобновляем воспроизведение музыки
        audioSource.Play();
        // Запускаем автоматический переход на следующий трек
        StartCoroutine("PlayNextTrackRoutine");
    }

    // Метод для автоматического перехода на следующий трек
    private IEnumerator PlayNextTrackRoutine()
    {
        // Ждем, пока текущая песня не закончится
        yield return new WaitWhile(() => audioSource.isPlaying);

        // Воспроизводим следующий трек
        PlayNextTrack();
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (musicTracks.Length > 0)
        {
            PlayNextTrack();
        }
        else
        {
            Debug.LogWarning("No music tracks assigned to MusicManager.");
        }
    }

    private void PlayNextTrack()
    {
        if (currentTrackIndex >= musicTracks.Length)
        {
            // Если достигнут конец списка песен, перейдите к началу
            currentTrackIndex = 0;
        }

        audioSource.clip = musicTracks[currentTrackIndex];
        audioSource.Play();

        currentTrackIndex++;
    }

    public float GetVolume()
    {
        return audioSource.volume;
    }

    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
    }
}