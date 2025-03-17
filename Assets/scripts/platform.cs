using UnityEngine;

public class platformmove : MonoBehaviour
{
    public GameObject player;
    private GameObject platform;
    private GameObject curr;
    public int length=150;
    public GameObject cubes;
    public GameObject parentObject;
    private GameObject obs;
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.back*20f*Time.deltaTime);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag=="spawn")
        {
            float xposition=player.transform.position.x;
            Debug.Log("jgjfd");
            platform=gameObject;
            curr = Instantiate(gameObject, new Vector3(xposition, -1f, 400f), Quaternion.identity);
            for (int i = 0; i < length; i++)
            {
                float xrange=UnityEngine.Random.Range(xposition-200f,xposition+200f);
                float zrange=UnityEngine.Random.Range(300f,400f);
                obs=Instantiate(cubes,new Vector3(xrange,0f,zrange),Quaternion.identity);
                obs.transform.SetParent(parentObject.transform);
            }

        }
        if (other.gameObject.tag=="Finish")
        {
            if (platform!=null)
            {
                Destroy(platform);
                platform=null;
            }
        }
    }
}
