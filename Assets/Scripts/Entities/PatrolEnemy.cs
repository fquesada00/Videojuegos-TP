using UnityEngine;

namespace Entities
{
    public class PatrolEnemy : MonoBehaviour
    {

        public Transform[] waypoints;
        private int _currentWaypointIndex = 0;
        private float _speed = 2f;

        private void Update()
        {
            Transform wp = waypoints[_currentWaypointIndex];
            if (Vector3.Distance(transform.position, wp.position) < 0.01f)
            {
                _currentWaypointIndex = (_currentWaypointIndex + 1) % waypoints.Length;
            }
            else
            {
                var position = wp.position;
                transform.position = Vector3.MoveTowards(
                    transform.position,
                    position,
                    _speed * Time.deltaTime);
                transform.LookAt(position);
            }
        }

    }
}