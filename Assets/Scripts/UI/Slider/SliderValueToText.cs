using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderValueToText : MonoBehaviour
{
    [SerializeField] bool isMusicSlider;
    private void Start()
    {
        TMP_Text textBox = transform.Find("TextBox").GetChild(0).GetComponent<TMP_Text>();
        GetComponent<Slider>().onValueChanged.AddListener(num => 
        {
            SoundManager.Instance.PlaySound(SoundType.SliderSlide);

            if (isMusicSlider)
            {
                num *= 100;
                int numToDisplay = (int)num;
                textBox.SetText(numToDisplay.ToString());
            }
            else
            {
                textBox.SetText(num.ToString());
            }
        });
    }
}
