using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FoodSO), true)]
public class FoodSOEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Varsayılan ScriptableObject Inspector'ını çiz
        base.OnInspectorGUI();

        // Buton ekle
        FoodSO myScriptableObject = (FoodSO)target;
        if (GUILayout.Button("Generate Unique ID"))
        {
            myScriptableObject.GenerateID();

            // Değişiklikleri kaydet
            EditorUtility.SetDirty(myScriptableObject);
        }
    }
}
