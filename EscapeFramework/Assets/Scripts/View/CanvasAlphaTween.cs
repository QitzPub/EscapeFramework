using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasAlphaTween : MonoBehaviour
{
    [SerializeField]
    CanvasGroup canvasGroup;
    float currentEffectTime = 0;
    float durationTime = 0;
    float effectValue => targetAlpha*(currentEffectTime / durationTime);
    bool isCompleatedeffect => currentEffectTime >= durationTime;

    float targetAlpha = 0;
    // Start is called before the first frame update
    void Start()
    {
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
    }
    void Update()
    {
        if (isCompleatedeffect) return;
        currentEffectTime += Time.deltaTime;
        canvasGroup.alpha = effectValue;
    }
    public void SetAlphaTween(float defautlAlpha,float targetAlpha,float duration)
    {
        durationTime = duration;
        this.targetAlpha = targetAlpha;
        currentEffectTime = 0;
    }

}
