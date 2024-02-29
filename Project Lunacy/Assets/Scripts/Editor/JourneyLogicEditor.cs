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

        //-----------------------GAMEPLAY EVENTS BUTTON-----------------------
        GUIContent gameplayEventsButton = new()
        {
            text = "Add Gameplay Events",
            tooltip = "This adds all gameplay JourneyEvent objects to the 'Active Events' list"
        };

        if (GUILayout.Button(gameplayEventsButton))
        {
            ClearActiveEvents();
            
            UnityEngine.Object[] allEvents = Resources.LoadAll("Events", typeof(JourneyEvent));
            UnityEngine.Object[] testEvents = Resources.LoadAll("Events/TestEvents", typeof(JourneyEvent));

            List<UnityEngine.Object> gameplayEvents = new();

            foreach (var journeyEvent in allEvents)
            {
                string path = AssetDatabase.GetAssetPath(journeyEvent);
                
                if (path != "Assets/Resources/Events/" + journeyEvent.name + ".asset") { continue; }

                gameplayEvents.Add(journeyEvent);
            }

            AddEventsOfType(gameplayEvents.ToArray());
        }

        //-----------------------TEST EVENTS BUTTON-----------------------
        GUIContent testEventsButton = new()
        {
            text = "Add Test Events",
            tooltip = "This adds all available Test JourneyEvents in the 'TestEvents' folder to the 'Active Events' list"
        };

        if (GUILayout.Button(testEventsButton))
        {
            ClearActiveEvents();

            UnityEngine.Object[] testEvents = Resources.LoadAll("Events/TestEvents", typeof(JourneyEvent));

            AddEventsOfType(testEvents);
        }

        //-----------------------EVERY EVENT BUTTON-----------------------
        GUIContent everyEventButton = new()
        {
            text = "Add Every Event",
            tooltip = "This adds all of the available JourneyEvent objects to the 'Active Events' list\""
        };

        if (GUILayout.Button(everyEventButton))
        {
            ClearActiveEvents();

            UnityEngine.Object[] everyEvent = Resources.LoadAll("Events", typeof(JourneyEvent));

            AddEventsOfType(everyEvent);
        }
    }

    private void ClearActiveEvents()
    {
        JourneyLogic logicComponent = (JourneyLogic)target;

        logicComponent.activeEvents.Clear();
    }
    
    private void AddEventsOfType(UnityEngine.Object[] folderObjects)
    {
        JourneyLogic logicComponent = (JourneyLogic)target;

        foreach (var journeyEvent in folderObjects)
        {
            JourneyEvent add = (JourneyEvent)journeyEvent;

            logicComponent.activeEvents.Add(add);
        }
    }
}
