using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    // Références au panneau de pause
    public GameObject pauseMenu;
    public Camera mainCamera;
    public Camera pauseCamera;
    public GameObject[] objectsToHide;

    
    private bool isPaused = false;

    public void OnButtonClick(Button button)
    {
        if (button.CompareTag("ReplayButton"))
        {
            SceneManager.LoadScene("GamePlay");
        }
        else if (button.CompareTag("PlayButton"))
        {
            SceneManager.LoadScene("Start");
        }
        else if (button.CompareTag("StartButton"))
        {
            SceneManager.LoadScene("GamePlay");
        }
        else if (button.CompareTag("MenuButton"))
        {
            SceneManager.LoadScene("MainMenu");
        }
        else if (button.CompareTag("QuitButton"))
        {
            SceneManager.LoadScene("MainMenu");
        }
        else if (button.CompareTag("SkipButton"))
        {
            SceneManager.LoadScene("LaunchScene");
        }
        else if (button.CompareTag("PauseButton"))
        {
            PauseGame();
        }
        else if (button.CompareTag("ResumeButton"))
        {
            ResumeGame();
        }
    }

    // Mise en pause du jeu
     private void PauseGame()
    {
        if (!isPaused)
        {
            Time.timeScale = 0f;
            isPaused = true;

            pauseMenu.SetActive(true);

            mainCamera.gameObject.SetActive(false);
            pauseCamera.gameObject.SetActive(true);

            foreach (var obj in objectsToHide)
            {
                obj.SetActive(false);
            }
        }
    }

    // Reprend le jeu
    private void ResumeGame()
    {
        if (isPaused)
        {
            Time.timeScale = 1f;
            isPaused = false;
            pauseMenu.SetActive(false);
            mainCamera.gameObject.SetActive(true);
            pauseCamera.gameObject.SetActive(false);

            foreach (var obj in objectsToHide)
            {
                obj.SetActive(true);
            }
        }
    }
}