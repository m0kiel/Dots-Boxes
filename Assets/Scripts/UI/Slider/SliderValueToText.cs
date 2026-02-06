using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderValueToText : MonoBehaviour
{
    private void Start()
    {
        TMP_Text textBox = transform.Find("TextBox").GetChild(0).GetComponent<TMP_Text>();
        GetComponent<Slider>().onValueChanged.AddListener(num => 
        {
            SoundManager.Instance.PlaySound(SoundType.SliderSlide);
            textBox.SetText(num.ToString()); 
        });
    }
}
