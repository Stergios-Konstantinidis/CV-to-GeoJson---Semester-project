// (c) Stergios Konstantinidis 2023.

using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.Serialization;
using System.Text;
using System;
using System.Security.Cryptography;


public class GeojsonGenerator : MonoBehaviour
{
    //'properties': { 'id': '01dffe8d499158d5cdb4590ea6b0a9dc', 'type': null, 'color': null, 'base_height': null, 'height': null, 'level': null, 'name': null, 'connecting': null }
    //this needs to be sent
    private String id;

    private String color;

    private int base_height;

    private int height;

    private String name;

    [SerializeField, FormerlySerializedAs("GeoJsonEntry")]
    private String GeojsonString;


    private List<Vector3> edges;

    private PreferencesSingleton prefs = PreferencesSingleton.GetInstance();

    #region Monobehaviour Methods
 
     public GeojsonGenerator(List<Vector3> boundaries)
        {
            this.id = GetTimeHash();
            this.color = "null";
            this.base_height = 0;
            this.height = prefs.MaxHeight;
            this.name = "null";
            this.edges = boundaries;

        }

    #endregion // MonoBehaviour Methods


    #region Private Methods
    private string GetTimeHash()
    {
        var hash = new SHA1Managed().ComputeHash(Encoding.UTF8.GetBytes(System.DateTime.Now.ToString("hh.mm.ss.fff")));
        var sb = new StringBuilder(hash.Length * 2);
        foreach(byte b in hash)
        {
            // can be 'x2' if you want lowercase
            sb.Append(b.ToString("X2"));
        }
        return sb.ToString().Substring(0,32);
    }

    private void setColor(String couleur)
    {
        this.color = couleur;
    }

    private void setHeight(int heightOfObject)
    {
        if(this.base_height <= heightOfObject)
        {
            this.height = heightOfObject;
            this.base_height = heightOfObject;
            prefs.SubmitNewMaxHeight(this.height);
        }
    }

    #endregion // Private Methods

    #region Public Methods
    ///<summary> This function returns a GeoJson line for this surface. </summary>
    public String GetGeoJson()
    {
        this.GeojsonString = "{" + GetFeature() + ", " + GetProperties() + ", " + GetGeometry() + "}}";
        return this.GeojsonString;
        
    }
    public String GetFeature()
    {
        return @"""type"": ""Feature""";
    }

    public void SetName(String nameToGive)
    {
        this.name = nameToGive;
    }
    
    public String GetProperties()
    {
        String couleur = null;
        if (this.color != null)
        {
            couleur = this.color;
        }
        return @"""properties"": {""id"":"" " + this.id + @""", ""type"": null, ""color"": "" " + couleur + @" "", ""base_height"":" + this.base_height + @", ""height"":"""+this.height+@""", ""level"": 1, ""name"": """ + this.name +@""", ""connecting"": null}";
    }

    public string GetGeometry()
    {
        string toReturnString = @"""geometry"":{""type"": ""Polygon"", ""coordinates"": [ [" ;
        Vector3 firstEdge = this.edges[0];
        foreach(Vector3 position in this.edges)
        {
            toReturnString = toReturnString + "[" + position[0]+", " +position[2] +"], " ;
        }
        toReturnString = toReturnString + "[" + firstEdge[0]+", " +firstEdge[2] +"], " ; //first point needs to be the same as the last one
        return toReturnString.Substring(0,toReturnString.Length - 2) + "] ]";
    }


    #endregion //public methods
}

