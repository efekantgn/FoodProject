using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BaseUpgradeTierSO), true)]
public class BaseUpgradeTierSOEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Varsayılan ScriptableObject Inspector'ını çiz
        base.OnInspectorGUI();

        // Buton ekle
        BaseUpgradeTierSO myScriptableObject = (BaseUpgradeTierSO)target;
        if (GUILayout.Button("Generate Unique ID"))
        {
            myScriptableObject.GenerateID();

            // Değişiklikleri kaydet
            EditorUtility.SetDirty(myScriptableObject);
        }
    }
}