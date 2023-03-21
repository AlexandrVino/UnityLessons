using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPItem : MonoBehaviour
{
    [SerializeField] private AnimationCurve _scaleCurve;
    [SerializeField] private float _animationTime;

    public void Display()
    {
        StartCoroutine(ScaleAnimation());
    }

    // Колебание масштаба счетчика
    private IEnumerator ScaleAnimation()
    {
        for (float t = 0; t < 1f; t += Time.deltaTime / _animationTime)
        {
            transform.localScale = Vector3.one * _scaleCurve.Evaluate(t);
            yield return null;
        }
        transform.localScale = Vector3.one;
    }
}
