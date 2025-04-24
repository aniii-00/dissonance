using UnityEngine;
using TMPro;

public class TaskManager : MonoBehaviour
{
    public TextMeshProUGUI taskText;

    public void UpdateTask(string newTask)
    {
        taskText.text = newTask;
    }
}
