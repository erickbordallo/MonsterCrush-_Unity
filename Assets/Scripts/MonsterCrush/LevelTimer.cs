using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTimer : Level {

    public int timeInSeconds;
    public int targetScore;

    private bool timeOut = false;
    private float timer;


    DataLoader dl;
    DataToLoad dtl;
    IDataSource iSource;
    JsonDataSource jSonData;

    AudioSource gamePlayAS;
    AudioSource levelAS;
    public AudioClip[] clipsWinLose;


    private void Awake()
    {
        dl = ServiceLocator.Get<DataLoader>();
        dtl = ServiceLocator.Get<DataToLoad>();


        iSource = dl.LoadedDataSources[dtl.GetLevelToLoad()];
        jSonData = iSource as JsonDataSource;

        InitializeValues();

        levelAS = GetComponent<AudioSource>();
        gamePlayAS = GameObject.Find("GameplayMusic").GetComponent<AudioSource>();
    }

    // Use this for initialization
    void Start ()
    {
        type = LevelType.TIMER;
        hud.SetLevelType(type);
        hud.SetScore(currentScore);
        hud.SetTarget(targetScore);
        hud.SetRemaining(string.Format("{0}:{1:00}", timeInSeconds / 60, timeInSeconds % 60));  

	}
	
	// Update is called once per frame
	void Update ()
    {
        if(!timeOut)
        {
            timer += Time.deltaTime;
            hud.SetRemaining(string.Format("{0}:{1:00}", (int)Mathf.Max((timeInSeconds - timer) / 60, 0), (int)Mathf.Max((timeInSeconds - timer) % 60, 0)));

            if (timeInSeconds - timer <= 0)
            {
                if (currentScore >= targetScore)
                {
                    gamePlayAS.Stop();
                    levelAS.clip = clipsWinLose[1];
                    levelAS.Play();

                    GameWin();
                }
                else
                {
                    gamePlayAS.Stop();
                    levelAS.clip = clipsWinLose[0];
                    levelAS.Play();

                    GameLose();
                }

                timeOut = true;
            }

        }
        
	}

    private void InitializeValues()
    {
        timeInSeconds = (int)jSonData.DataDictionary["TimeInSeconds"];
        targetScore = (int)jSonData.DataDictionary["TargetScore"];
    }

    public void BackToLevelSelection()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
    }
}
