﻿using System;
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


    private Delegate _action;
    private object[] _parameters;


    public void SetYesButton()
    {
        notificationWindow.SetActive(false);
        if (_action != null)
        {
            _action.DynamicInvoke(_parameters);
        }

    }
    public void SetNoButton()
    {
        notificationWindow.SetActive(false);

    }
    public void SetOkButton()
    {
        notificationWindow.SetActive(false);

    }


    public void SetText(string text)
    {
        _text.text = text;
    }

    public void ActiveYesNo(Delegate action, object[] parameters)
    {

        if (!notificationWindow.activeSelf)
        {
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
    public void ActiveOk()
    {

        if (!notificationWindow.activeSelf)
        {
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
}