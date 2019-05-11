using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDataSource
{
	string ID { get; set; }
	bool IsLoading { get; set; }
	bool IsLoaded { get; set; }
	IEnumerator LoadAsync();
	void Load();
}
