using UnityEngine;
using UnityEngine.SceneManagement;  // Nécessaire pour la gestion des scènes
using UnityEngine.Video;           // Nécessaire pour accéder au VideoPlayer

public class VideoSceneTransition : MonoBehaviour
{
    public VideoPlayer videoPlayer;

    // Start is called before the first frame update
    void Start()
    {
        if (videoPlayer == null)
        {
            videoPlayer = GetComponent<VideoPlayer>();
        }

        videoPlayer.Play();
        videoPlayer.loopPointReached += OnVideoFinished;
    }


    private void OnVideoFinished(VideoPlayer vp)
    {
        Debug.Log("Vidéo terminée, chargement de la scène suivante.");

        // Charge la scène suivante
        SceneManager.LoadScene("LaunchScene");
    }
}