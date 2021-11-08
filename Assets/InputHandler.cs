using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public GameObject actor;
    Animator anim;
    Command keySpace, keyP, keyK, keyW;
    List<Command> oldCommands = new List<Command>();

    Coroutine replayCoroutine;

    bool shouldStartReplay;
    bool isReplaying;

    void Start()
    {
    
        keySpace = new PerformJump();
        keyP = new PerformPunching();
        keyK = new PerformKicking();
        keyW = new MoveForward();
        anim = actor.GetComponent<Animator>();
        Camera.main.GetComponent<CameraFollow360>().player = actor.transform;
    }

    void Update()
    {

        if (!isReplaying)
        {
            HandleInput();
        }

        StartReplay();
    }
    void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            keySpace.Execute(anim, true);
            oldCommands.Add(keySpace);
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            keyP.Execute(anim, true);
            oldCommands.Add(keyP);
        }
        else if (Input.GetKeyDown(KeyCode.K))
        {
            keyK.Execute(anim, true);
            oldCommands.Add(keyK);
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            keyW.Execute(anim, true);
            oldCommands.Add(keyW);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            shouldStartReplay = true;
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            UndoLastCommand();
        }
    }
    void UndoLastCommand()
    {
        if (oldCommands.Count > 0)
        {
            Command c = oldCommands[oldCommands.Count - 1];
            c.Execute(anim, false);
            oldCommands.RemoveAt(oldCommands.Count - 1);
        }
    }
    void StartReplay()
    {
        if (shouldStartReplay && oldCommands.Count > 0)
        {
            shouldStartReplay = false;

            if (replayCoroutine != null)
            {
                StopCoroutine(replayCoroutine);
            }

            replayCoroutine = StartCoroutine(ReplayCommands());
        }
       
       
    }
    IEnumerator ReplayCommands()
    {
        isReplaying = true;
        for (int i = 0; i < oldCommands.Count; i++)
        {
            oldCommands[i].Execute(anim,true);
            yield return new WaitForSeconds(1f);
        }
        isReplaying = false;
     
    }
}
