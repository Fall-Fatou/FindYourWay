using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
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
    }
}