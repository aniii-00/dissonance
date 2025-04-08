using UnityEngine;

public class BlinkController : MonoBehaviour
{
    public Animator blinkAnimator;
    public string blinkTriggerName = "Blink"; // name of the trigger in Animator

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            blinkAnimator.SetTrigger(blinkTriggerName);
            Debug.Log("BlinkPressed");
        }
    }
}
