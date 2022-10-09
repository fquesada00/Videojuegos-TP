using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CmdAttack : ICommand
{
    private IAttackable _weapon;

    public CmdAttack(IAttackable weapon)
    {
        _weapon = weapon;
    }

    public void Execute()
    {
        _weapon.Attack();
    }
}
