using UnityEngine;

namespace HandPoseDetection
{
    public interface IHandSkeleton
    {
        Transform Root { get; }
        Transform[] Bones { get; }
    }
}