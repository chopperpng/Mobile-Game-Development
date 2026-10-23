using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenuController : MonoBehaviour
{
    public void startGame()
    {
        SceneManager.LoadScene("gameScene");
    }
}
