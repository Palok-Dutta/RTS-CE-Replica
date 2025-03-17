using UnityEngine;
using UnityEngine.SceneManagement;

public class restart : MonoBehaviour
{
    public void restartclick()
    {
        SceneManager.LoadScene(2);
    }
}
