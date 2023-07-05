using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using UnityEngine.XR.ARFoundation;
using UnityEngine.Networking;
using System.IO;


public class _SendAllPlanes : MonoBehaviour
{
    public ARPlaneManager planeManager;
    private List<ARPlane> allPlanes;
    private PreferencesSingleton config = PreferencesSingleton.GetInstance();

    public void UpdatePlanesList()
    {
        planeManager.GetAllPlanes(allPlanes);
        foreach (ARPlane plane in allPlanes)
        {
            List<Vector3> edgePoints = new List<Vector3>();
            if (plane.TryGetBoundary(edgePoints, Space.Self))
            {
                GeojsonGenerator jsonGenerator = new GeojsonGenerator(edgePoints);
                byte[] bytes = System.Text.Encoding.UTF8.GetBytes(jsonGenerator.GetGeoJson());
                UnityWebRequest www = UnityWebRequest.Put(config.SurfaceWebHookURL, bytes);
                www.SetRequestHeader("Content-Type", "application/json");
                www.SendWebRequest();
            }
        }
    }
}