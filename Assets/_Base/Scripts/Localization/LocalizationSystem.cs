using System;
using UnityEngine;
using Localization = I2.Loc.LocalizationManager;

public static class LocalizationSystem
{
	public static event Action OnLanguageChanged;

	[RuntimeInitializeOnLoadMethod]
	private static void Initialize()
	{
		Localization.OnLocalizeEvent += OnLocalized;
	}

	private static void OnLocalized()
	{
		OnLanguageChanged?.Invoke();
	}

	public static string GetTranslation(string term)
	{
		return Localization.GetTranslation(term) ?? term;
	}
}