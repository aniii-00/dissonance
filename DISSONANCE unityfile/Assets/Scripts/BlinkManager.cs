using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BlinkManager : MonoBehaviour
{

    // - { Basic Blink Variables }- \\
    public Slider blinkSlider;
    public Animator blinkAnimator;
    public float blinkTimer = 5f;
    public float decreaseRate = 1f;
    public float blinkAnimationDuration = 1f;

    private float currentTime;
    private bool isBlinking = false;

    // - { Mission #1 Variables } - \\
    public int blinkLightThreshold = 5;
    private int blinkCount = 0;
    public Light[] lights;
    public DialogueManager dialogueManager;

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

    // - { Basic Blink Mechanic } - \\
    void TriggerBlink()
    {
        if (isBlinking) return;

        isBlinking = true;
        blinkAnimator.Play("blinking");
  

        StartCoroutine(ResetAfterBlink());

    // - { Lights Turn Off} - \\
        blinkCount++;
        Debug.Log("Blink Count:" + blinkCount);

        if (blinkCount >= blinkLightThreshold)
        {
            CutLights();
        }
    }

    // - { Blink Bar UI } - \\
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

    // - { Mission # 1} - \\
    void CutLights()
    {
        foreach (Light light in lights)
        {
            light.enabled = false;
        }

        dialogueManager.StartDialogue(new string[] {
            "...The lights? Seriously? I think I saw a flashlight downstairs. I pray that it doesn't need batteries."
        });
    }
}
