using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Interface : MonoBehaviour
{
    [SerializeField] private Resources _resources;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private TextMeshProUGUI _textTitle;
    [SerializeField] private GameObject _menu;

    [SerializeField] private GameObject _character;
    [SerializeField] private HeartController _heartController;

    public void RemoveHP()
    {
        _heartController.RemoveHP();
    }

    public void LoadDeadWindow()
    {
        _textTitle.text = "Вы проиграли";
        _text.text = "Монеток: " + _resources.Coins.ToString();
        _character.SetActive(false);
        _menu.SetActive(true);
    }

    public void UnloadDeadWindow()
    {
        _character.SetActive(true);
        _menu.SetActive(false);
    }

    public void LoadFinishWindow()
    {
        _textTitle.text = "Вы выйграли !!!";
        _text.text = "Монеток: " + _resources.Coins.ToString();
        _character.SetActive(false);
        _menu.SetActive(true);
    }

    public void UnloadFinishWindow()
    {
        _character.SetActive(true);
        _menu.SetActive(false);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
