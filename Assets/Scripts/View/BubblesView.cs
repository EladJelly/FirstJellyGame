using Assets.Scripts.Controllers;
using UnityEngine;

public class BubblesView : MonoBehaviour
{
    private ParticleSystem _particles;

    void Awake()
    {
        _particles = GetComponent<ParticleSystem>();
    }

    void OnEnable()
    {
        GameEventsController.ClickAreaEvent += ActivateBubbles;
    }

    void OnDisable()
    {
        GameEventsController.ClickAreaEvent -= ActivateBubbles;
    }

    private void ActivateBubbles(Vector3 point)
    {
        transform.position = new Vector3(point.x, transform.position.y, transform.position.z);
        _particles.Play(true);
    }
}
