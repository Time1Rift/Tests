using UnityEditor;
using UnityEngine;

public class ColorizerWindow : EditorWindow
{
    private const string Color = "Color";
    private const string ButtonColorize = "Colorizer";

    private Color _color = UnityEngine.Color.white;

    private void OnGUI()
    {
        _color = EditorGUILayout.ColorField(Color, _color);

        if (GUILayout.Button(ButtonColorize))
            Colorizer();
    }

    [MenuItem("Window/Colorize")]
    private static void ShowWindow()
    {
        GetWindow<ColorizerWindow>("Colorize");
    }

    private void Colorizer()
    {
        foreach (GameObject gameObject in Selection.gameObjects)
        {
            Renderer renderer = gameObject.GetComponent<Renderer>();

            if (renderer != null)
            {
                Material material = new(renderer.sharedMaterial)
                {
                    color = _color
                };

                renderer.sharedMaterial = material;
            }
        }
    }
}