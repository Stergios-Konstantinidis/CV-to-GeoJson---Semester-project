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
    [SerializeField, FormerlySerializedAs("id")]
    private String id;

    [SerializeField, FormerlySerializedAs("color")]
    private String color;

    [SerializeField, FormerlySerializedAs("base_height")]
    private int base_height;

    [SerializeField, FormerlySerializedAs("height")]
    private int height;

    [SerializeField, FormerlySerializedAs("name")]
    private String name;

    //[SerializeField, FormerlySerializedAs("GeoJsonEntry")]
    //private String GeojsonString;


    private List<Vector3> edges;

    private PreferencesSingleton prefs = PreferencesSingleton.GetInstance();

    [SerializeField, FormerlySerializedAs("Geometry")]
    private Geometry geometry;


 
     public GeojsonGenerator(List<Vector3> boundaries)
        {
            this.id = GetTimeHash();
            this.color = null;
            this.base_height = 0;
            this.height = prefs.MaxHeight;
            this.name = null;
            this.edges = boundaries;
            this.geometry = new Geometry(this.edges);

        }


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




}

