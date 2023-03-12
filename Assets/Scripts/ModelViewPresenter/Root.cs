using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ModelViewPresenter.Model;

public class Root : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Amount _amountModel;

    [SerializeField] private PresentersFactory _factory;
    [SerializeField] private AmountPresenter _amountPresenter;


    private void Awake()
    {
        _amountModel = new Amount(new Vector2(0.0f, 0.0f), 0);

        // _amountPresenter.Init(_amountModel, _camera);
        _amountPresenter.Init(this);
    }

    public void OnPickupCoin()
    {
        _amountModel.OnPickupCoin();
        _amountPresenter.SetCoinAmount(_amountModel._amount);
    }

    public void TryDiscard(int price)
    {
        if (_amountModel.TryDiscard(price)) _amountPresenter.SetCoinAmount(_amountModel._amount);
    }

}
