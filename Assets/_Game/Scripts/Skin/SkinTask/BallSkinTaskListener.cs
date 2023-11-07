using System;
using UnityEngine;

[Serializable]
public abstract class BallSkinTaskListener : MonoBehaviour
{
	[SerializeField] private int SkinID;

	public static event Action<int> SkinOpened;

	protected virtual void OnSkinOpened()
	{
		SkinOpened?.Invoke(SkinID);
	}
}