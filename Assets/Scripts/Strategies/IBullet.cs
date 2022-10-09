using UnityEngine;
using Weapons;

namespace Strategies
{
    public interface IBullet 
    {
        float LifeTime { get; }
        float Speed { get; }
    
        Gun Owner { get; }
    
        // for collisions and gravity (physics)
        Rigidbody Rigidbody { get; }
    
        Collider Collider { get; }

        // always forward
        void Travel();
    
        void OnTriggerEnter(Collider other);
    
        void SetOwner(Gun owner);
        void SetSpeed(float speed);
        void SetLifeTime(float lifeTime);
    }
}