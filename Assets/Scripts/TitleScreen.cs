using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    [SerializeField] GameObject settingsPanel;
    [SerializeField] Button startButton;
    [SerializeField] Button settingsButton;
    [SerializeField] Button doneButton;
    [SerializeField] Toggle instructionToggle;

    [SerializeField] Toggle easy;
    [SerializeField] Toggle medium;
    [SerializeField] Toggle hard;

    [SerializeField] Slider volumeSlider;

    private bool skipInstructionScreen = false;

    // Start is called before the first frame update
    void Start()
    {
        settingsPanel.SetActive(false);

        volumeSlider.value = GameManager.instance.GetVolume();

        switch(GameManager.instance.GetStartingDifficulty())
        {
            case 0:
                easy.isOn = true;
                break;
            case 4:
                medium.isOn = true;
                break;
            case 8:
                hard.isOn = true;
                break;
            default:
                break;
        }

        instructionToggle.isOn = GameManager.instance.GetShowInstructionScreen();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartButtonPushed()
    {
        GameManager.instance.GetAudio().Stop();
        
        if(skipInstructionScreen)
        {
            SceneManager.LoadScene(2);   //scene 2 is the main game screen
        }
        else
        {
            SceneManager.LoadScene(1);  //scene 1 is the instruction screen
        }

    }

    public void SettingsButtonPushed()
    {
        settingsPanel.SetActive(true);
        startButton.gameObject.SetActive(false);
        settingsButton.gameObject.SetActive(false);

    }

    public void DoneButtonPushed()
    {
        settingsPanel.SetActive(false);
        startButton.gameObject.SetActive(true);
        settingsButton.gameObject.SetActive(true);
    }

    public void SkipInstructionsToggled()
    {
        skipInstructionScreen = !skipInstructionScreen;

        GameManager.instance.SetShowInstructionScreen(skipInstructionScreen);
    }

    public void EasyDifficulty()
    {
        if (easy.isOn)
        {
            //Debug.LogError("Easy");
            GameManager.instance.SetStartingDifficulty(0);
        }
    }

    public void MediumDifficulty()
    {
        if (medium.isOn)
        {
           // Debug.LogError("Medium");
            GameManager.instance.SetStartingDifficulty(4);
        }
    }

    public void HardDifficulty()
    {
        if (hard.isOn)
        {
            // Debug.LogError("Hard");
            GameManager.instance.SetStartingDifficulty(8);
        }
    }

    public void VolumeChanged()
    {
        GameManager.instance.SetVolume(volumeSlider.value);

       
    }
    
}
