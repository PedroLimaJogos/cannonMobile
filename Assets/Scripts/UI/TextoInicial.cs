using TMPro;
using UnityEngine;
using DG.Tweening;

public class TextoInicial : MonoBehaviour
{
    public float multiplier = 0.5f;
    public float duration = 0.5f;
    public Ease animationType = Ease.InOutSine;

    private void Start() {
        transform.DOScale(multiplier, duration).SetLoops(-1, LoopType.Yoyo).SetEase(animationType);
    }
    
}
