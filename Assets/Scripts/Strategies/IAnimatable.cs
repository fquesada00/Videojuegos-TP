namespace Strategies
{
    public interface IAnimatable
    {
        bool IsRunning { get; }
        bool IsDashing { get; }
        bool IsIdle { get; }
        bool IsAttacking { get; }
        bool IsJumping {get;}
        void Jump();
        void StartDash();
        void StartRun();
        void Idle();
        void Attack();
        void StopDash();
        void StopRun();
        void Land();
        
        void Run(float speed);
        
        float GetCurrentAnimationLength();
    }
}