using UnityEngine;

public class PlatformMover : MonoBehaviour
{
    private float speed = 5f;
    public float deactivationZ=-210f;

    public void StartMoving(float platformSpeed)
    {
        speed = platformSpeed;
    }

    void Update()
    {
        // Move the platform upwards continuously
        transform.Translate(Vector3.back* speed * Time.deltaTime);
        if (transform.position.z <= deactivationZ)
        {
            Debug.Log("Platform deactivated!");
            gameObject.SetActive(false);
        }
    }

    
}
