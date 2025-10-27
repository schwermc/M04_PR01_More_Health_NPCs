using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    [SerializeField]  private Slider _slider;

    void Start()
    {
        _slider = GetComponentInChildren<Slider>();
        GetComponentInParent<IHealth>().OnHPPctChanged += HandleHPPctChanged;
    }

    void Update()
    {
        _slider.transform.LookAt(Camera.main.transform);
    }

    void HandleHPPctChanged(float pct)
	{
		_slider.value = pct;
	}
}