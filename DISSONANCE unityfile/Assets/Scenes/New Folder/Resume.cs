using UnityEngine;
using UnityEngine.SceneManagement;

public class Resume : MonoBehaviour
{
    public string sceneToLoad = "interior"; // Set this in the Inspector

    public void ResumeGame()
    {
        SceneManager.LoadScene("interior");
    }

}

