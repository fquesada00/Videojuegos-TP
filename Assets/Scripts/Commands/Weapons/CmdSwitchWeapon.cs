namespace Commands.Weapons
{
    public class CmdSwitchWeapon : ICommand
    {
        private IAttackable _attackable;
        private int _weaponIndex;
        
        public CmdSwitchWeapon(IAttackable attackable, int weaponIndex)
        {
            _attackable = attackable;
            _weaponIndex = weaponIndex;
        }

        public void Execute()
        {
            _attackable.SwitchWeapon(_weaponIndex);
        }
    }
}