using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bayat.Games.Platforms
{

    public interface IWayPointFollower
    {

        WayPointPath Path { get; set; }

        float Speed { get; set; }

        float WaitTimeInSeconds { get; set; }

        bool AutoMoveNext { get; set; }

        bool IgnoreWayPointSpeed { get; set; }

        bool IgnoreWayPointWaitTime { get; set; }

        WayPoint CurrentWayPoint { get; }

        void NextWayPoint();

        void SetGoalWayPoint(int index);

        void SetGoalWayPoint(WayPoint wayPoint);

    }

}