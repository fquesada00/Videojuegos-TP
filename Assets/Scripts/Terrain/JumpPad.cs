using UnityEngine;

public class JumpPad : MonoBehaviour
{
    const float _horizontalSpeed = 30;
    Transform _target;
    Transform _origin;
    Vector3 _initialSpeed;
    Vector3 _speed;
    float _jumpTotalTime, _jumpTime;
    bool _isJumping = false;
    GameObject _jumper;

    void Start()
    {
        _target = transform.Find("Target");
        _origin = transform.Find("Origin");
        float deltaY = _target.position.y - transform.position.y;
        Vector3 direction = _target.position - transform.position - deltaY * Vector3.up;
        float range = Vector3.Distance(transform.position, _target.position - deltaY * Vector3.up); // delta horizontal
        _jumpTotalTime = range / _horizontalSpeed;
        float _ySpeed = (deltaY - 0.5f * Physics.gravity.y * Mathf.Pow(_jumpTotalTime, 2) ) / _jumpTotalTime;
        _initialSpeed = _ySpeed * Vector3.up + _horizontalSpeed * direction.normalized;

        //align up direction with initial speed direction
        _origin.rotation = Quaternion.LookRotation(_initialSpeed);

    }

    void LateUpdate()
    {
        if (_isJumping) {
            float dt = Time.time - _jumpTime;
            _jumper.transform.position = _origin.position + _initialSpeed * (float)dt + 0.5f * Physics.gravity * (float)Mathf.Pow(dt, 2);

            if(dt >= _jumpTotalTime) {    
                _isJumping = false;
            }
        }
    }

    public void levelTarget()
    {
        Transform _target = transform.Find("Target");
        float targetHeight = Terrain.activeTerrain.SampleHeight(_target.position);
        _target.position = new Vector3(_target.position.x, targetHeight, _target.position.z);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            _isJumping = true;
            _jumper = other.gameObject;
            _jumpTime = Time.time;
            _speed = _initialSpeed;
        }
    }
}
