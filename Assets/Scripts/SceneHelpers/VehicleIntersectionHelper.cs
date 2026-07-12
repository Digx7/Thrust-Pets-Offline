using UnityEngine;

public class VehicleIntersectionHelper : MonoBehaviour 
{
    
    public Vector3 DirectionOfTravel;
    public float speed;
    public float maxDistance;
    public float resetDistance;
    
    public Transform _transform;
    public VehicleObstacleHelper vehicleObstacleHelper;

    private void Awake() 
    {
        _transform = GetComponent<Transform>();
    }

    private void Start()
    {
        vehicleObstacleHelper.Place();
    }

    private void Update() 
    {
        _transform.position += DirectionOfTravel * speed * Time.deltaTime;

        if (DirectionOfTravel.x > 0f && _transform.position.x > maxDistance)
        {
            _transform.position = new Vector3(-resetDistance, _transform.position.y, _transform.position.z);
            vehicleObstacleHelper.Place();
        }
        else if (DirectionOfTravel.x < 0f && _transform.position.x < -maxDistance)
        {
            _transform.position = new Vector3(resetDistance, _transform.position.y, _transform.position.z);
            vehicleObstacleHelper.Place();
        }
    }
}