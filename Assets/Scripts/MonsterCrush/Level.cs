using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour {

    public enum LevelType
    {
        TIMER,
        OBSTACLE,
        MOVES,
    }

    DataLoader dl;
    DataToLoad dtl;
    IDataSource iSource;
    JsonDataSource jSonData;

    

    public Grid grid;
    public HUD hud;

    public int scoreStar1;
    public int scoreStar2;
    public int scoreStar3;

    protected LevelType type;
    public LevelType Type
    {
        get { return type; }
    }

    protected bool didWin;

    protected int currentScore;

    private void Awake()
    {
        dl = ServiceLocator.Get<DataLoader>();
        dtl = ServiceLocator.Get<DataToLoad>();


        iSource = dl.LoadedDataSources[dtl.GetLevelToLoad()];
        jSonData = iSource as JsonDataSource;

        InitializeValues();

        
    }


    // Use this for initialization
    void Start ()
    {
        hud.SetScore(currentScore);	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public virtual void GameWin()
    {
       
        didWin = true;
        StartCoroutine(WaitForGridFill());
    }

    public virtual void GameLose()
    {
        
        grid.GameOver();
        didWin = false;
        StartCoroutine(WaitForGridFill());
    }

    public virtual void OnMove()
    {
        Debug.Log("You Move");
    }

    public virtual void OnPieceCleared(GamePiece piece)
    {
        currentScore += piece.score;
        hud.SetScore(currentScore);
    }

    protected virtual IEnumerator WaitForGridFill()
    {
        while(grid.IsFilling)
        {
            yield return 0;
        }

        if(didWin)
        {
            hud.OnGameWin(currentScore);
        }
        else
        {
            hud.OnGameLose();
        }
    }


    private void InitializeValues()
    {
        scoreStar1 = (int)jSonData.DataDictionary["ScoreStar1"];
        scoreStar2 = (int)jSonData.DataDictionary["ScoreStar2"];
        scoreStar3 = (int)jSonData.DataDictionary["ScoreStar3"];
    }
}
