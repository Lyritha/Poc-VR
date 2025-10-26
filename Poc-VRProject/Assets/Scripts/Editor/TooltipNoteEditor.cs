using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TooltipNote))]
public class TooltipNoteEditor : Editor
{
    private TooltipNote noteComponent;
    private GUIStyle boxStyle;

    void OnEnable()
    {
        noteComponent = (TooltipNote)target;

        boxStyle = new GUIStyle(EditorStyles.helpBox)
        {
            fontSize = 14,
            wordWrap = true,
            padding = new RectOffset(10, 10, 8, 8),
            alignment = TextAnchor.UpperLeft,
            richText = true
        };
    }

    public override void OnInspectorGUI()
    {
        // If locked, show the note as a read-only colored box
        if (noteComponent.locked)
        {
            // Apply a soft background tint
            Color originalColor = GUI.backgroundColor;
            GUI.backgroundColor = new Color(0f, 0f, 1f, 1f); // bluish tint

            EditorGUILayout.LabelField($"{noteComponent.note}", boxStyle);

            GUI.backgroundColor = originalColor;
        }
        else
        {
            // Editable text area with a subtle green tint
            Color originalColor = GUI.backgroundColor;
            GUI.backgroundColor = new Color(0.7f, 1f, 0.7f, 0.3f); // greenish tint

            noteComponent.note = EditorGUILayout.TextArea(noteComponent.note, GUILayout.MinHeight(60));

            GUI.backgroundColor = originalColor;
        }

        if (GUI.changed)
        {
            EditorUtility.SetDirty(noteComponent);
        }
    }

    // Context menu items
    [MenuItem("CONTEXT/TooltipNote/Lock Note")]
    private static void LockNote(MenuCommand command)
    {
        TooltipNote note = (TooltipNote)command.context;
        note.locked = true;
        EditorUtility.SetDirty(note);
    }

    [MenuItem("CONTEXT/TooltipNote/Unlock Note")]
    private static void UnlockNote(MenuCommand command)
    {
        TooltipNote note = (TooltipNote)command.context;
        note.locked = false;
        EditorUtility.SetDirty(note);
    }

    // Dynamic enabling/disabling
    [MenuItem("CONTEXT/TooltipNote/Lock Note", true)]
    private static bool ValidateLock(MenuCommand command)
    {
        return !((TooltipNote)command.context).locked;
    }

    [MenuItem("CONTEXT/TooltipNote/Unlock Note", true)]
    private static bool ValidateUnlock(MenuCommand command)
    {
        return ((TooltipNote)command.context).locked;
    }
}
