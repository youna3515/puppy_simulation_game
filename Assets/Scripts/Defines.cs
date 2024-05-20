using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defines
{
    public enum UIEventType
    {
        PointDown,
        PointUp,
    }

    public enum DogTask
    {
        EatTask,
        GoToiletTask,
        SleepTask,
        TakeWalkTask,
        TakeWashTask
    }

    public enum DogState
    {
        Idle,
        MoveToClickedDest,
        RunToTaskPoint,
        Eat,
        Toliet,
        Sleep
    }
}
