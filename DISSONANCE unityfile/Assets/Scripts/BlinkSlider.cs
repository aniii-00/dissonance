using UnityEngine;
using UnityEngine.UI;

public class BlinkSlider : MonoBehaviour

{
    int blinkprogress = 0;
    public Slider slider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
   public void UpdateProgress()
   {
    blinkprogress++;
    slider.value = blinkprogress;
   }
}
