using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Jugar()
    {
        SceneManager.LoadScene("Level1_F1");
    }

    public void Salir()
    {
        Application.Quit();

        Debug.Log("Salir del juego");
    }
}