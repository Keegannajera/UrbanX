using System;
using System.Collections.Generic;
using UnityEngine; 

[System.Serializable]
public struct SerializableVector3
{
    public float x;
    public float y;
    public float z;

    public SerializableVector3(Vector3 v)
    {
        x = v.x;
        y = v.y;
        z = v.z;
    }

    public Vector3 ToVector3()
    {
        return new Vector3(x, y, z);
    }
}


[System.Serializable]
public struct SerializableQuaternion 
{
    public float x;
    public float y;
    public float z;
    public float w;

    public SerializableQuaternion(Quaternion q)
    {
        x = q.x;
        y = q.y;
        z = q.z;
        w = q.w;
    }

    public Quaternion ToQuaternion()
    {
        return new Quaternion(x, y, z, w);
    }
}


[System.Serializable]
public class ComponentState 
{
    public SerializableVector3 position;
    public SerializableQuaternion rotation;
    public bool isActive;
    public int customValue;
}