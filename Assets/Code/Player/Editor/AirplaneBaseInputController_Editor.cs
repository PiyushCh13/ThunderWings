using Airplane.PlayerControls;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AirplaneBaseInputController))]
public class AirplaneBaseInputController_Editor : Editor
{
    #region Variables
    private AirplaneBaseInputController _baseInputController;
    #endregion

    private void OnEnable()
    {
        _baseInputController = (AirplaneBaseInputController)target;
    }

    // ReSharper restore Unity.ExpensiveCode
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        string debuginfo = "";
        debuginfo += "Pitch = " + _baseInputController.Pitch + "\n";
        debuginfo += "Roll = " + _baseInputController.Roll + "\n";
        debuginfo += "Yaw = " + _baseInputController.Yaw + "\n";
        debuginfo += "Flaps = " + _baseInputController.Flaps + "\n";
        debuginfo += "Throttle = " + _baseInputController.Throttle + "\n";
        debuginfo += "Brake = " + _baseInputController.Brake + "\n";
        
        GUILayout.Space(20);
        GUILayout.TextArea(debuginfo, GUILayout.Height(100));
        GUILayout.Space(20);
        
        Repaint();
    }
}
