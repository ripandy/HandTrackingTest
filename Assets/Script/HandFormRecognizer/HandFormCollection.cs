using Kadinche.Kassets.Collection;
using UnityEngine;

namespace HandGestureDetector
{
    [CreateAssetMenu(fileName = "NewHandFormCollection", menuName = "HandFormRecognizer/HandFormCollection", order = 0)]
    public class HandFormCollection : Collection<HandFormData>
    {
    }
}