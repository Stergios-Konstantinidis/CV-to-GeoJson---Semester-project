// (c) Stergios Konstantinidis 2023.

using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.Serialization;
using System.Text;
using System;
public class Geometry : MonoBehaviour
{
    //'properties': { 'id': '01dffe8d499158d5cdb4590ea6b0a9dc', 'type': null, 'color': null, 'base_height': null, 'height': null, 'level': null, 'name': null, 'connecting': null }
    //this needs to be sent
    [SerializeField, FormerlySerializedAs("type")]
    private String type;
    [SerializeField, FormerlySerializedAs("coordinates")]
    private String coordinates;



    private PreferencesSingleton prefs = PreferencesSingleton.GetInstance();

 
     public Geometry(List<Vector3> boundaries)
        {
            this.type = "Polygon";
            this.coordinates = GetGeometry(boundaries);
        }


    public string GetGeometry(List<Vector3> boundaries)
    {
        string toReturnString = "[ [" ;
        Vector3 firstEdge = boundaries[0];
        foreach(Vector3 position in boundaries)
        {
            toReturnString = toReturnString + "[" + position[0]+", " +position[2] +"], " ;
        }
        toReturnString = toReturnString + "[" + firstEdge[0]+", " +firstEdge[2] +"], " ; //first point needs to be the same as the last one
        return toReturnString.Substring(0,toReturnString.Length - 2) + "] ]";
    }

}