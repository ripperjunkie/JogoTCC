using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
   [SerializeField] string _musicVolumeParam = "masterMusic";

   [SerializeField] AudioMixer _musicMixer;

   [SerializeField] Slider _musicSlider;

   [SerializeField] float _multiplier = 30f;

   void Awake()
   {
       _musicSlider.onValueChanged.AddListener(HandleMusicSliderValueChanged);
   }

   void Start()
   {
       _musicSlider.value = PlayerPrefs.GetFloat(_musicVolumeParam, _musicSlider.value);
   }

   void OnDisable()
   {
       PlayerPrefs.SetFloat(_musicVolumeParam, _musicSlider.value);
   }

   void HandleMusicSliderValueChanged(float value)
   {
       _musicMixer.SetFloat(_musicVolumeParam, Mathf.Log10(value) * _multiplier);
   }
}
