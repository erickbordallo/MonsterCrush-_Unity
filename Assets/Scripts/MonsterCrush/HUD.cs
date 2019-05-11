using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour {

    public Level level;
    public GameOver gameOver;

    public Text remainingText;
    public Text remainingSubText;
    public Text targetText;
    public Text targetSubText;
    public Text scoreText;
    public Image[] stars;

    private int starIdx = 0;

    // Use this for initialization
    void Start ()
    {
		for(int i = 0; i<stars.Length; i++)
        {
            if(i==starIdx)
            {
                stars[i].enabled = true;
            }
            else
            {
                stars[i].enabled = false;
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetScore(int score)
    {
        scoreText.text = score.ToString();

        int visibleStar = 0;

        if(score>=level.scoreStar1 && score < level.scoreStar2)
        {
            visibleStar = 1;
        }
        else if(score >= level.scoreStar2 && score < level.scoreStar3)
        {
            visibleStar = 2;
        }
        else if (score >= level.scoreStar3)
        {
            visibleStar = 3;
        }

        for(int i = 0; i< stars.Length; i++)
        {
            if(i==visibleStar)
            {
                stars[i].enabled = true;
            }
            else
            {
                stars[i].enabled = false;
            }
        }

        starIdx = visibleStar;
    }

    public void SetTarget(int target)
    {
        targetText.text = target.ToString();
    }

    public void SetRemaining(int remaining)
    {
        remainingText.text = remaining.ToString();
    }

    public void SetRemaining(string remaining)
    {
        remainingText.text = remaining;
    }

    public void SetLevelType(Level.LevelType type)
    {
        if(type == Level.LevelType.MOVES)
        {
            remainingSubText.text = "Moves Remaining";
            targetSubText.text = "Target Score";
        }
        else if (type == Level.LevelType.OBSTACLE)
        {
            remainingSubText.text = "Moves Remaining";
            targetSubText.text = "Asteroids Remaining";
        }
        else if (type == Level.LevelType.TIMER)
        {
            remainingSubText.text = "Time Remaining";
            targetSubText.text = "Target Score";
        }
    }

    public void OnGameWin(int score)
    {
        gameOver.ShowWin(score, starIdx);

    }

    public void OnGameLose()
    {
        gameOver.ShowLose();
    }
}
