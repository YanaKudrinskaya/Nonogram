using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(NonogramsEditor))]
public class NonogramsEditorGUI : Editor
{

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        NonogramsEditor gridNanogramm = (NonogramsEditor)target;
        if(GUILayout.Button("Create Grid"))
        {
           gridNanogramm.CreateEmptyGrid();
        }
        if(GUILayout.Button("Add New nono in Json"))
        {
            gridNanogramm.AddNewNonogram();
        }
        /*if (GUILayout.Button("Load Nonogram"))
        {
            gridNanogramm.LoadNonograms();
        }*/
        /*if (GUILayout.Button("LoadInJsonToList"))
        {
            gridNanogramm.LoadInJsonToList();
        }*/
    }
}
