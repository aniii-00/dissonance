using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitGame : MonoBehaviour
{
    public string sceneToLoad = "main menu"; // Set this in the Inspector

    public void Quit()
    {
        SceneManager.LoadScene("main menu");
    }

}

