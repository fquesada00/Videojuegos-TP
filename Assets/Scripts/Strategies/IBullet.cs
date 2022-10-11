using UnityEngine;
using Weapons;

namespace Strategies
{
    public interface IBullet 
    {
        float LifeTime { get; }
        float Speed { get; }
    
        IGun Owner { get; }
    
        // for collisions and gravity (physics)
        Rigidbody Rigidbody { get; }
    
        Collider Collider { get; }

        string CollisionTag { get; set; }

        // always forward
        void Travel();
    
        void OnTriggerEnter(Collider other);
    
        void SetOwner(IGun owner);
        void SetSpeed(float speed);
        void SetLifeTime(float lifeTime);


    }
}