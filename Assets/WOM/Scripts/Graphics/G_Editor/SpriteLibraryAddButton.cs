using UnityEngine;
using UnityEditor;
using ProjectGraphics;

namespace ProjectGraphics
{
    [CustomEditor(typeof(SpriteLibraryAdd))]
    public class SpriteLibraryAddButton : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            SpriteLibraryAdd generator = (SpriteLibraryAdd)target;

            if (GUILayout.Button("Generate Library"))
            {
                generator.GenerateLibrary();
            }

            if (GUILayout.Button("Clear Library"))
            {
                generator.ClearLibrary();
            }
        }
    }
}
