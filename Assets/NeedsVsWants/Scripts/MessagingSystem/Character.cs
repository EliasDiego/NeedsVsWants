using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NeedsVsWants.MessagingSystem
{
    [CreateAssetMenu(menuName = "NeedsVsWants/Messages/Character")]
    public class Character : ScriptableObject
    {
        [SerializeField]
        Sprite _ProfilePicture;

        public Sprite profilePicture => _ProfilePicture;

        public static implicit operator string(Character c) => c.name;
    }
}