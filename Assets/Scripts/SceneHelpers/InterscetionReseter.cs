using UnityEngine;

public class InterscetionReseter : MonoBehaviour 
{
    public Vector3 ResetVector;
    public float distanceToReset;
    
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("VehicleIntersectionHelper OnTriggerEnter Intersection Reset Triggered by: " + other.name);
        
        VehicleIntersectionHelper vehicle = other.transform.GetComponentInParent<VehicleIntersectionHelper>();

        VehicleObstacleHelper vehicleObstacle = other.transform.GetComponentInParent<VehicleObstacleHelper>();

        if (vehicle != null)
        {
            vehicle.transform.position += ResetVector;
            Debug.Log("VehicleIntersectionHelper found and reset position for: " + other.name);
        }
        else
        {
            Debug.LogWarning("VehicleIntersectionHelper NOT found on the colliding object: " + other.name);
        }

        if (vehicleObstacle != null)
        {
            vehicleObstacle.Place();
            Debug.Log("VehicleObstacleHelper found and placed new mesh for: " + other.name);
        }
        else
        {
            Debug.LogWarning("VehicleObstacleHelper NOT found on the colliding object: " + other.name);
        }
    }
}