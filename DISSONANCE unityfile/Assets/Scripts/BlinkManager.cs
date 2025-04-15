using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BlinkManager : MonoBehaviour
{
    public Slider blinkSlider;
    public Animator blinkAnimator;
    public float blinkTimer = 5f;
    public float decreaseRate = 1f;
    public float blinkAnimationDuration = 1f; // ← set this in Inspector to match your animation length

    private float currentTime;
    private bool isBlinking = false;

    void Start()
    {
        ResetBlinkTimer();
    }

    void Update()
    {
        if (!isBlinking)
        {
            currentTime -= Time.deltaTime * decreaseRate;
            blinkSlider.value = currentTime;

            if (Input.GetKeyDown(KeyCode.Q))
            {
                TriggerBlink();
            }

            if (currentTime <= 0f)
            {
                TriggerBlink();
            }
        }
    }

    void TriggerBlink()
    {
        if (isBlinking) return;

        isBlinking = true;
        blinkAnimator.SetTrigger("Blink");
        Debug.Log("Blink triggered!");

        StartCoroutine(ResetAfterBlink());
    }

    IEnumerator ResetAfterBlink()
    {
        yield return new WaitForSeconds(blinkAnimationDuration);
        ResetBlinkTimer();
    }

    void ResetBlinkTimer()
    {
        currentTime = blinkTimer;
        blinkSlider.maxValue = blinkTimer;
        blinkSlider.value = currentTime;
        isBlinking = false;
    }
}
