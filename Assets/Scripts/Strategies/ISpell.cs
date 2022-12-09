namespace Strategies
{
    public interface ISpell
    {
        float Damage { get; set; }

        bool Crit { get; set; }
        
        float Range { get; set; }
        
        int Duration { get; set; }

        void Activate();
    }
}