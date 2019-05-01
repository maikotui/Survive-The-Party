using STP.Info;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using XNode;

namespace STP.Dialogue
{
    [CreateNodeMenu("Dialogue Node")]
    public class DialogueNode : Node
    {
        /// <summary>
        /// The DialogueOption that this Node was accessed from. Null if this node is a start node for it's graph (DialogueTree)
        /// </summary>
        [SerializeField, Input(connectionType = ConnectionType.Override, typeConstraint = TypeConstraint.Strict)]
        private DialogueOption m_source;

        /// <summary>
        /// The person who is speaking the given text
        /// </summary>
        public CharInfo Speaker => m_speaker;
        [SerializeField]
        private CharInfo m_speaker = CharInfo.Empty;

        /// <summary>
        /// The text to be spoken to the player
        /// </summary>
        public string Text => m_text;
        [SerializeField, TextArea(3, 10)]
        private string m_text = null;

        // The option ports for the DialogueTree graph
        [SerializeField, Output(connectionType = ConnectionType.Override)]
        private DialogueNode m_option1;
        [SerializeField, Output(connectionType = ConnectionType.Override)]
        private DialogueNode m_option2;
        [SerializeField, Output(connectionType = ConnectionType.Override)]
        private DialogueNode m_option3;
        [SerializeField, Output(connectionType = ConnectionType.Override)]
        private DialogueNode m_option4;

        /// <summary>
        /// A list of all options this node has
        /// </summary>
        public ReadOnlyCollection<DialogueOption> Options { get; private set; }

        /// <summary>
        /// Used for initialization
        /// </summary>
        protected override void Init()
        {
            base.Init();

            Options = new ReadOnlyCollection<DialogueOption>(GetDialogueOptions());
        }

        /// <summary>
        /// Gets all the option ports, then follows the connection (if one exists) to the DialogueOption node it is connected to.
        /// That option node is then added to a list of DialogueOptions that is returned.
        /// </summary>
        private IList<DialogueOption> GetDialogueOptions()
        {
            List<DialogueOption> options = new List<DialogueOption>();
            string[] portnames = new string[] { "m_option1", "m_option2", "m_option3", "m_option4" };

            foreach (string portname in portnames)
            {
                if (GetPort("m_option1").Connection != null)
                {
                    options.Add(GetPort("m_option1").Connection.node as DialogueOption);
                }
            }

            return options;
        }

        /// <summary>
        /// Sets this node as the start node in its graph
        /// </summary>
        [ContextMenu("Set As Start Node")]
        private void SetAsStartNode()
        {
            GetPort("m_source").ClearConnections();

            DialogueTree tree = graph as DialogueTree;
            tree.SetStartNode(this);
        }

        /// <summary>
        /// Returns the correct value for the given output port.
        /// </summary>
        public override object GetValue(NodePort port)
        {
            return this;
        }
    }
}