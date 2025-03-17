using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
public class pause : MonoBehaviour
{
    public Slider slider;
    public GameObject loadingscreen;
    public Text prtext;
    public GameObject pausepanel;
    
    public void pauseclick()
    {
        pausepanel.SetActive(true);
        Time.timeScale=0f;
    }
    public void resumec()
    {
        pausepanel.SetActive(false);
        Time.timeScale=1f;
    }
    public void restartc()
    {
        Time.timeScale=1f;
        SceneManager.LoadScene(2);
    }
    public void hometap()
    {
        
        StartCoroutine(LoadAsyncronously(0));
        Time.timeScale=1f;
    }
    IEnumerator LoadAsyncronously(int sceneIndex)
    {
        loadingscreen.SetActive(true);
        AsyncOperation operation=SceneManager.LoadSceneAsync(sceneIndex);
        while (!operation.isDone)
        {
            float progress=Mathf.Clamp01(operation.progress/0.9f);
            slider.value=progress;
            prtext.text=progress*100f + "%";
            yield return null;
        }

    }

}
