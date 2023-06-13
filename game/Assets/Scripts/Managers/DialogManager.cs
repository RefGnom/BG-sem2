using Assets.Scripts.Service;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] List<TextMeshProUGUI> choiceList;

    Message message;
    bool enabledDialog;

    public static DialogManager instance;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        message = DialogSystem.GetMessage();
    }

    public void Enable()
    {
        enabledDialog = true;
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
            message.MoveBack();
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            message.MoveNext();
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
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