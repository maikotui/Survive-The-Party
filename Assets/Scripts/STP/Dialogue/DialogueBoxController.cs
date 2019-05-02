using STP.Info;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace STP.Dialogue
{
    public class DialogueBoxController : MonoBehaviour
    {
        public Animator Animator;

        public Image SpeakerPortrait;
        public Text SpeakerName;

        public Text DialogueTextBox;

        public Button ContinueButton;

        public Button Option1;
        public Button Option2;
        public Button Option3;
        public Button Option4;

        private Button[] m_options;

        private void OnEnable()
        {
            m_options = new Button[] { Option1, Option2, Option3, Option4};
            DialogueManager.Instance.OnDialogueChanged += DialogueChangedHandler;
        }

        private void DialogueChangedHandler(DialogueState state)
        {
            if (state.IsActive)
            {
                // Display the current speaker
                SpeakerPortrait.sprite = state.Speaker.Portrait;
                SpeakerName.text = state.Speaker.name;

                // Display the current dialogue text
                DisplayDialogueText(state.Text);

                // Display the options if any are present;
                DisplayOptions(state.Options);
            }
            else
            {
                // TODO: End dialogue
            }
        }

        private void DisplayDialogueText(string text)
        {
            StopAllCoroutines();
            StartCoroutine(TypeText(text));
        }

        private IEnumerator TypeText(string text)
        {
            DialogueTextBox.text = "";
            foreach(char character in text)
            {
                DialogueTextBox.text += character;
                yield return null;
            }
        }

        private void DisplayOptions(string[] options)
        {
            // TODO: animate options being displayed

            for(int i = 0; i < options.Length; i++)
            {
                m_options[i].GetComponentInChildren<Text>().text = options[i];
            }
        }
    }
}
