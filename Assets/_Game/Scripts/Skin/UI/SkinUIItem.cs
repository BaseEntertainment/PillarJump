using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class SkinUIItem : MonoBehaviour
{
	[SerializeField] private Image _image;
	[SerializeField] private Image _CheckMark;
	[SerializeField] private Image _task;

	private Button _button;

	public int ID { get; private set; }

	public void Set(SkinInfo skinInfo)
	{
		ID = skinInfo.ID;
		_image.sprite = skinInfo.Image;
	}

	public void ResetButton(Action action)
	{
		_button = GetComponent<Button>();

		_button.onClick.RemoveAllListeners();
		_button.onClick.AddListener(() => action?.Invoke());
	}

	public void SetActivateTaskIcon(bool active)
	{
		_task.gameObject.SetActive(active);
	}

	public void SetActivateSelectedCheckMark(bool active)
	{
		_CheckMark.gameObject.SetActive(active);
	}
}