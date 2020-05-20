using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public void GotoMainGame()
    {
        SceneManager.LoadScene("MainGame");
    }
    
}
