using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AiDogDetection))]
public class FOVEDogditor : Editor
{
    private void OnSceneGUI()
    {
        AiDogDetection FOV = (AiDogDetection)target;

        Handles.color = Color.white;
        Handles.DrawWireArc(FOV.transform.position, Vector3.up, Vector3.forward, 360, FOV.viewRadius);

        Vector3 viewAngleA = FOV.directionFromAngle(-FOV.viewAngle / 2, false);
        Vector3 viewAngleB = FOV.directionFromAngle(FOV.viewAngle / 2, false);

        Handles.DrawLine(FOV.transform.position, FOV.transform.position + viewAngleA * FOV.viewRadius);
        Handles.DrawLine(FOV.transform.position, FOV.transform.position + viewAngleB * FOV.viewRadius);
    }
}