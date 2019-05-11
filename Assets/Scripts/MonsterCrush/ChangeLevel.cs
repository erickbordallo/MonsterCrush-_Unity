using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeLevel : MonoBehaviour
{
    DataLoader dl;
    DataToLoad dtl;
    IDataSource iSource;
    JsonDataSource jSonData;

    private void Awake()
    {
        dl = ServiceLocator.Get<DataLoader>();
        dtl = ServiceLocator.Get<DataToLoad>();


    }

    public void GoToLevel(int level)
    {
        dtl.SetCurrentLevel(level);

        iSource = dl.LoadedDataSources[dtl.GetLevelToLoad()];
        jSonData = iSource as JsonDataSource;

        string levelType = jSonData.DataDictionary["LevelType"].ToString();
        if (levelType == "TargetScore")
        {
            SceneManager.LoadScene(3);
        }
        else if (levelType == "BreakingObstacle")
        {
            SceneManager.LoadScene(4);
        }
        else if(levelType == "Timer")
        {
            SceneManager.LoadScene(5);
        }
    }
}
