using UnityEngine;

public class LimitPointLightShadows : MonoBehaviour
{
    [Tooltip("Maximum number of point lights allowed to cast shadows")]
    public int maxShadowedPointLights = 4;

    void Start()
    {
        Light[] allLights = FindObjectsOfType<Light>();
        int shadowCount = 0;

        foreach (var light in allLights)
        {
            if (light.type == LightType.Point)
            {
                if (shadowCount < maxShadowedPointLights)
                {
                    light.shadows = LightShadows.Soft; 
                    shadowCount++;
                }
                else
                {
                    light.shadows = LightShadows.None;
                }
            }
        }
    }
}
