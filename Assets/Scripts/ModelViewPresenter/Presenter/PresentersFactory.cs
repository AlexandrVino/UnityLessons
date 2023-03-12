using UnityEngine;
using System;
using ModelViewPresenter.Model;

public class PresentersFactory : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Presenter _coinTemplate;

    public void CreateCoin(Coin coin)
    {
        CreatePresenter(_coinTemplate, coin);
    }

    private Presenter CreatePresenter(Presenter template, Transformable model)
    {
        Presenter presenter = Instantiate(template);
        presenter.Init(model, _camera);

        return presenter;
    }
}
