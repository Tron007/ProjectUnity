using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenu;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;

        MusicManager.Instance.ResumeMusic();
        SoundManager.Instance.ResumeBackgroundMusic();
    }

    void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;

        MusicManager.Instance.PauseMusic();
        SoundManager.Instance.PauseBackgroundMusic();
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1f;

        MusicManager.Instance.ResumeMusic();
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(2);
        Time.timeScale = 1f;

        MusicManager.Instance.ResumeMusic();
        SoundManager.Instance.ResumeBackgroundMusic();
    }

}
