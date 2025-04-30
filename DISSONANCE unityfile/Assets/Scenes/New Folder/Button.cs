using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    public string sceneToLoad = "interior"; // Set this in the Inspector

    public void StartGame()
    {
        SceneManager.LoadScene("interior");
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

}




