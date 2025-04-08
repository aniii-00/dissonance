using UnityEngine;

public class BlinkController : MonoBehaviour
{
    public Animator blinkAnimator;
    public string blinkTriggerName = "Blink"; // name of the trigger in Animator

    void Start()
    {
  
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            blinkAnimator.Play("blinking");
            Debug.Log("blinking");
        }

    }
}
