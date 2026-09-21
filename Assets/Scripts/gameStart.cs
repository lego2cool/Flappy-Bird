using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class gameStart : MonoBehaviour
{
    public Button startButton;

    void Start()
    {
        startButton.onClick.AddListener(StartGame);
    }

    void OnDestroy()
    {
        // Best Practice: Always remove listeners when the object is destroyed to prevent memory leaks
        startButton.onClick.RemoveListener(StartGame);
    }
    
    void StartGame()
    {
        SceneManager.LoadScene("Game");
    }
}
