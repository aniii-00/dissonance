using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    public string sceneToLoad = "exterior"; // Set this in the Inspector

    public void StartGame()
    {
        SceneManager.LoadScene("exterior");
    }

}




