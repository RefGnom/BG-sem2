using System;
using System.Collections.Generic;
using TMPro;

namespace Assets.Scripts.Service
{
    public class Message
    {
        private int currentChoiceIndex;
        private bool getNextFromChoice;

        public readonly string Text;
        public readonly List<Message> Choices;
        public FontStyles Style { get; set; }
        public Message Next { get; set; }
        public Action OnNext { get; set; }

        public Message(string text, FontStyles style = FontStyles.Normal, bool getNextFromChoice = true)
        {
            Text = text;
            Style = style;
            Choices = new();
            this.getNextFromChoice = getNextFromChoice;
        }

        public Message(string text, List<Message> choices, FontStyles style = FontStyles.Normal, bool getNextFromChoice = true)
        {
            Text = text;
            Choices = choices;
            Style = style;
            Next = Choices[0].Next;
            OnNext = Choices[0].OnNext;
            this.getNextFromChoice = getNextFromChoice;
        }

        public void MoveNextChoice()
        {
            if (Choices.Count == 0 || currentChoiceIndex == Choices.Count - 1)
                return;
            Choices[currentChoiceIndex].Style = FontStyles.Normal;
            currentChoiceIndex++;
            if (getNextFromChoice)
                Next = Choices[currentChoiceIndex].Next;
            OnNext = Choices[currentChoiceIndex].OnNext;
            Choices[currentChoiceIndex].Style = FontStyles.Bold;
        }

        public void MoveBackChoice()
        {
            if (Choices.Count == 0 || currentChoiceIndex == 0)
                return;
            Choices[currentChoiceIndex].Style = FontStyles.Normal;
            currentChoiceIndex--;
            if (getNextFromChoice)
                Next = Choices[currentChoiceIndex].Next;
            OnNext = Choices[currentChoiceIndex].OnNext;
            Choices[currentChoiceIndex].Style = FontStyles.Bold;
        }
    }
}