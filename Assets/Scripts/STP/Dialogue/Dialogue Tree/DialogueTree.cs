using System.Collections;
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
