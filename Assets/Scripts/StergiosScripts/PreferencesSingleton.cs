using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.ARFoundation;
using Unity.XR.CoreUtils;

public class PreferencesSingleton : MonoBehaviour
{
    public static PreferencesSingleton Instance { get; private set; }


    public static PreferencesSingleton GetInstance()
        {
            if (Instance == null)
            {
                Instance = new PreferencesSingleton();
            }
            return Instance;
        }

    private bool accountForBuildingCoordinates;
    ///<summary> This function determines if we should send the points and surfaces in a cartesian way or not.</summary>
    /// <param name="values">Is a boolean, it is either equal to true or false.</param>
    public void setAccountForBuildingCoordinates(bool values)
    {
        this.accountForBuildingCoordinates = values;
    }

    private Vector2 CoordinatesOfBuilding;
    ///<summary> update the coordonates automatically from the GPS position </summary>
    private void updateCoordinatesAuto()
    {
        this.CoordinatesOfBuilding = new Vector2(UnityEngine.Input.location.lastData.latitude, UnityEngine.Input.location.lastData.longitude);
    }
    ///<summary> update the coordonates manually form the users input </summary>
    private void updateCoordinatesMano(float lat, float lon)
    {
        this.CoordinatesOfBuilding = new Vector2(lat, lon);
    }


    private List<Vector3> PointCloudPoints;
    ///<summary> This function adds a point in our point cloud. 
    ///It is usefull while the point cloud is being generated to get a complete view</summary>
    /// <param name="PointCloudNewPoint">The new point to be added. </param>
    public void AddPointCloudPoint(Vector3 PointCloudNewPoint)
    {
        this.PointCloudPoints.Add(PointCloudNewPoint);
    }


    ///<summary> This function replaces our point cloud. 
    ///It is used if the user want to filter the point cloud and remove unwanted values.</summary>
    /// <param name="NewPointCloudPoints">The new point cloud, is a list of all points. </param>
    public void SetPointCloudPoints(List<Vector3> NewPointCloudPoints)
    {
        this.PointCloudPoints = NewPointCloudPoints;
    }

    public string pointWebHookURL;
    ///<summary> This function sets the URL that will receive all point coordinates in Vector 3 format.</summary>
    /// <param name="URL">The URL able to support a webhook.</param>
    public void SetPointWebHookURL(string URL)
    {
        this.pointWebHookURL = URL;
    }

    public string SurfaceWebHookURL;
    ///<summary> This function sets the URL that will receive all surface coordinates in Vector 3 format.</summary>
    /// <param name="URL">The URL able to support a webhook.</param>
    public void SetSurfaceWebHookURL(string URL)
    {
        this.SurfaceWebHookURL = URL;
    }

    public int MaxHeight;
    ///<summary> This function ensures that the highest point detected is stored properly.
    /// When a new value is submitted, if it is higher than tha current highest value then this value is set as max height</summary>
    /// <param name="HeightToTest">The height to check if higher than the current height.</param>
    public void SubmitNewMaxHeight(int HeightToTest)
    {
        if(HeightToTest >= this.MaxHeight)
        {
            this.MaxHeight = HeightToTest;
        }
    }

}
