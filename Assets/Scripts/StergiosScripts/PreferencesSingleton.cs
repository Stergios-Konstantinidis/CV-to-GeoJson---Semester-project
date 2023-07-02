using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.ARFoundation;
using Unity.XR.CoreUtils;

public class PreferencesSingleton : MonoBehaviour
{
    public static PreferencesSingleton Instance { get; private set; }


    private void Awake() 
    { 
    // If there is an instance, and it's not me, delete myself.
    
    if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        } 
    }

    private List<Vector3> PointCloudPoints;
    public void AddPointCloudPoint(Vector3 PointCloudNewPoint)
    {
        this.PointCloudPoints.Add(PointCloudNewPoint);
    }
    public void SetPointCloudPoints(List<Vector3> NewPointCloudPoints)
    {
        this.PointCloudPoints = NewPointCloudPoints;
    }

    public string pointWebHookURL;
    public void SetPointWebHookURL(string URL)
    {
        this.pointWebHookURL = URL;
    }

    public string SurfaceWebHookURL;
    public void SetSurfaceWebHookURL(string URL)
    {
        this.SurfaceWebHookURL = URL;
        Debug.Log(this.SurfaceWebHookURL);
    }

}
