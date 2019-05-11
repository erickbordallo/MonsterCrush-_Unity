using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JsonDataSource : ScriptableObject, IDataSource
{
	public string ID { get; set; }
	public bool IsLoading { get; set; }
	public bool IsLoaded { get; set; }
	public void Load() { }

	public TextAsset JsonTextFile;
	public Dictionary<string, object> DataDictionary;

	private void OnEnable()
	{
		DataDictionary = null;
		IsLoaded = false;
		IsLoading = false;
		ID = string.Empty;
	}

	public IEnumerator LoadAsync()
	{
		try
		{
			object deserializedObject = JsonFx.Json.JsonReader.Deserialize(JsonTextFile.text);
			if (deserializedObject is Dictionary<string, object>)
			{
				DataDictionary = (Dictionary<string, object>)deserializedObject;
				ID = JsonTextFile.name;
				IsLoaded = true;
			}
		}
		catch (System.Exception e)
		{
			Debug.LogError("Error Loading Json Data" + e.Message);
		}

		yield return null;
	}
}
