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

    public void SendData()
    {
        MeshFilter[] amf = (MeshFilter[])Resources.FindObjectsOfTypeAll(typeof(MeshFilter));
        foreach (MeshFilter meshFilter in amf)
        {
            Mesh mesh = meshFilter.mesh;
            Vector3[] vertices = mesh.vertices;
            List<Vector3> points = new List<Vector3>();
            foreach (Vector3 item in vertices)
            {
                points.Add(item); 
            }
            GeojsonGenerator GeoJSONEntry = new GeojsonGenerator(points);
            GeoJSONEntry.GetGeoJson();
            string jsonStringTrial = JsonUtility.ToJson(GeoJSONEntry);
            UnityWebRequest www = UnityWebRequest.Put("https://webhook.site/f49e79b9-81f0-4168-ba01-28d3b7d00201", jsonStringTrial);
            www.SetRequestHeader("Content-Type", "application/json");
            www.Send();
        }
    }
}