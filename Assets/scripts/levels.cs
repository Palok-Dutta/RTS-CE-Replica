using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;
public class levels : MonoBehaviour
{
    public GameObject loadscreen;
    public Text progresstext;
    public Slider slider;
    public void Playgame(int scene2index)
    {
        StartCoroutine(isloading(scene2index));
    }
    IEnumerator isloading(int scene2index)
    {
        loadscreen.SetActive(true);
        AsyncOperation operation=SceneManager.LoadSceneAsync(scene2index);
        while (!operation.isDone)
        {
            float pr=Mathf.Clamp01(operation.progress/0.9f);
            slider.value=pr;
            progresstext.text=pr*100f + "%";
            yield return null;
        }
    }
}
