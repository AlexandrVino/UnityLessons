using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartController : MonoBehaviour
{
    [SerializeField] private GameObject[] _models;
    [SerializeField] private Interface _interface;

    private int _currSelected = 2;

    public void RemoveHP()
    {
        if (_currSelected < 0) return;

        _models[_currSelected].SetActive(false);
        _currSelected--;

        if (_currSelected == -1) _interface.LoadDeadWindow();
    }

    public void AddHP()
    {
        if (_currSelected >= _models.Length) return;

        _currSelected++;
        _models[_currSelected].SetActive(true);
    }
}
