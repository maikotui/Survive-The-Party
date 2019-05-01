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
