using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private string persistanceSceneName; // Sabit sahne adı
    [SerializeField] private CanvasGroup fadeCanvasGroup; // Fade efekt için bir CanvasGroup
    [SerializeField] private float fadeDuration = 1f; // Fade geçiş süresi
    [SerializeField] private IntEventChannelSO SceneLoadChannel; // Fade geçiş süresi

    private void Awake()
    {
        LoadScene(1);
    }
    private void OnEnable()
    {
        SceneLoadChannel.OnEventRaised += LoadScene;
    }

    private void OnDisable()
    {
        SceneLoadChannel.OnEventRaised -= LoadScene;

    }
    public void LoadScene(int buildIndex)
    {
        StartCoroutine(LoadSceneRoutine(buildIndex));
    }

    private IEnumerator LoadSceneRoutine(int buildIndex)
    {
        // Fade-out
        if (fadeCanvasGroup != null)
        {
            yield return StartCoroutine(Fade(1f));
        }

        // Mevcut sahneleri unload et
        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.name != persistanceSceneName)
        {
            SceneManager.UnloadSceneAsync(currentScene);
        }

        // Yeni sahneyi yükle
        yield return SceneManager.LoadSceneAsync(buildIndex, LoadSceneMode.Additive);

        // Yeni sahneyi aktif olarak ayarla
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(buildIndex));

        // Fade-in
        if (fadeCanvasGroup != null)
        {
            yield return StartCoroutine(Fade(0f));
        }
    }

    private IEnumerator Fade(float targetAlpha)
    {
        float startAlpha = fadeCanvasGroup.alpha;
        float timeElapsed = 0f;

        while (timeElapsed < fadeDuration)
        {
            fadeCanvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, timeElapsed / fadeDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        fadeCanvasGroup.alpha = targetAlpha;
    }
}
