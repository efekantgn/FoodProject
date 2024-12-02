using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.Rendering.RenderGraphModule;
using UnityEngine.UIElements;

public class Warning : MonoBehaviour
{
    public static Warning instance;
    public float FadeDuration;
    public float WarnignStayTime;
    public CanvasGroup cg;
    public TextMeshProUGUI Text;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    public void SetWarning(string value)
    {
        Text.text = value;
    }
    public void GiveWarning(string value)
    {
        SetWarning(value);
        FadeIn();
        Invoke(nameof(FadeOut), WarnignStayTime);
    }
    private void FadeIn()
    {
        cg.DOFade(1f, FadeDuration);
    }

    // Fade-Out (Canvas alfa değeri 1'den 0'a düşer)
    private void FadeOut()
    {
        cg.DOFade(0f, FadeDuration);
    }
}