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

    public enum PuppyTaskType
    {
        EatTask,
        GoToiletTask,
        SleepTask,
        TakeWalkTask,
        TakeWashTask,
        GoWrongToiletTask
    }

    public enum PuppyState
    {
        Idle,
        MoveToClickedDest,
        RunToTaskPoint,
        Eat,
        Toliet,
        Sleep,
        WantToilet
    }
}
