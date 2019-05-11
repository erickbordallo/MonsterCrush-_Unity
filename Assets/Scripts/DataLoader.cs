using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataLoader : MonoBehaviour, IGameModule
{
	public List<UnityEngine.Object> DataSources;
	public event Action OnSourcesLoaded;
	public Dictionary<string, IDataSource> LoadedDataSources;

	private void Awake()
	{
		// sanity check - & internal setup
		LoadedDataSources = new Dictionary<string, IDataSource>();
		OnSourcesLoaded += PrintData;
		GameLoader.CallOnComplete(Init);
	}

	private void Init()
	{
		ServiceLocator.Register<DataLoader>(this);
	}

	public IEnumerator LoadModule()
	{
		Debug.Log("<color=lime>Loading Data Module</color>");
		foreach (UnityEngine.Object obj in DataSources)
		{
			if (obj is IDataSource)
			{
				IDataSource source = obj as IDataSource;
				yield return LoadAsync(source);
			}
		}
		
		yield return new WaitForEndOfFrame();
		Debug.Log("<color=orange>Data Sources Loaded!</color>");
		yield return null;
	}

	private IEnumerator LoadAsync(IDataSource source)
	{
		if (!source.IsLoading)
		{
			Debug.Log("<color=magenta>Loading Source: </color>" + source.ID);
			source.IsLoading = true;
			yield return source.LoadAsync();
			source.IsLoaded = true;
			LoadedDataSources.Add(source.ID, source);
		}
	}

	private void PrintData()
	{
		foreach (KeyValuePair<string, IDataSource> kvp in LoadedDataSources)
		{
			Debug.Log(string.Format("<color=yellow> Key:{0} Value:{1}</color>", kvp.Key, kvp.Value));
		}
	}



}
