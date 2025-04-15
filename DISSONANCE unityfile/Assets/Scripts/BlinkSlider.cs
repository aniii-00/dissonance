using UnityEngine;
using UnityEngine.UI;

public class BlinkSlider : MonoBehaviour
{
    public Slider slider;
    public float blinkTimer = 5f; // How long before a forced blink
    public float decreaseRate = 1f; // How fast the bar decreases per second
    public Animation blinkAnimation;

    private float currentTime;
    private bool isBlinking = false;

    void Start()
    {
        currentTime = blinkTimer;
        slider.maxValue = blinkTimer;
        slider.value = currentTime;
    }

    void Update()
    {
        if (!isBlinking)
        {
            currentTime -= Time.deltaTime * decreaseRate;
            slider.value = currentTime;

            if (currentTime <= 0f)
            {
                ForceBlink();
            }
        }
    }

    public void ManualBlink() // Call this when pressing Q or something
    {
        if (!isBlinking)
        {
            ResetBlinkTimer();
            blinkAnimation.Play();
        }
    }

    void ForceBlink()
    {
        isBlinking = true;
        blinkAnimation.Play();
        Invoke(nameof(ResetBlinkTimer), blinkAnimation.clip.length);
    }

    void ResetBlinkTimer()
    {
        currentTime = blinkTimer;
        slider.value = currentTime;
        isBlinking = false;
    }
}
