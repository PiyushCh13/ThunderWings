using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(fileName = "Terrain Settings" , menuName = "ScriptableObjects/TerrainSettings")]
internal class TerrainSettingScriptableObject : ScriptableObject
{
    public float pixelError;
    public bool drawInstanced;
    public bool rayTracing;
    public int baseMapDistance;
    public ShadowCastingMode castShadows;
    public ReflectionProbeUsage probeMode;
    public int treeDistance;
    public int baseMapResolution;
    public int resolutionPerPatch;
    public int detailResolution;
    public int detailDistance;
    public bool drawDetails;
    public bool ignoreQualitySettings;
    public int billboardstartLength;
}
