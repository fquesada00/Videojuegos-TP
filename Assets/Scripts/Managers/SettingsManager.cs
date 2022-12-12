using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SettingsManager : MonoBehaviour
{

    [SerializeField] private Slider _volumeSlider;
    [SerializeField] private Toggle _fullscreenToggle;
    [SerializeField] private Dropdown _resolutionDropdown;
    [SerializeField] private Dropdown _qualityDropdown;
    [SerializeField] private Slider _mouseSensitivitySlider;

    [SerializeField] private Vector2 _mouseSensivity = new Vector2(700, 3);   
    

    // Start is called before the first frame update
    void Start()
    {
        //set volume state
        _volumeSlider.value = AudioListener.volume;

        //set fullscreen state
        _fullscreenToggle.isOn = Screen.fullScreen;

        //set resolution state
        _resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        int currentResolutionIndex = 0;
        for (int i = 0; i < Screen.resolutions.Length; i++)
        {
            string option = Screen.resolutions[i].width + "x" + Screen.resolutions[i].height + " " + Screen.resolutions[i].refreshRate + "Hz";
            options.Add(option);

            if (Screen.resolutions[i].width == Screen.currentResolution.width && Screen.resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        _resolutionDropdown.AddOptions(options);
        _resolutionDropdown.value = currentResolutionIndex;
        _resolutionDropdown.RefreshShownValue();

        //set quality state
        _qualityDropdown.ClearOptions();
        options = new List<string>();
        for (int i = 0; i < QualitySettings.names.Length; i++)
        {
            options.Add(QualitySettings.names[i]);
        }
        _qualityDropdown.AddOptions(options);
        _qualityDropdown.value = QualitySettings.GetQualityLevel();
        _qualityDropdown.RefreshShownValue();

        //set mouse sensitivity state TODO 

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setVolume(float volume){
        AudioListener.volume = volume;
    }

    public void setFullscreen(bool isFullscreen){
        Screen.fullScreen = isFullscreen;
    }

    public void setResolution(int resolutionIndex){
        Resolution resolution = Screen.resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void setQuality(int qualityIndex){
        QualitySettings.SetQualityLevel(qualityIndex);
    }


}
