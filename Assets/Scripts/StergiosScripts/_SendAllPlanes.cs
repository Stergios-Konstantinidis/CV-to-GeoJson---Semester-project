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
        //allPlanes = List<GameObject> planes = UnityEngine.Object.FindObjectsOfType<GameObject>();
        foreach (ARPlane plane in allPlanes)
        {
            List<Vector3> plane3d = new List<Vector3>();
            foreach (var plane2d in plane.boundary)
            {
                plane3d.Add(new Vector3(plane2d[0], 0,plane2d[1]));
            }
            GeojsonGenerator jsonGenerator = new GeojsonGenerator(plane3d);
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(jsonGenerator.GetGeoJson());
            UnityWebRequest www = UnityWebRequest.Put(config.SurfaceWebHookURL, bytes);
            www.SetRequestHeader("Content-Type", "application/json");
            www.SendWebRequest();
        }
    }
}