using UnityEngine;
using UnityEngine.UI;

public class FlashlightToggle : MonoBehaviour
{
    public GameObject flashlightObject; 
    public Image flashlightIcon;
    private bool flashlightOn = false;

    public Light flashlightbulb;
    public GameObject flashlightUI;

    void Start()
    {
        flashlightObject.SetActive(false);

        //Icon Stuff 
        flashlightUI.SetActive(false);
        Color iconColor = flashlightIcon.color;
        iconColor.a = 0f;
        flashlightIcon.color = iconColor;

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && flashlightObject.activeSelf)
        {
            flashlightOn = !flashlightOn;
            flashlightbulb.enabled = flashlightOn;
            UpdateIcon();
        }
    }

    void UpdateIcon()
    {
        Color iconColor = flashlightIcon.color;
        iconColor.a = flashlightOn ? 1f : 0.3f; 
        flashlightIcon.color = iconColor;
    }
}
