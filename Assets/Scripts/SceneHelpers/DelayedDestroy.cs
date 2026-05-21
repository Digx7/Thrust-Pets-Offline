using UnityEngine;

public class DelayedDestroy : MonoBehaviour {
    [SerializeField] private float _delay = 1f;
    public float Delay { get => _delay; set => _delay = value; }

    private void Start()
    {
        Destroy(gameObject, _delay);
    }
}