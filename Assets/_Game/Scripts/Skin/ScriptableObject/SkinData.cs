using System.Collections.Generic;
using UnityEngine;

public class SkinData : ScriptableObject
{
	[SerializeField] private List<BallSkinInfo> _balls;
	public IReadOnlyList<BallSkinInfo> Balls => _balls.AsReadOnly();
}