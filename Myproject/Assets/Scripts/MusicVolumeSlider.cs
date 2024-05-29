using UnityEngine;
using UnityEngine.UI;

public class MusicVolumeSlider : MonoBehaviour
{
    private Slider volumeSlider;
    private MusicManager musicManager;

    private void Start()
    {
        // �������� ��������� Slider, � �������� �������� ������
        volumeSlider = GetComponent<Slider>();

        // ������� ������ MusicManager � �����
        musicManager = FindObjectOfType<MusicManager>();

        // ��������� ����������� ������� ��������� �������� ��������
        volumeSlider.onValueChanged.AddListener(OnVolumeChanged);

        // ��������� ���������� �������� �������� �� ������� ���������
        if (musicManager != null)
        {
            volumeSlider.value = musicManager.GetVolume();
        }
    }

    private void OnDestroy()
    {
        // �������� ����������� ������� ��� ����������� �������
        if (volumeSlider != null)
        {
            volumeSlider.onValueChanged.RemoveListener(OnVolumeChanged);
        }
    }

    private void OnVolumeChanged(float volume)
    {
        // ��������� ��������� ������ ��� ��������� �������� ��������
        if (musicManager != null)
        {
            musicManager.SetVolume(volume);
        }
    }
}
