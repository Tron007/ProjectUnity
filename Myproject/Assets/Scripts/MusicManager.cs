using UnityEngine;
using System.Collections;


public class MusicManager : MonoBehaviour
{
    // ����������� �������� ��� ������� � ������������� ���������� MusicManager
    public static MusicManager Instance { get; private set; }
    public AudioClip[] musicTracks;
    private AudioSource audioSource;
    private int currentTrackIndex = 0;
    // ���������� ��� �������� ������� ��������������� ��� �����
    private float pausedTime = 0f;

    private void Awake()
    {
        // ���������, ��� ���������� ������ ���� ��������� MusicManager
        if (Instance == null)
        {
            // ������� ������� ������ ����������� MusicManager
            Instance = this;
            // ���������, ��� �� �� ����� ��������� ��� �������� ����� �����
            DontDestroyOnLoad(gameObject);
            // �������� ��������� AudioSource
            audioSource = GetComponent<AudioSource>();
        }
        else
        {
            // ���� ��������� ��� ����������, ���������� ������� ������
            Destroy(gameObject);
        }
    }

    // ����� ��� ��������������� ������
    public void PlayMusic(AudioClip musicClip)
    {
        // ���������� ��������� ��� ���������������
        audioSource.clip = musicClip;
        // ���� ���� ���������� ����� ���������������, ������������� ���
        if (pausedTime > 0f)
        {
            audioSource.time = pausedTime;
            pausedTime = 0f; // ���������� ���������� ����� ����� �������������
        }
        // ������ ���������������
        audioSource.Play();
    }

    // ����� ��� ������������ ��������������� ������
    public void PauseMusic()
    {
        // ��������� ������� ����� ���������������
        pausedTime = audioSource.time;
        audioSource.Pause();
        // ������������� �������������� ������� �� ��������� ����
        StopCoroutine("PlayNextTrackRoutine");
    }

    // ����� ��� ������������� ��������������� ������
    public void ResumeMusic()
    {
        // ������������ ��������������� ������
        audioSource.Play();
        // ��������� �������������� ������� �� ��������� ����
        StartCoroutine("PlayNextTrackRoutine");
    }

    // ����� ��� ��������������� �������� �� ��������� ����
    private IEnumerator PlayNextTrackRoutine()
    {
        // ����, ���� ������� ����� �� ����������
        yield return new WaitWhile(() => audioSource.isPlaying);

        // ������������� ��������� ����
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
            // ���� ��������� ����� ������ �����, ��������� � ������
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