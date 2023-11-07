using System;
using UnityEngine;

[Serializable]
public class BallSkinInfo : SkinInfo
{
	[SerializeField] private BallSkin _prefab;

	public BallSkin Prefab => _prefab;


	[SerializeField] private BallSkinTaskListener _taksToOpenListener;
	public BallSkinTaskListener TaskToOpenBallSkinListener => _taksToOpenListener;
}