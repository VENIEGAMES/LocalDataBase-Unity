using UnityEngine;
using UnityEngine.UI;

public class Example : MonoBehaviour {
	[SerializeField]
	InputField m_InputName;
	[SerializeField]
	Text m_TextName;

	public void OnClickSave(){
		PlayerLocalData.Instance.name = m_InputName.text;
		PlayerLocalData.Save();
	}

	public void OnClickLoad(){
		PlayerLocalData.Load();
		m_TextName.text = PlayerLocalData.Instance.name;
	}

	public void OnClickDelete(){
		PlayerLocalData.Delete();
	}
}

[System.Serializable]
public class PlayerLocalData : LocalDataBase<PlayerLocalData> {
	public string name;
	public int hp;
	public int mp;
}
