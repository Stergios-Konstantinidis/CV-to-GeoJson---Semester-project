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
                Debug.Log(item);
            }
            GeojsonGenerator GeoJSONEntry = new GeojsonGenerator(points);
            string jsonStringTrial = JsonUtility.ToJson(GeoJSONEntry);
            UnityWebRequest www = UnityWebRequest.Put(config.SurfaceWebHookURL, jsonStringTrial);
            www.SetRequestHeader("Content-Type", "application/json");
            www.Send();
        }
    }
}