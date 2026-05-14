using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class MeshBoundsHelper : MonoBehaviour 
{
    public float boundsSizeMultiplier = 100000f;
    public bool runsOnStart = true;
    public bool runsOnUpdate = false;
    
    private Mesh _mesh;

    void Awake()
    {
        _mesh = GetComponent<MeshFilter>().mesh;
    }

    void Start() 
    {
        if(runsOnStart)
        {
            UpdateBounds();
        }
    }

    void Update()
    {
        if(runsOnUpdate)
        {
            UpdateBounds();
        }
    }

    void UpdateBounds()
    {
        Bounds bounds = _mesh.bounds;
        bounds.size *= boundsSizeMultiplier;


        _mesh.bounds = bounds;
    }
}