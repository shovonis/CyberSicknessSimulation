using UnityEngine;
using UnityEditor;

namespace DWP2
{
    public class MessageWindow : EditorWindow
    {
        void OnGUI()
        {
            GUILayout.Space(20);
            EditorGUILayout.LabelField("First Time DWP2 Setup", EditorStyles.boldLabel);
            EditorGUILayout.TextArea("Before DWP2 can function propery two manual setup steps are needed. If you already" +
                " followed them just ignore this message.", EditorStyles.wordWrappedLabel);
            GUILayout.Space(20);
            EditorGUILayout.LabelField("Burst Compiler", EditorStyles.boldLabel);
            EditorGUILayout.TextArea("To be able to run DWP2 you will need to install 'Burst' package using Package Manager (Window => Package Manager)." +
                "If you get the 'Burst failed...' error on build check the following link: https://docs.unity3d.com/Packages/com.unity.burst@1.1/manual/index.html#burst-aot-requirements" +
                "Note that for Windows builds both Visual Studio and Visual Studio C++ Development Tools are required." +
                "Alternatively you can use 'DWP_DISABLE_BURST' to disable Burst alltogether."
                , EditorStyles.wordWrappedLabel);
            GUILayout.Space(20);
            EditorGUILayout.LabelField("Unsafe Code", EditorStyles.boldLabel);
            EditorGUILayout.TextArea("DWP2 requires unsafe code to be enabled to function. Unsafe code in DWP2 is used to memory copy between NativeArray and Array and vice versa." +
                "which causes massive performance improvements compared to iterative copying.." +
                "Despite the name it will not cause your application to be less safe, it just refers to native / unmanaged memory access." +
                "To enable this option go to Project Settings => Player => Allow 'unsafe' Code and tick the option."
                , EditorStyles.wordWrappedLabel);
            GUILayout.Space(20);
            EditorGUILayout.LabelField("Upgrading from DWP1", EditorStyles.boldLabel);
            EditorGUILayout.TextArea("If you are upgrading from Dynamic Water Physics check Upgrade Notes (DWP2 => Dynamic Water Physics 2 => Documentation)."
                , EditorStyles.wordWrappedLabel);
            GUILayout.Space(20);
            EditorGUILayout.TextArea("A copy of this message can also be found inside README.txt"
                , EditorStyles.wordWrappedLabel);
            GUILayout.Space(30);
            if (GUILayout.Button("Understood. Close this message.")) this.Close();
        }
    }
}
