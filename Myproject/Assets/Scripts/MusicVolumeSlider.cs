using UnityEngine;
using UnityEngine.UI;

public class MusicVolumeSlider : MonoBehaviour
{
    private Slider volumeSlider;
    private MusicManager musicManager;

    private void Start()
    {
        // Получаем компонент Slider, к которому привязан скрипт
        volumeSlider = GetComponent<Slider>();

        // Находим объект MusicManager в сцене
        musicManager = FindObjectOfType<MusicManager>();

        // Установка обработчика события изменения значения слайдера
        volumeSlider.onValueChanged.AddListener(OnVolumeChanged);

        // Установка начального значения слайдера на текущую громкость
        if (musicManager != null)
        {
            volumeSlider.value = musicManager.GetVolume();
        }
    }

    private void OnDestroy()
    {
        // Удаление обработчика события при уничтожении объекта
        if (volumeSlider != null)
        {
            volumeSlider.onValueChanged.RemoveListener(OnVolumeChanged);
        }
    }

    private void OnVolumeChanged(float volume)
    {
        // Установка громкости музыки при изменении значения слайдера
        if (musicManager != null)
        {
            musicManager.SetVolume(volume);
        }
    }
}
