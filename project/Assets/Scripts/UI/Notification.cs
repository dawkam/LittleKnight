using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Notification :MonoBehaviour
{
    public Text _text;
    public GameObject notificationWindow;
    public GameObject yesButton;
    public GameObject noButton;
    public GameObject okButton;

    public CanvasGroup canvasGroup;

    private Delegate _action;
    private object[] _parameters;


    #region Singleton
    public static Notification instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Notification !");
        }

        instance = this;
    }

    #endregion
    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {

            CloseWindow();
        }
    }

    public void SetYesButton()
    {
        CloseWindow();
        if (_action != null)
        {
            _action.DynamicInvoke(_parameters);
        }

    }
    public void SetNoButton()
    {
        CloseWindow();

    }
    public void SetOkButton()
    {
        CloseWindow();

    }

    public void ActiveYesNo(Delegate action, object[] parameters, string text)
    {

        if (!notificationWindow.activeSelf)
        {
            _text.text = text;
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
            _action = action;
            _parameters = parameters;
            notificationWindow.SetActive(true);
            yesButton.SetActive(true);
            noButton.SetActive(true);
            okButton.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Istnieje już jedno powiadomienie");
        }
    }
    public void ActiveOk(string text)
    {

        if (!notificationWindow.activeSelf)
        {
            _text.text = text;
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
            _action = default;
            notificationWindow.SetActive(true);
            yesButton.SetActive(false);
            noButton.SetActive(false);
            okButton.SetActive(true);
        }
        else
        {
            Debug.LogWarning("Istnieje już jedno powiadomienie");
        }
    }

    public bool IsFree()
    {
        return !notificationWindow.activeSelf;

    }

    private void CloseWindow()
    {
        notificationWindow.SetActive(false);
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }
}
