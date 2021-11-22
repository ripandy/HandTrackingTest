using Kadinche.Kassets.Collection;
using UnityEngine;

namespace HandPoseDetection
{
    [CreateAssetMenu(fileName = "NewHandFormCollection", menuName = "HandFormRecognizer/HandFormCollection", order = 0)]
    public class HandFormCollection : Collection<HandFormData>
    {
    }
}