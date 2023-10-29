using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandTrigger : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private CommandInvoker invoker;

    private void Start()
    {
        invoker = new CommandInvoker();
    }

    public void SetSwapCommand()
    {
        ICommand swapCommand = new SwapCommand(player);
        invoker.SetCommand(swapCommand);
    }

    public void SetAttackCommand()
    {
        ICommand attackCommand = new AttackCommand(player);
        invoker.SetCommand(attackCommand);
    }

    public void ExecuteCommand()
    {
        invoker.ExecuteCommand();
    }
}
