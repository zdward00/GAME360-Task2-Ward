using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public void PlayGame()
    {
        Debug.Log($"Clicked");
        SceneManager.LoadScene("singleton");
        
    }

    public void Quit()
    {
        Debug.Log($"Clicked");
        GameManager.Instance.quitGame();
    }
}
