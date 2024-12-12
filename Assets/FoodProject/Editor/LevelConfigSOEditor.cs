using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LevelConfigSO), true)]
public class LevelConfigSOEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Varsayılan ScriptableObject Inspector'ını çiz
        base.OnInspectorGUI();

        // Buton ekle
        LevelConfigSO myScriptableObject = (LevelConfigSO)target;
        if (GUILayout.Button("Generate Unique ID"))
        {
            myScriptableObject.GenerateID();

            // Değişiklikleri kaydet
            EditorUtility.SetDirty(myScriptableObject);
        }
    }
}
