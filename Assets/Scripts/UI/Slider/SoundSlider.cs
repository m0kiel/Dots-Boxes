using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SoundSlider : MonoBehaviour
{
    [SerializeField] MixerGroupType mixerGroupType;

    private void Start()
    {
        GetComponent<Slider>().onValueChanged.AddListener(num =>
        {
            SoundManager.Instance.GetAudioMixerGroup(mixerGroupType).audioMixer.SetFloat("MasterVolume", Mathf.Log10(num) * 20.0f);
        });
    }
}
