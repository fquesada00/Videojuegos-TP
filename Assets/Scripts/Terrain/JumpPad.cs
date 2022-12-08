using UnityEngine;

public class JumpPad : MonoBehaviour
{
    const float _horizontalSpeed = 30;
    Transform _target;
    Vector3 _initialSpeed;
    float _jumpTotalTime, _jumpTime;
    bool _isJumping = false;
    GameObject _jumper;

    void Start()
    {
        _target = transform.Find("Target");
        Vector3 direction = _target.position - transform.position;
        float range = Vector3.Distance(transform.position, _target.position);
        _jumpTotalTime = range / _horizontalSpeed;
        float deltaY = _target.position.y - transform.position.y;
        float _ySpeed = (deltaY - 0.5f * Physics.gravity.y * Mathf.Pow(_jumpTotalTime, 2) ) / _jumpTotalTime;
        
        _initialSpeed = _ySpeed * Vector3.up + _horizontalSpeed * direction.normalized;
    }

    void Update()
    {
        if (_isJumping) {

            _initialSpeed += Physics.gravity * Time.deltaTime;
            _jumper.transform.position += _initialSpeed * Time.deltaTime;
            _jumpTime += Time.deltaTime;

            if(_jumpTime >= _jumpTotalTime - 0.4f) {
                _isJumping = false;
                // FIXME: should be on Queue
                _jumper.GetComponent<MovementController>().enabled = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            _isJumping = true;
            _jumper = other.gameObject;
            _jumper.GetComponent<MovementController>().enabled = false;
            _jumpTime = 0;
        }
    }
}
