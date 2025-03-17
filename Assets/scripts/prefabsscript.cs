using UnityEngine;

public class prefabsmovement : MonoBehaviour
{
    public float deactivationZ=-300f;
    void Update()
    {
        transform.Translate(Vector3.forward * -20f * Time.deltaTime);
        if (transform.position.z <= deactivationZ)
        {
            Debug.Log("Platform deactivated!");
            gameObject.SetActive(false);
        }
    }
    
}

