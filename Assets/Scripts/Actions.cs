using System;
using static UnityEngine.EventSystems.EventTrigger;

public static class Actions //static so we can call it from elsewhere in our project
{
    public static Action<int, float> increaseScore;
    public static Action<int> shakeCamera;
    public static Action<int, connectionCheck> getOpponentManager;
}

