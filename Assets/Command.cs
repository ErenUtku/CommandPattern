using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Command
{
    public abstract void Execute(Animator anim,bool forward);
}

public class PerformJump : Command
{
    public override void Execute(Animator anim, bool forward)
    {
        if(forward)
        anim.SetTrigger("isJumping");
        else
            anim.SetTrigger("isJumpingR");
    }
}
public class PerformPunching : Command
{
    public override void Execute(Animator anim, bool forward)
    {
        if(forward)
        anim.SetTrigger("isPunching");
        else
            anim.SetTrigger("isPunchingR");
    }
}
public class PerformKicking : Command
{
    public override void Execute(Animator anim, bool forward)
    {
        if(forward)
        anim.SetTrigger("isKicking");
        else
            anim.SetTrigger("isKickingR");

    }
}

public class Default : Command
{
    public override void Execute(Animator anim, bool forward)
    {
        return;
    }
}
public class MoveForward : Command
{
    public override void Execute(Animator anim, bool forward)
    {
        if(forward)
        anim.SetTrigger("isWalking");
        else
            anim.SetTrigger("isWalkingR");
    }
}


