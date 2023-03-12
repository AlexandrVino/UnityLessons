using UnityEngine;
using UnityEngine.UI;

namespace ModelViewPresenter.Model
{
    public class Amount : Transformable
    {
        public Amount(Vector2 position, float rotation) : base(position, rotation) { }

        internal int _amount;

        private void Awake() => _amount = PlayerPrefs.GetInt("coins", 0);

        public void OnPickupCoin() => _amount++;

        public bool TryDiscard(int price)
        {
            if (_amount < price) return false;

            _amount -= price;
            return true;
        }
    }
}