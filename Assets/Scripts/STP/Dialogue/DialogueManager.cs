using STP.Info;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace STP.Dialogue
{
    public class DialogueManager : Singleton<DialogueManager>
    {
        public bool DialogueIsActive { get => CurrentDialogue != null; }

        public DialogueTree CurrentDialogue { get; private set; }

        public delegate void DialogueChangedHandler(DialogueState state);
        public event DialogueChangedHandler OnDialogueChanged;

        /// <summary>
        /// Starts the given dialogue if one is not already active. Returns true if the Dialogue started, false otherwise.
        /// </summary>
        public bool StartDialogue(DialogueTree dialogue)
        {
            if (!DialogueIsActive) // Dialogue is NOT active
            {
                CurrentDialogue = dialogue;
                OnDialogueChanged?.Invoke(new DialogueState(dialogue.CurrentNode));
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Chooses the given option and invokes it's OnOptionChosen event. If the optionID is invalid or there is no active dialogue
        /// returns false. Returns true otherwise.
        /// </summary>
        public bool ChooseOption(int optionID)
        {
            if (DialogueIsActive && CurrentDialogue.CurrentNode.Options.Count > optionID)
            {
                DialogueOption chosenOption = CurrentDialogue.ChooseOption(optionID);
                chosenOption.OnOptionChosen?.Invoke();

                OnDialogueChanged?.Invoke(new DialogueState(CurrentDialogue.CurrentNode));
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Ends the current dialogue if one is active. If dialogue was ended, returns true. If not, returns false.
        /// </summary>
        public bool EndDialogue()
        {
            if (DialogueIsActive)
            {
                CurrentDialogue = null;
                OnDialogueChanged?.Invoke(new DialogueState());
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public class DialogueState
    {
        public bool IsActive { get; private set; }

        public CharInfo Speaker { get; private set; }

        public string Text { get; private set; }

        public string[] Options { get; private set; }

        public DialogueState()
        {
            IsActive = false;
        }

        public DialogueState(DialogueNode node)
        {
            IsActive = true;
            Speaker = node.Speaker;
            Text = node.Text;

            Options = new string[node.Options.Count];
            for (int i = 0; i < Options.Length; i++)
            {
                Options[i] = node.Options[i].Text;
            }
        }
    }
}
