using Controllers;
using UnityEngine;

namespace Strategies
{
    public interface IPotion
    {
        Rigidbody Rigidbody { get; }
    
        Collider Collider { get; }
        public void Take(IDamageable damageable);
    }
}