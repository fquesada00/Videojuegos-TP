namespace Strategies
{
    public interface IAnimatable
    {
        bool IsRunning { get; }
        bool IsJumping { get; }
        bool IsDashing { get; }
        bool IsIdle { get; }
        bool IsAttacking { get; }
        
        void StartJump();
        void StartDash();
        void StartRun();
        void Idle();
        void StartAttack();
        void StopJump();
        void StopDash();
        void StopRun();
        void StopAttack();
    }
}