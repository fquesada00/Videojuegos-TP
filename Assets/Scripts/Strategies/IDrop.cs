using UnityEngine;

namespace Strategies
{
    public interface IDrop
    {
        Rigidbody Rigidbody { get; }
    
        Collider Collider { get; }
        
        public void Take(Entity entity);
    }
}