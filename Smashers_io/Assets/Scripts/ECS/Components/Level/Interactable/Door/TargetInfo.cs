using UnityEngine;

namespace Door.Animations
{
    public class TargetInfo
    {
        public Transform root { get; private set; }
        public GenericAnimationComponent animations { get; private set; }

        public TargetInfo(Transform root, GenericAnimationComponent animations)
        {
            this.root = root;
            this.animations = animations;
        }
    }
}
