using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderValueToText : MonoBehaviour
{
    [SerializeField] bool isMusicSlider;

    [SerializeField] TMP_Text textBox;

    private void Awake()
    {
        if (isMusicSlider)
        {
            SetSoundVolumeToText(SoundManager.Instance.CurrentMasterVolume);
        }

        GetComponent<Slider>().onValueChanged.AddListener(num => 
        {
            SoundManager.Instance.PlaySound(SoundType.SliderSlide);

            SetSoundVolumeToText(num);
        });
    }

    private void OnEnable()
    {
        if (!isMusicSlider) { return; }
        GetComponent<Slider>().value = SoundManager.Instance.CurrentMasterVolume;
        SetSoundVolumeToText(GetComponent<Slider>().value);
    }

    private void OnDisable()
    {
        if (!isMusicSlider) { return; }
        SoundManager.Instance.SetMasterVolume(GetComponent<Slider>().value);
    }

    private void SetSoundVolumeToText(float amount)
    {
        if (isMusicSlider)
        {
            amount *= 100;
            int numToDisplay = (int)amount;
            textBox.SetText(numToDisplay.ToString());
        }
        else
        {
            textBox.SetText(amount.ToString());
        }
    }
}
