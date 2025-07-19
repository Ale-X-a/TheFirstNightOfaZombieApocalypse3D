using UnityEngine;
using UnityEditor;

public class LightShadowOptimizer : MonoBehaviour
{
    [MenuItem("Tools/Optimize Light Shadows")]
    static void OptimizeLightShadows()
    {
        Light[] allLights = FindObjectsByType<Light>(FindObjectsSortMode.None);

        foreach (Light light in allLights)
        {
            if (light.type == LightType.Point || light.type == LightType.Spot)
            {
                light.shadows = LightShadows.None; // Or LightShadows.Hard
                Debug.Log($"Disabled shadows for: {light.name}");
            }
        }
        Debug.Log("✅ Optimization complete.");
    }
}