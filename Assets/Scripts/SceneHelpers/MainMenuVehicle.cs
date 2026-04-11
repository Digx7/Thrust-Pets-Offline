using UnityEngine;

public class MainMenuVehicle : MonoBehaviour 
{
    [SerializeField] private float _transformationSpeed = 1f;
    [SerializeField] private Vector3 _transformationDirection = Vector3.forward;

    private void Update()
    {
        transform.Translate(_transformationDirection * _transformationSpeed * Time.deltaTime);
    }
    
}