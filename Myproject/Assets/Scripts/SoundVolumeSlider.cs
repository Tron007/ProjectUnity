using UnityEngine;
using UnityEngine.UI;

public class SoundVolumeSlider : MonoBehaviour
{
    private Slider volumeSlider;
    private SoundManager soundManager;

    private void Start()
    {
        // Получаем компонент Slider, к которому привязан скрипт
        volumeSlider = GetComponent<Slider>();

        // Находим объект SoundManager в сцене
        soundManager = FindObjectOfType<SoundManager>();

        // Установка обработчика события изменения значения слайдера
        volumeSlider.onValueChanged.AddListener(OnVolumeChanged);

        // Установка начального значения слайдера на текущую громкость звуков
        if (soundManager != null)
        {
            volumeSlider.value = soundManager.GetVolume();
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
        // Установка громкости звуков при изменении значения слайдера
        if (soundManager != null)
        {
            soundManager.SetVolume(volume);
        }
    }
}
