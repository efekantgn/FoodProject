using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FoodIngridientSO), true)]
public class FoodIngridientSOEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Varsayılan ScriptableObject Inspector'ını çiz
        base.OnInspectorGUI();

        // Buton ekle
        FoodIngridientSO myScriptableObject = (FoodIngridientSO)target;
        if (GUILayout.Button("Generate Unique ID"))
        {
            myScriptableObject.GenerateID();

            // Değişiklikleri kaydet
            EditorUtility.SetDirty(myScriptableObject);
        }
    }
}
