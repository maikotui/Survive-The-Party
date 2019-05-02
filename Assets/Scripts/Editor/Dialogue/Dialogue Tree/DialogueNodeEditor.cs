using System.Collections.Generic;
using UnityEngine;
using STP.Dialogue;
using XNodeEditor;
using UnityEditor;
using System.Linq;

[CustomNodeEditor(typeof(DialogueNode))]
public class DialogueNodeEditor : NodeEditor
{
    /// <summary>
    /// Called to update the header in the GUI
    /// </summary>
    public override void OnHeaderGUI()
    {
        DialogueNode node = target as DialogueNode ;
        DialogueTree tree = node.graph as DialogueTree;

        GUI.color = Color.white;
        string title = target.name;

        if (tree.StartNode == node)
        {
            GUI.color = Color.green;
            title = "Start Node";
        }
        else if (tree.CurrentNode == node)
        {
            GUI.color = Color.red;
        }

        GUILayout.Label(title, NodeEditorResources.styles.nodeHeader, GUILayout.Height(30));

        GUI.color = Color.white;
    }

    /// <summary>
    /// Called to update the body in the GUI
    /// </summary>
    public override void OnBodyGUI()
    {
        DialogueNode node = target as DialogueNode;
        DialogueTree tree = node.graph as DialogueTree;

        // If the tree is a start node, don't draw the source. Otherwise, draw it.
        if(tree.StartNode == node)
        {
            DrawAllExcept(new string[] { "m_Script", "graph", "position", "ports", "m_isStartNode", "m_source" });
        }
        else
        {
            DrawAllExcept(new string[] { "m_Script", "graph", "position", "ports", "m_isStartNode" });
        }
    }

    /// <summary>
    /// Draws all serializable values except for those with the given names in "excludes"
    /// </summary>
    /// <param name="excludes"></param>
    private void DrawAllExcept(string[] excludes)
    {
        serializedObject.Update();
        portPositions = new Dictionary<XNode.NodePort, Vector2>();

        SerializedProperty iterator = serializedObject.GetIterator();
        bool enterChildren = true;
        EditorGUIUtility.labelWidth = 84;
        while (iterator.NextVisible(enterChildren))
        {
            enterChildren = false;
            if (excludes.Contains(iterator.name)) continue;
            NodeEditorGUILayout.PropertyField(iterator, true);
        }
        serializedObject.ApplyModifiedProperties();
    }
}
