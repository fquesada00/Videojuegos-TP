namespace Strategies
{
    public interface IAnimatable
    {
      
        void Attack();
    
        void setGrounded(bool grounded);

        void SetWeapon(int index, Weapon weapon);
        
        void Run(float speed);
        
    }
}