using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMoves : Level {

    DataLoader dl;
    DataToLoad dtl;
    IDataSource iSource;
    JsonDataSource jSonData;

    AudioSource gamePlayAS;
    AudioSource levelAS;
    public AudioClip[] clipsWinLose;

    public int numMoves;
    public int targetScore;

    private int movesUsed = 0;



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
        type = LevelType.MOVES;

        hud.SetLevelType(type);
        hud.SetScore(currentScore);
        hud.SetTarget(targetScore);
        hud.SetRemaining(numMoves);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void OnMove()
    {
        movesUsed++;

        hud.SetRemaining(numMoves - movesUsed);

        if (numMoves - movesUsed == 0)
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
        }
    }


    private void InitializeValues()
    {
        numMoves = (int)jSonData.DataDictionary["NumMovements"];
        targetScore = (int)jSonData.DataDictionary["TargetScore"];
    }

    public void BackToLevelSelection()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
    }
}
