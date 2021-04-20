using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace ScalerAnimation
{
    public class ScalerAnimationEditorWindow : EditorWindow
    {
        private AnimationClip m_animationClip;
        private float m_scaleFactor = 1.0f;

        [MenuItem("Tools/ScalerAnimation")]
        static void Window()
        {
            var window = GetWindow<ScalerAnimationEditorWindow>("ScalerAnimation");
            window.Show();
        }

        private void Update()
        {
        }

        private void OnGUI()
        {
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("AnimationClip");
            m_animationClip = (AnimationClip)EditorGUILayout.ObjectField(m_animationClip, typeof(AnimationClip), true);
            m_scaleFactor = EditorGUILayout.FloatField("Scale Factor", m_scaleFactor);

            if (m_animationClip != null)
            {
                if (GUILayout.Button("Apply"))
                {
                    ScalerAnimation(m_animationClip);
                }
            }
        }

        private void ScalerAnimation(AnimationClip animationClip)
        {
            // Copy Clip
            var copyAnimationClip = CopyAnimationClip(animationClip);
            // Get Properties
            var animationProperties = GetAnimationProperties(copyAnimationClip);
            // Apply Scale Factor
            ApplyScalerAnimation(copyAnimationClip, animationProperties);
        }

        private AnimationClip CopyAnimationClip(AnimationClip animationClip)
        {
            var copyAnimationClip = Instantiate(animationClip) as AnimationClip;
            var path = AssetDatabase.GetAssetPath(animationClip);
            var directoryName = System.IO.Path.GetDirectoryName(path);
            AssetDatabase.CreateAsset(copyAnimationClip, $"{directoryName}/{animationClip.name}_scaled.anim");
            return copyAnimationClip;
        }

        private void ApplyScalerAnimation(AnimationClip animationClip, Dictionary<string, AnimationProperty> animationProperties)
        {
            var bindings = AnimationUtility.GetCurveBindings(animationClip);
            foreach (var binding in bindings)
            {
                var curve = AnimationUtility.GetEditorCurve(animationClip, binding);
                var boneName = System.IO.Path.GetFileName(binding.path);
                var animationProperty = animationProperties[boneName];
                switch (binding.propertyName)
                {
                    case "m_LocalPosition.x":
                        curve.keys = animationProperty.GetScalerLocalPositionX(m_scaleFactor);
                        AnimationUtility.SetEditorCurve(animationClip, binding, curve);
                        break;
                    case "m_LocalPosition.y":
                        curve.keys = animationProperty.GetScalerLocalPositionY(m_scaleFactor);
                        AnimationUtility.SetEditorCurve(animationClip, binding, curve);
                        break;
                    case "m_LocalPosition.z":
                        curve.keys = animationProperty.GetScalerLocalPositionZ(m_scaleFactor);
                        AnimationUtility.SetEditorCurve(animationClip, binding, curve);
                        break;
                }
            }
        }

        private Dictionary<string, AnimationProperty> GetAnimationProperties(AnimationClip animationClip)
        {
            var animationProperties = new Dictionary<string, AnimationProperty>();

            var bindings = AnimationUtility.GetCurveBindings(animationClip);
            foreach (var binding in bindings)
            {
                var curve = AnimationUtility.GetEditorCurve(animationClip, binding);
                var boneName = System.IO.Path.GetFileName(binding.path);
                if (!animationProperties.ContainsKey(boneName))
                {
                    animationProperties.Add(boneName, new AnimationProperty());
                }
                switch (binding.propertyName)
                {
                    case "m_LocalPosition.x":
                        animationProperties[boneName].localPositionX.AddRange(curve.keys);
                        break;
                    case "m_LocalPosition.y":
                        animationProperties[boneName].localPositionY.AddRange(curve.keys);
                        break;
                    case "m_LocalPosition.z":
                        animationProperties[boneName].localPositionZ.AddRange(curve.keys);
                        break;
                }
            }
            return animationProperties;
        }
    }
}
