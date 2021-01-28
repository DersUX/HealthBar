using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthBar : MonoBehaviour
{
	[SerializeField] private float _fadingTime;

	[SerializeField] private float _maxValue = 100;
	[SerializeField] private Color _color = Color.red;
	[SerializeField] private int _width = 4;
	[SerializeField] private Slider _slider;

	private float _current;

	private void Start()
	{
		_slider.fillRect.GetComponent<Image>().color = _color;

		_slider.maxValue = _maxValue;
		_slider.minValue = 0;
		_current = _maxValue;

		RectTransform rect = _slider.GetComponent<RectTransform>();

		int rectDeltaX = Screen.width / _width;
		float rectPosX = 0;

		rectPosX = rect.position.x - (rectDeltaX - rect.sizeDelta.x) / 2;
		_slider.direction = Slider.Direction.RightToLeft;

		rect.sizeDelta = new Vector2(rectDeltaX, rect.sizeDelta.y);
		rect.position = new Vector3(rectPosX, rect.position.y, rect.position.z);
	}

	private void Update()
	{
		if (_current < 0) _current = 0;
		if (_current > _maxValue) _current = _maxValue;
		_slider.value = _current;
	}

	private IEnumerator SmoothCorrentValue(float from, float to)
	{
		float time = 0.0f;

		while (time < 1.0f)
		{
			time += Time.deltaTime * (1.0f / _fadingTime);
			_current = Mathf.Lerp(from, to, time);

			yield return 0;
		}
	}

	public void OnDamageButtonClick()
    {
		StartCoroutine(SmoothCorrentValue(_current, _current - 10));
	}

	public void OnHealthButtonClick()
	{
		StartCoroutine(SmoothCorrentValue(_current, _current + 10));
	}
}