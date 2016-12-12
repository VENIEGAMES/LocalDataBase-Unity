using UnityEngine;
using System.Collections.Generic;
using System.IO;

/// <summary>
/// ローカルに保存/読み込みできる基底クラス
/// </summary>
[System.Serializable]
public abstract class LocalDataBase<T> where T : new()
{
	public static T Instance{
		get{
			if(m_RuntimeInstance == null) {
				m_RuntimeInstance = new T();
			}
			return m_RuntimeInstance;
		}
	}
	static T m_RuntimeInstance;

	/// <summary>
	/// 保存先のフォルダ
	/// </summary>
	private static string SaveFolderPath = Application.persistentDataPath + "/LocalData/";

	/// <summary>
	/// 保存ファイル名
	/// </summary>
	/// <value>The save path.</value>
	private static string SavePath{
		get{
			return SaveFolderPath + Instance.GetType().FullName + ".dat";
		}
	}

	/// <summary>
	/// データをローカルに保存
	/// </summary>
	public static void Save()
	{
		if(!Directory.Exists(SaveFolderPath)){
			Directory.CreateDirectory(SaveFolderPath);
		}
		string json = JsonUtility.ToJson(Instance);
		File.WriteAllText(SavePath, json);
	}

	/// <summary>
	/// データをローカルから読み込み
	/// </summary>
	public static void Load()
	{
		T deserializedObject = new T();
		if (File.Exists (SavePath)) {
			try
			{
				deserializedObject = JsonUtility.FromJson<T>(File.ReadAllText(SavePath));
			}
			catch
			{
				Debug.Log(string.Format("{0}の定義が変更された可能性があるため読み込めませんでした。", (SavePath)));
			}
		}
		m_RuntimeInstance = deserializedObject;
	}

	/// <summary>
	/// データを削除
	/// </summary>
	public static void Delete()
	{
		File.Delete(SavePath);
	}
}
