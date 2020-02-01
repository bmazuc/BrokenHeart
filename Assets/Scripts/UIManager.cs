using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Canvas menuInGame;

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void GoToMenuInGame()
    {
        menuInGame.gameObject.SetActive(true);
    }

    public void GoGame()
    {
        menuInGame.gameObject.SetActive(false);
    }
}
