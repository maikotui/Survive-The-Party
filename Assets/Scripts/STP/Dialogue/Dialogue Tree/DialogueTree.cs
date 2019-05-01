﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace STP.Dialogue
{
    [CreateAssetMenu(menuName = "Dialogue/Dialogue Tree", fileName = "Tree")]
    public class DialogueTree : NodeGraph
    {
        /// <summary>
        /// Where the dialogue starts when initiated
        /// </summary>
        public DialogueNode StartNode { get; private set; }
        [SerializeField]
        private DialogueNode m_startNode; // needs to be serialized so it persists across scenes

        /// <summary>
        /// The currently addressed node in this graph. Starts with the start node.
        /// </summary>
        public DialogueNode CurrentNode { get; private set; }

        /// <summary>
        /// Resets the current node whenever this tree is enabled
        /// </summary>
        private void OnEnable()
        {
            CurrentNode = StartNode;
            ValidateNodes();
        }

        /// <summary>
        /// Ensures that every node this tree contains is valid. If not, displays a warning in the console.
        /// </summary>
        private void ValidateNodes()
        {
            if(StartNode == null)
            {
                Debug.LogWarning("No start node is set in Dialogue Tree '" + name + "'.");
            }

            foreach(Node node in nodes)
            {
                if(node is DialogueOption optionNode)
                {
                    if(optionNode.GetPort("m_source").Connection == null)
                    {
                        Debug.LogWarning("Empty source exists within a OptionNode in Dialogue Tree '" + name + "'.");
                    }
                    
                    if(optionNode.GetPort("m_destination").Connection == null)
                    {
                        Debug.LogWarning("Empty destination exists within a OptionNode in Dialogue Tree '" + name + "'.");
                    }
                }

                if(node is DialogueNode dialogueNode)
                {
                    if(dialogueNode.Speaker == null)
                    {
                        Debug.LogWarning("Empty speaker for DialogueNode in Dialogue Tree '" + name + "'.");
                    }
                }
            }
        }

        /// <summary>
        /// Travels to the node that is the destination of the option given. Returns the option that was chosen. If the optionID was invalid,
        /// returns null.
        /// </summary>
        public DialogueOption ChooseOption(int optionID)
        {
            if (CurrentNode.Options.Count > 0 && CurrentNode.Options.Count <= optionID)
            {
                DialogueOption chosenOption = CurrentNode.Options[optionID];
                CurrentNode = chosenOption.Destination;

                return chosenOption;
            }
            else
            {
                Debug.LogError("Invalid option chosen for Dialogue Tree '" + name + "'.");
                return null;
            }
        }

        /// <summary>
        /// Sets the start node to the given node. This prevents accidental setting of the StartNode outside of 
        /// STP.Dialogue through the property given above.
        /// </summary>
        public void SetStartNode(DialogueNode node)
        {
            if (node.graph == this)
            {
                StartNode = node;
            }
        }
    }
}
