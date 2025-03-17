using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PLAYERMOVEMENT : MonoBehaviour
{
    public GameObject[] vehicles;
    public GameObject particleeffect;
    public GameObject maincamera;
    public float x=11.1f;
    public float y=2.07f;
    public float z=-127f;
    public GameObject plane;
    public float xplane=0f;
    public Text coincollected;// In game show coins
    public GameObject totalcoincollected; //To remove showing coins after finish
    public static int howmuchcoins=0; //How many coins collected
    public static float howmuchscorecollected=0f; //How many score
    public GameObject scorepanel; //panel to show states
    private bool isdestroyed=false; //To ensure if object destroyed or not
    public Text scoretotal; //To show total score
    public GameObject totalscorecollected; // To remove showing total score after finish
    public GameObject buttons; //pause button remove after finish
    public Text ShowScore; //Call to show the score states
    public Text ShowCoins; //Call to show coin states
    public float TimeRemaining=0f;
    public float screenmid;
    public Touch touch;
    public AudioSource destruction;
    public AudioSource planeMovement;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        howmuchcoins=0;
        howmuchscorecollected=0f;
        destruction.Stop();
        planeMovement.Play();
        Time.timeScale=1f;
        screenmid=Screen.width/2;
        plane=Instantiate(vehicles[VehicleManager.ind],gameObject.transform.position,Quaternion.identity);
        plane.transform.localRotation=Quaternion.Euler(0f,90f,0f);
        plane.transform.SetParent(transform);
        plane.transform.localScale=new Vector3(0.5f,0.5f,0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
        howmuchscorecollected+=3f*Time.deltaTime;
        if (Input.touchCount<=0)
        {
            if (Input.GetKeyDown(KeyCode.Return) && isdestroyed==true)
            {
                isdestroyed=false;
                SceneManager.LoadSceneAsync(0);
                Time.timeScale = 1f;
                
                
            }
            if (isdestroyed)
            {
                TimeRemaining += 2f * Time.unscaledDeltaTime; // Use unscaledDeltaTime to access after game pause
            }
            if (Input.GetKey(KeyCode.D))
            {
                transform.Translate(Vector3.right*10f*Time.deltaTime);
                plane.transform.localRotation=Quaternion.Euler(xplane,90f,0f);
                if (xplane<30f)
                {
                    xplane+=30f*Time.deltaTime;
                }
                
            }
            else if (Input.GetKey(KeyCode.A))
            {
                transform.Translate(Vector3.left*10f*Time.deltaTime);
                plane.transform.localRotation=Quaternion.Euler(xplane,90f,0f);
                if (xplane>-30f)
                {
                    xplane-=30f*Time.deltaTime;
                }
            }
            else
            {
                if (Math.Abs(xplane)<0.5f)
                {
                    xplane=0f;
                }
                else
                {
                    if (xplane>0f)
                    {
                        xplane-=30f*Time.deltaTime;
                    }
                    else if (xplane<0f)
                    {
                        xplane+=30f*Time.deltaTime;
                    }
                }
                plane.transform.localRotation=Quaternion.Euler(xplane,90f,0f);
            }
        }
        else if (Input.touchCount>0)
        {
            touch = Input.GetTouch(0);
            if ((touch.position.x<=screenmid || touch.position.x>screenmid) && isdestroyed==true)
            {
                isdestroyed=false;
                SceneManager.LoadSceneAsync(0);
                Time.timeScale = 1f;
                
                
            }
            if (isdestroyed)
            {
                TimeRemaining += 2f * Time.unscaledDeltaTime; // Use unscaledDeltaTime to access after game pause
            }
            if (touch.position.x>screenmid)
            {
                transform.Translate(Vector3.right*10f*Time.deltaTime);
                plane.transform.localRotation=Quaternion.Euler(xplane,90f,0f);
                if (xplane<30f)
                {
                    xplane+=30f*Time.deltaTime;
                }
                
            }
            else if (touch.position.x<screenmid)
            {
                transform.Translate(Vector3.left*10f*Time.deltaTime);
                plane.transform.localRotation=Quaternion.Euler(xplane,90f,0f);
                if (xplane>-30f)
                {
                    xplane-=30f*Time.deltaTime;
                }
            }
            else
            {
                if (Math.Abs(xplane)<0.5f)
                {
                    xplane=0f;
                }
                else
                {
                    if (xplane>0f)
                    {
                        xplane-=30f*Time.deltaTime;
                    }
                    else if (xplane<0f)
                    {
                        xplane+=30f*Time.deltaTime;
                    }
                }
                plane.transform.localRotation=Quaternion.Euler(xplane,90f,0f);
            }
        }
        
        coincollected.text=howmuchcoins.ToString();
        scoretotal.text=howmuchscorecollected.ToString("F0");
        ShowCoins.text=coincollected.text;
        ShowScore.text=scoretotal.text;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Finish")
        {
            planeMovement.Stop();
            buttons.SetActive(false);
            totalcoincollected.SetActive(false);
            totalscorecollected.SetActive(false);
            Time.timeScale=0f;
            particleeffect.SetActive(true);
            destruction.Play();
            transform.GetChild(2).gameObject.SetActive(false);
            
            
            StartCoroutine(CameraMovement());
            StartCoroutine(iswaitforscorepanel());
            StartCoroutine(forDestroyTrue());
            
        }
        if (other.gameObject.tag=="coins")
        {
            other.gameObject.SetActive(false);
            Destroy(other.gameObject);
            howmuchcoins+=1;

        }
    }
    IEnumerator iswaitforscorepanel()
    {
        
        yield return new WaitForSecondsRealtime(2f);
        scorepanel.SetActive(true);
    }
    IEnumerator forDestroyTrue()
    {
        yield return new WaitForSecondsRealtime(2.1f);
        isdestroyed=true;
    }
    

    
    IEnumerator CameraMovement()
    {
        float startTime = Time.unscaledTime; // Track time with unscaledTime for consistent movement
        
        while (TimeRemaining<6f)
        {
            // Move the camera upwards using unscaledDeltaTime
            
            maincamera.transform.position=new Vector3(transform.position.x,y,z);
            y+=3f*Time.unscaledDeltaTime;
            z-=10f*Time.unscaledDeltaTime;
            
            
            // Adjust the camera's rotation as well
            maincamera.transform.localRotation = Quaternion.Euler(x, 0f, 0f);
            x += 5f * Time.unscaledDeltaTime;

            // You can exit the loop after some condition, for example, after a few seconds or distance.
            // If you want the camera to move indefinitely until the effect starts:
            if (x > 60f) // Example condition to stop the movement (you can adjust this condition as needed)
                break;

            yield return null; // Wait until the next frame
            
        }
        
        
    }
}
