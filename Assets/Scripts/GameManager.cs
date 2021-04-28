using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    

    [SerializeField] int intersectionsBeforeIncrease = 10;
    [SerializeField] float startingTime = 2.5f;
    [SerializeField] float startingSpeed = 18;

    [SerializeField] float timeChangePerLevel = 0.2f;
    [SerializeField] float speedChangePerLevel = 2.5f;

    private int startingDifficulty = 0;
    
    private int currentLevel = 0;

    private float audioVolume = 1;

    private bool skipInstructionScreen = false;
    
    public static GameManager instance = null;

    AudioSource titleAudio;
    
    // Use this for initialization
    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        titleAudio = GetComponent<AudioSource>();
        // titleAudio.Play();

        
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    public int GetCurrentLevel()
    {
        return startingDifficulty + currentLevel;
    }

    public int GetIntersectionsBeforeIncrease()
    {
        return intersectionsBeforeIncrease;
    }

    public bool GetShowInstructionScreen()
    {
        return skipInstructionScreen;
    }
    public void SetShowInstructionScreen(bool showInstructions)
    {
        skipInstructionScreen = showInstructions;
    }

    public void IncrementCurrentLevel()
    {
        if (currentLevel < 10)  //10 is going to be the max level
        {
            currentLevel++;
        }
    }
    
    public float GetNextLevelTime()
    {

       return startingTime - GetCurrentLevel() * timeChangePerLevel;

    }

    public float GetNextLevelSpeed()
    {
        return startingSpeed + GetCurrentLevel() * speedChangePerLevel;
    }
    
    public void SetStartingDifficulty(int value)
    {
        startingDifficulty = value;
    }

    public void SetVolume(float newVolume)
    {
        audioVolume = newVolume;

        titleAudio.volume = audioVolume;
    }
    public float GetVolume()
    {
        return audioVolume;
    }

    public AudioSource GetAudio()
    {
        return titleAudio;
    }

   public float GetStartingDifficulty()
    {
        return startingDifficulty;
    }
}
