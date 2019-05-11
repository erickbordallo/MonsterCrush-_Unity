﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public static class ScriptableObjectUtility
{
	public static void CreateAsset<T>() where T : ScriptableObject
	{
		var asset = ScriptableObject.CreateInstance<T>();
		ProjectWindowUtil.CreateAsset(asset, "New " + typeof(T).Name + ".asset");
	}
}

public class DataAssetMenuItem
{
	[MenuItem("Assets/Create/DataSource/JsonDataSource")]
	public static void CreateTextDataSource()
	{
		ScriptableObjectUtility.CreateAsset<JsonDataSource>();
	}
}
