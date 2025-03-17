using UnityEngine;
using TMPro;
public class FpsShower : MonoBehaviour
{
    public TextMeshProUGUI FpsText;
    private float pollingTime=1f;
    private float time;
    private int frameCount;

    void Start()
    {
        // Disable VSync to uncap the frame rate
        QualitySettings.vSyncCount = 1;
        Application.targetFrameRate = Screen.currentResolution.refreshRate;
    }
    void Update()
    {
        time+=Time.deltaTime;
        frameCount++;
        if (time>=pollingTime)
        {
            int framRate=Mathf.RoundToInt(frameCount/time);
            FpsText.text=framRate.ToString() + " FPS";
            time-=pollingTime;
            frameCount=0;
        }
    }
}
