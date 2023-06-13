using System.Collections.Generic;
using TMPro;

namespace Assets.Scripts.Service
{
    internal class Message
    {
        private int currentChoiceIndex;

        public readonly string Text;
        public readonly List<Message> Choices;
        public FontStyles Style { get; set; }
        public Message Next { get; set; }

        public Message(string text, FontStyles style = FontStyles.Normal)
        {
            Text = text;
            Style = style;
            Choices = new();
        }

        public Message(string text, List<Message> choices, FontStyles style = FontStyles.Normal)
        {
            Text = text;
            Choices = choices;
            Style = style;
        }

        public void MoveNext()
        {
            if (Choices.Count == 0 || currentChoiceIndex == Choices.Count - 1)
                return;
            Choices[currentChoiceIndex].Style = FontStyles.Normal;
            currentChoiceIndex++;
            Next = Choices[currentChoiceIndex].Next;
            Choices[currentChoiceIndex].Style = FontStyles.Bold;
        }

        public void MoveBack()
        {
            if (Choices.Count == 0 || currentChoiceIndex == 0)
                return;
            Choices[currentChoiceIndex].Style = FontStyles.Normal;
            currentChoiceIndex--;
            Next = Choices[currentChoiceIndex].Next;
            Choices[currentChoiceIndex].Style = FontStyles.Bold;
        }
    }
}