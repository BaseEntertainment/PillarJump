using System;
using UnityEngine;

[Serializable]
public abstract class SkinInfo
{
	[SerializeField] private string _name;
	[SerializeField] private int _id;
	[SerializeField] private Sprite _image;
	[TextArea(2, 5)]
	[SerializeField] private string _taskToOpenDescription;

	public string Name => _name;
	public int ID => _id;
	public Sprite Image => _image;
	public string TaskToOpenDescription => _taskToOpenDescription;
}
