using UnityEngine;
using UnityEngine.Events;
using XNode;

namespace STP.Dialogue
{
    [CreateNodeMenu("Dialogue Option"), NodeWidth(325)]
    public class DialogueOption : Node
    {
        // Declaration of all ports for this node
        [SerializeField, Input(connectionType = ConnectionType.Override, typeConstraint = TypeConstraint.Strict)]
        private DialogueNode m_source;
        [SerializeField, Output(connectionType = ConnectionType.Override)]
        private DialogueOption m_destination;

        /// <summary>
        /// The text to be displayed to the viewer
        /// </summary>
        public string Text => m_text;
        [SerializeField]
        private string m_text = null;

        /// <summary>
        /// Called when this Option was chosen by the player
        /// </summary>
        public UnityEvent OnOptionChosen;

        /// <summary>
        /// The destination of this option.
        /// </summary>
        public DialogueNode Destination
        {
            get
            {
                if(GetPort("m_destination").Connection != null)
                {
                    return GetPort("m_destination").Connection.node as DialogueNode;
                }

                return null;
            }
        }

        /// <summary>
        /// Used for initialization
        /// </summary>
        protected override void Init()
        {
            base.Init();
        }

        /// <summary>
        /// Returns the value of the given output port.
        /// </summary>
        public override object GetValue(NodePort port)
        {
            return this;
        }
    }
}