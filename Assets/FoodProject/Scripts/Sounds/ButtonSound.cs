using UnityEngine;
using UnityEngine.UI;

public class ButtonSound : MonoBehaviour
{
    private Button button;
    private void Awake()
    {
        button = GetComponentInChildren<Button>();
    }
    private void OnEnable()
    {
        button.onClick.AddListener(OnButtonClick);
    }
    private void OnDisable()
    {
        button.onClick.RemoveListener(OnButtonClick);
    }
    public void OnButtonClick()
    {
        SFXManager.Instance.PlaySound(SFXManager.Instance.ButtonClick);
    }
}

