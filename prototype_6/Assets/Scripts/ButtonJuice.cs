using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonJuice : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler
{
    public AnimationCurve Curve;
    public float MaxScale;

    public float Duration = 0.3f;

    RectTransform mRectTransform;

    void Start()
    {
        mRectTransform = GetComponent<RectTransform>();
    }


    public void OnPointerEnter(PointerEventData eventData) => Fade(true);

    public void OnPointerExit(PointerEventData eventData) => Fade(false);

    public void OnSelect(BaseEventData eventData) => Fade(true);

    public void OnDeselect(BaseEventData eventData) => Fade(false);

    void Fade(bool up)
    {
        if(mFadeRoutine!=null)
            StopCoroutine(mFadeRoutine);
        mFadeRoutine = CR_Fade(up);
        StartCoroutine(mFadeRoutine);
    }
    
    IEnumerator mFadeRoutine;

    IEnumerator CR_Fade(bool up)
    {
        for (float t = 0; t < 1.0f; t += Time.deltaTime / Duration)
        {
            float time = up ? t : 1 - t;
            float eval = Curve.Evaluate(time);
            float scale = Mathf.LerpUnclamped(1.0f, MaxScale, eval);
            mRectTransform.localScale = scale * Vector3.one;
            yield return null;
        }
        mRectTransform.localScale = Vector3.one * (up? MaxScale : 1.0f);
    }
    
    
}
