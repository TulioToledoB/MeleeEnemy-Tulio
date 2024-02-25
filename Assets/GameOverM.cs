using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic; 

public class GameOverM : MonoBehaviour
{
    public GameObject gameOverCanva;
    private List<AudioSource> audioSources = new List<AudioSource>();
    private float originalVolume = 1f;

    public void ShowGameOverCanva()
    {
        
        audioSources.Clear();
        audioSources.AddRange(FindObjectsOfType<AudioSource>());
        foreach (var source in audioSources)
        {
            source.volume = 0; 
        }

        gameOverCanva.SetActive(true);
        Time.timeScale = 0f; 
    }

    public void RestartGame()
    {
        
        foreach (var source in audioSources)
        {
            source.volume = originalVolume; 
        }

        Time.timeScale = 1f; 
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
    }
}
