using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ModelViewPresenter.Model;


public class AmountPresenter : MonoBehaviour
{
    [SerializeField] private Text _render;

    private Root _init;

    public void Init(Root init)
    {
        _init = init;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Contains("coin"))
        {
            _init.OnPickupCoin();
            Destroy(collision.gameObject);
        }
    }

    public void SetCoinAmount(int amount)
    {
        _render.text = $"coins: {amount}";
        PlayerPrefs.SetInt("coins", amount);
    }

    public void TryDiscard(int price)
    {
        _init.TryDiscard(price);
    }

}
