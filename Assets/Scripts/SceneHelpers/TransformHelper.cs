using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Transform))]
public class TransformHelper : MonoBehaviour 
{
    public List<Vector3> locations;

    public void SetLocation(int index)
    {
        if(index >= locations.Count) return;

        transform.position = locations[index];
    }
}