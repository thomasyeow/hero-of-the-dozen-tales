using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartingScreen : MonoBehaviour
{
    public GameObject viewMain;
    public GameObject viewSettings;
    public AudioMixer audioMixer;

    Resolution[] resolutions;
    public Dropdown resolutionDropdown;

    [SerializeField]
    private ResourcesSO woodSO;
    [SerializeField]
    private RunesInventory runesAmtSO;

    void Awake()
    {
        viewSettings.gameObject.SetActive(false);
        viewMain.gameObject.SetActive(true);

        resolutions = Screen.resolutions; //resolution dropdown start
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;//resolution dropdown end
    }


    public void ShowStartingScreen()
    {
        viewMain.gameObject.SetActive(true);
        viewSettings.gameObject.SetActive(false);
    }

    public void ShowSettings()
    {
        viewMain.gameObject.SetActive(false);
        viewSettings.gameObject.SetActive(true);
    }
    public void StartNewGame()
    {
        initializeInventory();
        SceneManager.LoadScene("3D Hero test");
        SoundManager.Instance.SetClip(SoundGenere.BACKGROUND);
    }
    public void ExitGame()
    {
        Application.Quit();
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
    public void SetFullscreen(bool isOn)
    {
        Screen.fullScreen = isOn;
    }
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }






    public void initializeInventory()           //resets + initializes resources 
    {
        woodSO.Value = 0;

        for (int i = 0; i < 10; i++)
        {
            runesAmtSO.runesAmt[i] = 0;
        #if UNITY_EDITOR
            EditorUtility.SetDirty(runesAmtSO);
        #endif
        }
    }
}
