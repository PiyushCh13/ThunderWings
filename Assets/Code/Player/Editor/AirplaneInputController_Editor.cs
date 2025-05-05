using Airplane.PlayerControls;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AirplaneInputController))]
public class AirplaneInputController_Editor : Editor
{
    #region Variables
    private AirplaneInputController _inputController;
    #endregion

    private void OnEnable()
    {
        _inputController = (AirplaneInputController)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        string debuginfo = "";
        debuginfo += "Pitch = " + _inputController.Pitch + "\n";
        debuginfo += "Roll = " + _inputController.Roll + "\n";
        debuginfo += "Yaw = " + _inputController.Yaw + "\n";
        debuginfo += "Flaps = " + _inputController.Flaps + "\n";
        debuginfo += "Throttle = " + _inputController.Throttle + "\n";
        debuginfo += "Sticky Throttle = " + _inputController.StickyThrottle + "\n";
        debuginfo += "Brake = " + _inputController.Brake + "\n";
        
        GUILayout.Space(20);
        GUILayout.TextArea(debuginfo, GUILayout.Height(100));
        GUILayout.Space(20);
        
        Repaint();
    }
}
