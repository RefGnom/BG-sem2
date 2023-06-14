using Assets.Scripts.Service;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] List<TextMeshProUGUI> choiceList;

    private Message message;
    private bool enabledDialog;

    public static DialogManager Instance;

    void Awake()
    {
        Instance = this;
    }

    public void Enable(Message message)
    {
        enabledDialog = true;
        this.message = message;
    }

    public void Disable()
    {
        enabledDialog = false;
        text.text = "";
        for (int i = 0; i < choiceList.Count; i++)
        {
            choiceList[i].text = "";
        }
    }

    void Update()
    {
        if (!enabledDialog)
            return;
        UpdateUI();
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            message.MoveNextChoice();
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            message.MoveBackChoice();
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            message.OnNext?.Invoke();
            if (message.Next is null)
            {
                Disable();
                return;
            }
            message = message.Next;
        }
    }

    void UpdateUI()
    {
        text.text = message.Text;
        text.fontStyle = message.Style;
        for (int i = 0; i < choiceList.Count; i++)
        {
            choiceList[i].text = "";
        }
        if (message.Choices.Count == 0)
        {
            return;
        }
        for (int i = 0; i < message.Choices.Count; i++)
        {
            choiceList[i].text = message.Choices[i].Text;
            choiceList[i].fontStyle = message.Choices[i].Style;
        }
    }
}