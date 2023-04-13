using Pixelplacement;
using UnityEngine;

namespace Core.Camera
{
    public class CameraController : MonoBehaviour
    {
        public static CameraController Instance;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
        }

        public void ShakeCamera(float strength, float duration = 1.0f)
        {
            Tween.Shake(transform, transform.localPosition, new Vector3(strength, strength, 0), duration, 0);
        }
    }
}