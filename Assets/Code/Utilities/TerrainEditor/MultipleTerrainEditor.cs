using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MultipleTerrainEditor : MonoBehaviour
{
    public Terrain[] terrains = new Terrain[0];
    [SerializeField] private TerrainSettingScriptableObject scriptableObject;
    public void AssignActiveTerrains()
    {
        SetTargetTerrains(Terrain.activeTerrains);
    }

    public void SetTargetTerrains(Terrain[] terrains)
    {
        this.terrains = terrains;
    }

    public void ChangeSettings() 
    {
        foreach (Terrain terrain in terrains) 
        {
            terrain.heightmapPixelError = scriptableObject.pixelError;
            terrain.enableHeightmapRayTracing = scriptableObject.rayTracing;
            terrain.drawInstanced = scriptableObject.drawInstanced;
            terrain.drawTreesAndFoliage = scriptableObject.drawDetails;
            terrain.basemapDistance = scriptableObject.baseMapDistance;
            terrain.shadowCastingMode = scriptableObject.castShadows;
            terrain.reflectionProbeUsage = scriptableObject.probeMode;
            terrain.treeDistance = scriptableObject.treeDistance;
            terrain.terrainData.baseMapResolution = scriptableObject.baseMapResolution;
            terrain.terrainData.SetDetailResolution(scriptableObject.detailResolution, scriptableObject.resolutionPerPatch);
            terrain.terrainData.wavingGrassAmount = 0;
            terrain.terrainData.wavingGrassSpeed = 0;
            terrain.terrainData.wavingGrassStrength = 0;
            terrain.detailObjectDensity = 0.5f;
            terrain.detailObjectDistance = scriptableObject.detailDistance;
            terrain.terrainData.enableHolesTextureCompression = false;
            terrain.ignoreQualitySettings = scriptableObject.ignoreQualitySettings;
            terrain.treeBillboardDistance = scriptableObject.billboardstartLength;

        }
    }

}
