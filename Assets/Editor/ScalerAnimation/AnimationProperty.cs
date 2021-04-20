using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScalerAnimation
{
    public class AnimationProperty
    {
        public List<Keyframe> localPositionX = new List<Keyframe>();
        public List<Keyframe> localPositionY = new List<Keyframe>();
        public List<Keyframe> localPositionZ = new List<Keyframe>();
        public List<Keyframe> localRotationX = new List<Keyframe>();
        public List<Keyframe> localRotationY = new List<Keyframe>();
        public List<Keyframe> localRotationZ = new List<Keyframe>();
        public List<Keyframe> localRotationW = new List<Keyframe>();

        public Keyframe[] GetScalerLocalPositionX(float scaleFactor)
        {
            var keyframes = localPositionX.ToArray();
            for (int i = 0, n = keyframes.Length; i < n; i++)
            {
                var keyframe = keyframes[i];
                keyframe.value *= scaleFactor;
                keyframe.inTangent *= scaleFactor;
                keyframe.outTangent *= scaleFactor;
                keyframes[i] = keyframe;
            }
            return keyframes;
        }
        public Keyframe[] GetScalerLocalPositionY(float scaleFactor)
        {
            var keyframes = localPositionY.ToArray();
            for (int i = 0, n = keyframes.Length; i < n; i++)
            {
                var keyframe = keyframes[i];
                keyframe.value *= scaleFactor;
                keyframe.inTangent *= scaleFactor;
                keyframe.outTangent *= scaleFactor;
                keyframes[i] = keyframe;
            }
            return keyframes;
        }
        public Keyframe[] GetScalerLocalPositionZ(float scaleFactor)
        {
            var keyframes = localPositionZ.ToArray();
            for (int i = 0, n = keyframes.Length; i < n; i++)
            {
                var keyframe = keyframes[i];
                keyframe.value *= scaleFactor;
                keyframe.inTangent *= scaleFactor;
                keyframe.outTangent *= scaleFactor;
                keyframes[i] = keyframe;
            }
            return keyframes;
        }

        public Quaternion GetQuaternion(int index)
        {
            return new Quaternion(
                    localRotationX[index].value,
                    localRotationY[index].value * -1f,
                    localRotationZ[index].value * -1f,
                    localRotationW[index].value
                );
        }

        public Keyframe[] GetScalerLocalRotationX()
        {
            var keyframes = localRotationX.ToArray();
            for (int i = 0, n = keyframes.Length; i < n; i++)
            {
                var keyframe = keyframes[i];
                keyframe.value = GetQuaternion(i).x;
                keyframes[i] = keyframe;
            }
            return keyframes;
        }

        public Keyframe[] GetScalerLocalRotationY()
        {
            var keyframes = localRotationY.ToArray();
            for (int i = 0, n = keyframes.Length; i < n; i++)
            {
                var keyframe = keyframes[i];
                keyframe.value = GetQuaternion(i).y;
                keyframe.inTangent *= -1f;
                keyframe.outTangent *= -1f;
                keyframes[i] = keyframe;
            }
            return keyframes;
        }

        public Keyframe[] GetScalerLocalRotationZ()
        {
            var keyframes = localRotationZ.ToArray();
            for (int i = 0, n = keyframes.Length; i < n; i++)
            {
                var keyframe = keyframes[i];
                keyframe.value = GetQuaternion(i).z;
                keyframe.inTangent *= -1f;
                keyframe.outTangent *= -1f;
                keyframes[i] = keyframe;
            }
            return keyframes;
        }

        public Keyframe[] GetScalerLocalRotationW()
        {
            var keyframes = localRotationW.ToArray();
            for (int i = 0, n = keyframes.Length; i < n; i++)
            {
                var keyframe = keyframes[i];
                keyframe.value = GetQuaternion(i).w;
                keyframes[i] = keyframe;
            }
            return keyframes;
        }
    }
}
