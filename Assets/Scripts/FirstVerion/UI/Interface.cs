using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Interface : MonoBehaviour
{
    [SerializeField] private GameObject _character;
    [SerializeField] private GameObject _deadWindow;
    [SerializeField] private HeartController _heartController;

    public void RemoveHP()
    {
        _heartController.RemoveHP();
    }

    public void LoadDeadWindow()
    {
        _character.SetActive(false);
        _deadWindow.SetActive(true);
    }

    public void UnloadDeadWindow()
    {
        _character.SetActive(true);
        _deadWindow.SetActive(false);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
