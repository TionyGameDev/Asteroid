using System;
using UnityEngine;

namespace Ability.Cooldown
{
    [Serializable]
    public abstract class IVerifyHandler
    {
        public abstract bool VerifyOnComplete(float currPoints, float totalPoints);
    }
    [Serializable]
    public class StandartVerifyHandler : IVerifyHandler
    {
        public override bool VerifyOnComplete(float currPoints, float totalPoints) => currPoints >= totalPoints;
    }
    [Serializable]
    public class CumulativeVerifyHandler : IVerifyHandler
    {
        public override bool VerifyOnComplete(float currPoints, float totalPoints)
        {
            return (int) currPoints - 1 >= 0;
        }
    }
}