using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataToLoad : MonoBehaviour
{
    private string[] LoadedLevels = { "level1", "level2", "level3" };
    
    private int mCurrentLevel = 0;

    public string GetLevelToLoad() { return LoadedLevels[mCurrentLevel - 1]; }

    public void SetCurrentLevel(int level) { mCurrentLevel = level; }

    private void Awake()
    {
        ServiceLocator.Register<DataToLoad>(this);
    }

}
