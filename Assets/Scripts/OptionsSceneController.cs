using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsSceneController : MonoBehaviour
{
    public Toggle fullSceneTog, vsyncTog;
    public Button increaseResolution, descreaseResolution, resolution;
    public ResItem[] resItems;
    public AudioMixer audioMixer;
    public Slider masterSlider, musicSlider, sfxSlider;
    public Text masterVolumeText, musicVolumeText, sfxVolumeText;
    public AudioSource sfxLoop;

    private int currentResItem;

    void Start()
    {
        fullSceneTog.isOn = Screen.fullScreen;
        vsyncTog.isOn = QualitySettings.vSyncCount == 1 ? true : false;
        resItems = new ResItem[] { new ResItem { width = 480, height = 360 },
                                    new ResItem { width = 720, height = 480},
                                    new ResItem { width = 1080, height = 720 },
                                    new ResItem { width = 1920, height = 1080 }
                   };
        currentResItem = -1;
        for (int i = 0; i < resItems.Length; i++)
        {
            if (Screen.width == resItems[i].width && Screen.height == resItems[i].height)
            {
                currentResItem = i;
                break;
            }
        }
        if (currentResItem == -1)
        {
            currentResItem = resItems.Length - 1;
        }
        resolution.GetComponentInChildren<Text>().text = resItems[currentResItem].toString();
        OnClickApplyGraphics();
        sfxLoop.Stop();

        if (PlayerPrefs.HasKey("MasterVol"))
        {
            masterSlider.value = PlayerPrefs.GetFloat("MasterVol");
            OnChangeMasterVolume();
        }

        if (PlayerPrefs.HasKey("MusicVol"))
        {
            musicSlider.value = PlayerPrefs.GetFloat("MusicVol");
            OnChangeMusicVolume();
        }

        if (PlayerPrefs.HasKey("SFXVol"))
        {
            sfxSlider.value = PlayerPrefs.GetFloat("SFXVol");
            OnChangeSFXVolume();
        }

    }

    public void OnClickButtonClose()
    {
        SceneManager.UnloadSceneAsync("OptionsScene");
    }

    public void OnClickApplyGraphics()
    {
        Screen.fullScreen = fullSceneTog.isOn;
        QualitySettings.vSyncCount = vsyncTog.isOn ? 1 : 0;
        Screen.SetResolution(resItems[currentResItem].width, resItems[currentResItem].height, fullSceneTog.isOn);
    }

    public void OnClickIncreaseResolutionButton()
    {
        currentResItem++;
        if (currentResItem > resItems.Length - 1)
        {
            currentResItem = 0;
        }
        resolution.GetComponentInChildren<Text>().text = resItems[currentResItem].toString();
    }

    public void OnClickDescreaseResolutionButton()
    {
        currentResItem--;
        if (currentResItem < 0)
        {
            currentResItem = resItems.Length - 1;
        }
        resolution.GetComponentInChildren<Text>().text = resItems[currentResItem].toString();
    }

    public void OnChangeMasterVolume()
    {
        masterVolumeText.text = (masterSlider.value + 80).ToString();
        audioMixer.SetFloat("Master", masterSlider.value);
        PlayerPrefs.SetFloat("MasterVol", masterSlider.value);
    }

    public void OnChangeMusicVolume()
    {
        musicVolumeText.text = (musicSlider.value + 80).ToString();
        audioMixer.SetFloat("Music", musicSlider.value);
        PlayerPrefs.SetFloat("MusicVol", musicSlider.value);
    }

    public void OnChangeSFXVolume()
    {
        sfxVolumeText.text = (sfxSlider.value + 80).ToString();
        audioMixer.SetFloat("SFX", sfxSlider.value);
        PlayerPrefs.SetFloat("SFXVol", sfxSlider.value);
    }

    public void PlaySFX()
    {
        sfxLoop.Play();
    }

    public void StopSFX()
    {
        sfxLoop.Stop();
    }

}



[System.Serializable]
public class ResItem
{
    public int width, height;
    
    public string toString()
    {
        return width + " X " + height;
    }
}