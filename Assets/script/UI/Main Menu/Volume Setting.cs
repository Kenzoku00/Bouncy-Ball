using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSetting : MonoBehaviour
{
    [SerializeField] private AudioMixer myMixer;
    [SerializeField] private Slider musicSlider;

    private void Start()
    {
        SetMasterVolume();
    }

    public void SetMasterVolume()
    {
        float Master = musicSlider.value;
        myMixer.SetFloat("Master", Mathf.Log10(Master)*20);
    }
}
