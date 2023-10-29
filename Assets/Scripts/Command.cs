using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The Command interface defines the Execute method that concrete commands must implement.
public interface ICommand
{
    void Execute();
}

// Concrete command classes implement the ICommand interface.
public class SwapCommand : ICommand
{
    private readonly GameObject player;

    public SwapCommand(GameObject player)
    {
        this.player = player;
    }

    public void Execute()
    {
        // Implement the action to be executed, e.g., make the player jump.
        player.GetComponent<Turret>().Swap();
    }
}

// Another example of a concrete command
public class AttackCommand : ICommand
{
    private readonly GameObject player;

    public AttackCommand(GameObject player)
    {
        this.player = player;
    }

    public void Execute()
    {
        // Implement the action to be executed, e.g., make the player attack.
        player.GetComponent<Turret>().Attack();
    }
}

// The Invoker class holds a command and can execute it.
public class CommandInvoker
{
    private ICommand command;

    public void SetCommand(ICommand command)
    {
        this.command = command;
    }

    public void ExecuteCommand()
    {
        if (command != null)
        {   
            //Debug.Log("Test");
            command.Execute();
        }
    }
}
