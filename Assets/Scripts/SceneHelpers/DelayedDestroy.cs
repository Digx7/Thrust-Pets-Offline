using UnityEngine;

public class DelayedDestroy : MonoBehaviour {
    [SerializeField] private float _delay = 1f;

    private void Start()
    {
        Destroy(gameObject, _delay);
    }
}