using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.Events;

public class PlayerCommandDetector : MonoBehaviour
{

    public UnityEvent OnJumpCommand;
    public UnityEvent OnFastCommand;
    public UnityEvent OnSlowCommand;

    public void HandleCommand(string command)
    {

        command = command.ToLowerInvariant().Trim();
        Debug.Log($"Handle Command: '{command}'");
        UnityEvent? evt = command switch {
            "jump" => OnJumpCommand,
            "slow" => OnSlowCommand,
            "fast" => OnFastCommand,
            _ => null
        };
        evt?.Invoke();
        // switch (command)
        // {
        //     case "jump":
        //         OnJumpCommand.Invoke();
        //         break;
        //     case "slow":
        //         OnSlowCommand.Invoke();
        //         break;
        //     case "fast":
        //         OnFastCommand.Invoke();
        //         break;
        // }
    }

    private UnityEvent GetEvent()
    {
        return OnJumpCommand;
    }



}
