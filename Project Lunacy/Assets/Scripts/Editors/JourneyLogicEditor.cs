using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(JourneyLogic))]
public class JourneyLogicEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        GUIContent buttonCon = new()
        {
            text = "Add All Events",
            tooltip = "This adds all of the available JourneyEvent objects to the list"
        };

        if (GUILayout.Button(buttonCon))
        {
            AddAllEventsFunc();
        }
    }

    private void AddAllEventsFunc()
    {
        JourneyLogic logicComponent = (JourneyLogic)target;
        
        logicComponent.activeEvents.Clear();
        
        Object[] folderObjects = Resources.LoadAll("Events", typeof(JourneyEvent));

        foreach (var journeyEvent in folderObjects)
        {
            JourneyEvent add = (JourneyEvent)journeyEvent;

            logicComponent.activeEvents.Add(add);
        }
    }
}
