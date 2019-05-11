using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    private void Awake()
    {
    }

    // Use this for initialization
    void Start ()
    {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
    }
}
