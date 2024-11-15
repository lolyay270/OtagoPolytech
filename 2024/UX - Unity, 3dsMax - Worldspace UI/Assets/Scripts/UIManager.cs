using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("Popup Message")]
    [SerializeField] Image popupBackground;
    [SerializeField] TMP_Text popupText;
    [SerializeField] float textVisibleSecs;
    [SerializeField] Vector2 scaleBG;
    [SerializeField] CanvasGroup popupCanvasG; //holds all canvas elements that need to be edited together
    [SerializeField] float fadePerFrame;

    Coroutine _fadePopup;
    Coroutine _timer;

    //ResettingTimer resetTimer;

    //runs when game starts, before Start method
    void Awake()
    {
        if (Instance == null) Instance = this; //singleton setup
        else Destroy(gameObject);
    }

    //runs first frame
    private void Start()
    {
        //start the popup invisible
        popupCanvasG.alpha = 0;
    }

    //runs when Interactable gets clicked
    public void ShowPopupText(string message)
    {
        popupCanvasG.alpha = 1;

        //set background size to fit around message
        float fontSize = popupText.fontSize;
        popupBackground.rectTransform.sizeDelta = new(message.Length * fontSize * scaleBG.x, fontSize * scaleBG.y);

        popupText.text = message;

        //reset timer
        if (_timer != null) StopCoroutine(_timer);
        if (_fadePopup != null) StopCoroutine(_fadePopup);
        _timer = StartCoroutine(VisibleTimer());

        //try
        //{
        //    //setup timer for how long text shows
        //    Instantiate(resetTimer);
        //    resetTimer.StartTimer(textVisibleSecs);
        //    //reset fade if it started already
        //    if (_fadePopup != null) StopCoroutine(_fadePopup);
        //    while (resetTimer.isRunning)
        //    {
        //        print("timer running");
        //    }
        //    print("Timer done");
        //    _fadePopup = StartCoroutine(FadePopup());
        //}
        //catch
        //{
        //    print("its an error");
        //}
    }

    IEnumerator FadePopup()
    {
        while (popupCanvasG.alpha > 0)
        {
            popupCanvasG.alpha -= fadePerFrame;
            yield return null;
        }
        _fadePopup = null;
    }

    IEnumerator VisibleTimer()
    {
        yield return new WaitForSeconds(textVisibleSecs);
        _fadePopup = StartCoroutine(FadePopup());
        _timer = null;
    }
}
