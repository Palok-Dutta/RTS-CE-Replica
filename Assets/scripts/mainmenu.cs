using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;

public class mainmenu : MonoBehaviour
{
    public GameObject loadingscreen;
    public Slider slider;
    public Text prtext;
    public GameObject storep;
    public GameObject buttonOfPlane1;
    public GameObject buttonPurChasedPlane1;
    
    public float score;
    public int coin=0;
    public Text scoretext;
    public Text cointext;
    
    void Start()
    {
        
        score=PlayerPrefs.GetFloat("score");
        scoretext.text= score.ToString("F0");
        coin=PlayerPrefs.GetInt("coin");
        cointext.text=coin.ToString();
        
    }
    void Update()
    {
        if (PLAYERMOVEMENT.howmuchscorecollected>score)
        {
            PlayerPrefs.SetFloat("score",PLAYERMOVEMENT.howmuchscorecollected);
            score = PLAYERMOVEMENT.howmuchscorecollected;
            scoretext.text= score.ToString("F0");
            
        }
        if (PLAYERMOVEMENT.howmuchcoins > 0)
        {
            coin+=PLAYERMOVEMENT.howmuchcoins;
            PlayerPrefs.SetInt("coin",coin);
            
            cointext.text=coin.ToString();
            PLAYERMOVEMENT.howmuchcoins = 0;
        }
 
    }

    public void Playmenu(int sceneIndex)
    {
        StartCoroutine(LoadAsyncronously(sceneIndex));
    }
    public void Quitgame()
    {
        Application.Quit();
    }
    public void Storemenu()
    {
        storep.SetActive(true);
        Time.timeScale=0f;
    }
    public void plane1()
    {
        VehicleManager.ind=1;
        buttonOfPlane1.SetActive(false);
        buttonPurChasedPlane1.SetActive(true);
        
    }
    public void plane2()
    {
        VehicleManager.ind=2;
        storep.SetActive(false);
        Time.timeScale=1f;
    }
    public void crossbutton()
    {
        storep.SetActive(false);
        Time.timeScale=1f;
    }
    public void ResetScore()
    {
        score = 0;
        PLAYERMOVEMENT.howmuchscorecollected = 0;
        PlayerPrefs.SetFloat("score", 0);
        PlayerPrefs.Save();
        scoretext.text = score.ToString("F0");
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
