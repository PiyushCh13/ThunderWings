
using Airplane.Physics;
using UnityEditor;
using UnityEngine;

public static class AirplaneMenu
{
  [MenuItem("Airplane Tools/ Create new Airplane")]
  public static void CreatePlane()
  {
    GameObject obj = Selection.activeGameObject;
    if (obj)
    {
      AirplaneController airplaneController = obj.AddComponent<AirplaneController>();
      GameObject centreOfGravity = new GameObject("Centre Of Gravity");
      centreOfGravity.transform.SetParent(obj.transform);

      airplaneController.centerOfGravity = centreOfGravity.transform;
    }
    
  }
}
