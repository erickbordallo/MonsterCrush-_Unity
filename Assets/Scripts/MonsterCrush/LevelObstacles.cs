using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelObstacles : Level {

    public int numMoves;

    public Grid.PieceType[] obstacleTypes;

    private int numObstaclesLeft;

    private int movesUsed = 0;


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
        type = LevelType.OBSTACLE;
        for (int i = 0; i<obstacleTypes.Length; i++)
        {
            numObstaclesLeft += grid.GetPiecesOfType(obstacleTypes[i]).Count;
        }

        hud.SetLevelType(type);
        hud.SetScore(currentScore);
        hud.SetTarget(numObstaclesLeft);
        hud.SetRemaining(numMoves);

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void OnMove()
    {
        movesUsed++;

       hud.SetRemaining(numMoves - movesUsed);

        if(numMoves - movesUsed == 0 && numObstaclesLeft > 0)
        {
            gamePlayAS.Stop();
            levelAS.clip = clipsWinLose[0];
            levelAS.Play();

            grid.GameOver();
            GameLose();
        }
    }

    public override void OnPieceCleared(GamePiece piece)
    {
        base.OnPieceCleared(piece);

        for(int i = 0; i< obstacleTypes.Length; i++)
        {
            if(obstacleTypes[i] == piece.Type)
            {
                numObstaclesLeft--;

                hud.SetTarget(numObstaclesLeft);

                if(numObstaclesLeft == 0)
                {
                    currentScore += 1000 * (numMoves - movesUsed);
                    hud.SetScore(currentScore);

                    gamePlayAS.Stop();
                    levelAS.clip = clipsWinLose[1];
                    levelAS.Play();


                    GameWin();
                }
            }
        }
    }

    private void InitializeValues()
    {
        numMoves = (int)jSonData.DataDictionary["NumMovements"];
    }

    public void BackToLevelSelection()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
    }
}
