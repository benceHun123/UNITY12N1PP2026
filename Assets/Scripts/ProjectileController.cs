using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField] float _missleSpeed = 15f;
    [SerializeField] float _aliveTime = 5f;
    Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();

        if (transform.localRotation.z > 0)
        {
            _rigidbody.AddForce(
                new Vector2(-1, 0) * _missleSpeed,
                ForceMode2D.Impulse);
        }
        else
        {
            _rigidbody.AddForce(
                new Vector2(1, 0) * _missleSpeed,
                ForceMode2D.Impulse);
        }

        Destroy(gameObject, _aliveTime);
    }
}
