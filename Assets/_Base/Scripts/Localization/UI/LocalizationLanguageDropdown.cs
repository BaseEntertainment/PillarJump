using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Localization = I2.Loc.LocalizationManager;

[RequireComponent(typeof(TMP_Dropdown))]
public class LocalizationLanguageDropdown : MonoBehaviour
{
	private TMP_Dropdown _dropdown;

	private void Awake()
	{
		_dropdown = GetComponent<TMP_Dropdown>();
	}

	private void Start()
	{
		ResetDropdown();
	}

	private void ResetDropdown()
	{
		if (Localization.Sources.Count == 0)
		{
			Localization.UpdateSources();
		}

		var allLanguages = Localization.GetAllLanguages();

		ResetOptions(allLanguages);
		ResetCurrentValue(allLanguages);

		AddListener();
	}

	private void ResetOptions(List<string> languages)
	{
		_dropdown.ClearOptions();
		_dropdown.AddOptions(languages);
	}
	private void ResetCurrentValue(List<string> languages)
	{
		_dropdown.value = languages.IndexOf(Localization.CurrentLanguage);
	}

	private void AddListener()
	{
		_dropdown.onValueChanged.AddListener(OnValueChanged);
	}

	private void OnValueChanged(int value)
	{
		Localization.CurrentLanguage = _dropdown.options[value].text;
	}
}