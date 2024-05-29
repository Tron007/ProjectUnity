using UnityEngine;
using UnityEngine.UI;

public class SoundVolumeSlider : MonoBehaviour
{
    private Slider volumeSlider;
    private SoundManager soundManager;

    private void Start()
    {
        // �������� ��������� Slider, � �������� �������� ������
        volumeSlider = GetComponent<Slider>();

        // ������� ������ SoundManager � �����
        soundManager = FindObjectOfType<SoundManager>();

        // ��������� ����������� ������� ��������� �������� ��������
        volumeSlider.onValueChanged.AddListener(OnVolumeChanged);

        // ��������� ���������� �������� �������� �� ������� ��������� ������
        if (soundManager != null)
        {
            volumeSlider.value = soundManager.GetVolume();
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
        if (soundManager != null)
        {
            soundManager.SetVolume(volume);
        }
    }
}
