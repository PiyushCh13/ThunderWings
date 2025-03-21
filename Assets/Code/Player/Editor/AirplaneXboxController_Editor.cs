using System.Collections;
using System.Collections.Generic;
using Airplane.PlayerControls;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AirplaneXboxInputController))]
public class AirplaneXboxController_Editor : Editor
{
    #region Variables
    private AirplaneXboxInputController _xboxInputController;
    #endregion

    private void OnEnable()
    {
        _xboxInputController = (AirplaneXboxInputController)target;
    }

    // ReSharper restore Unity.ExpensiveCode
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        string debuginfo = "";
        debuginfo += "Pitch = " + _xboxInputController.Pitch + "\n";
        debuginfo += "Roll = " + _xboxInputController.Roll + "\n";
        debuginfo += "Yaw = " + _xboxInputController.Yaw + "\n";
        debuginfo += "Flaps = " + _xboxInputController.Flaps + "\n";
        debuginfo += "Throttle = " + _xboxInputController.Throttle + "\n";
        debuginfo += "Brake = " + _xboxInputController.Brake + "\n";
        
        GUILayout.Space(20);
        GUILayout.TextArea(debuginfo, GUILayout.Height(100));
        GUILayout.Space(20);
        
        Repaint();
    }
}
