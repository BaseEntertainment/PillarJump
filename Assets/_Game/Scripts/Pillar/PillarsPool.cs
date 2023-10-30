using System;
using System.Collections.Generic;
using UnityEngine;

public class PillarsPool : MonoBehaviour
{
	[Range(5, 200)]
	[SerializeField] private int _size = 15;
	[SerializeField] private Pillar _prefab;

	private static PillarsPool _instance;

	private static readonly Queue<Pillar> _pool = new();

	private static readonly List<Pillar> _activePillars = new();

	public static List<Pillar> ActivePillars => _activePillars;

	private void Awake()
	{
		if (_instance == null)
		{
			_instance = this;

			InitializePool();
		}
	}

	void OnDestroy()
	{
		_pool.Clear();
		_activePillars.Clear();
	}

	private void InitializePool()
	{
		for (int i = 0; i < _size; i++)
		{
			AddNewObjectToPool();
		}
	}

	private void AddNewObjectToPool()
	{
		var cube = Instantiate(_prefab, transform);
		cube.gameObject.SetActive(false);
		_pool.Enqueue(cube);
	}

	public static Pillar Instantiate(Vector3 position)
	{
		if (_instance == null)
		{
			return null;
		}

		if (_pool.Count == 0)
		{
			_instance.AddNewObjectToPool();
		}

		var pillar = _pool.Dequeue();

		pillar.transform.position = position;
		pillar.gameObject.SetActive(true);

		_activePillars.Add(pillar);

		return pillar;
	}

	public static void Destroy(Pillar pillar)
	{
		if (_instance == null)
		{
			return;
		}

		if (pillar == null)
		{
			return;
		}

		pillar.gameObject.SetActive(false);
		_pool.Enqueue(pillar);

		_activePillars.Remove(pillar);
	}

	public static void DestroyAllActivePillars()
	{
		foreach (var cube in _activePillars)
		{
			Destroy(cube);
		}

		_activePillars.Clear();
	}
}