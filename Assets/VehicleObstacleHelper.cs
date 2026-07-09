using UnityEngine;
using System.Collections.Generic;

public class VehicleObstacleHelper : MonoBehaviour, IObstacle
{
    
    public List<Mesh> vehicleMeshes; // List of vehicle meshes to choose from
    [SerializeField] private MeshFilter meshFilter;
    
    public void Place()
    {
        if (meshFilter == null)
        {
            Debug.LogWarning("MeshFilter is not assigned in VehicleObstacleHelper." + gameObject.name);
            
            return;
        }
        
        if (vehicleMeshes.Count > 0)
        {
            // Randomly select a mesh from the list
            int randomIndex = Random.Range(0, vehicleMeshes.Count);
            meshFilter.mesh = vehicleMeshes[randomIndex];
        }
        else
        {
            Debug.LogWarning("No vehicle meshes assigned to VehicleObstacleHelper.");
        }
    }
}