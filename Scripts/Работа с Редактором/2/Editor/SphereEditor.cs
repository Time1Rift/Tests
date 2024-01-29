using UnityEditor;
using UnityEngine;
using System.Reflection;

[CustomEditor(typeof(Sphere))]
public class SphereEditor : Editor
{
    private const string RadiusField = "_radius";
    private const string SliderValueLabel = "Radius";
    private const float MinSliderValue = 0.01f;
    private const float MaxSliderValue = 5;
    private const string ResetButtonText = "Reset";

    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();

        Sphere sphere = (Sphere)target;

        GUILayout.BeginHorizontal();

        UpdateSphereRadius(sphere);
        DrawResetButton(sphere);

        GUILayout.EndHorizontal();
    }

    private static void UpdateSphereRadius(Sphere sphere)
    {
        FieldInfo radiusField = sphere.GetType().GetField(RadiusField, BindingFlags.Instance | BindingFlags.NonPublic);
        float radius = (float)radiusField.GetValue(sphere);

        radius = EditorGUILayout.Slider(SliderValueLabel, radius, MinSliderValue, MaxSliderValue);

        radiusField.SetValue(sphere, radius);

        sphere.transform.localScale = Vector3.one * radius;
    }

    private static void DrawResetButton(Sphere sphere)
    {
        if(GUILayout.Button(ResetButtonText))
            sphere.ResetRadius();
    }
}