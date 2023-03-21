using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCounter : MonoBehaviour
{
    private int _coinsPerClick = 1;
    [SerializeField] private Resources _resources;


    private void OnTriggerEnter2D(Collider2D collider)
    {

        if (collider.gameObject.TryGetComponent(out Coins coin))
        {
            Destroy(collider.gameObject); 
            _resources.CollectCoins(_coinsPerClick, transform.position);
        }
    }

    public void AddCoinsPerClick(int value) => _coinsPerClick += value;
}
