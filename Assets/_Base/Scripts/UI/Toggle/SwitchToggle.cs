using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class SwitchToggle : MonoBehaviour
{
	[SerializeField] private RectTransform _handle;
	[SerializeField] private Image _background;

	[SerializeField, ColorUsage(showAlpha: false)] private Color _activeColor = new(0.188f, 0.858f, 0.356f);
	[SerializeField, ColorUsage(showAlpha: false)] private Color _inactiveColor = new(0.682f, 0.682f, 0.698f);

	[SerializeField, Range(0, 1)] private float _animationDuration = 0.1f;

	public Toggle Toggle { get; private set; }

	private Vector2 _handlePosition;

	private void Awake()
	{
		Toggle = GetComponent<Toggle>();

		_handlePosition = _handle.anchoredPosition;

		Toggle.onValueChanged.AddListener(Switch);
	}

	private void OnEnable()
	{
		Switch(Toggle.isOn);
	}

	private void OnDestroy()
	{
		Toggle.onValueChanged.RemoveListener(Switch);
	}

	private void Switch(bool isOn)
	{
		_handle.DOAnchorPos(isOn ? -(_handlePosition) : _handlePosition, _animationDuration);

		_background.color = Toggle.isOn ? _activeColor : _inactiveColor;
	}
}
