using UnityEngine;

namespace Commands.Weapons
{
    public class CmdBoomerang : ICommand
    {
        private readonly ISkills _skills;

        public CmdBoomerang(ISkills skills)
        {
            _skills = skills;
        }

        public void Execute()
        {
            _skills.throwBoomerang();
        }
    }
}
