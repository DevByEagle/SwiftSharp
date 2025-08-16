using System;
using System.Reflection;

namespace SwiftSharp
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Interface | AttributeTargets.Enum | AttributeTargets.Delegate, Inherited = true, AllowMultiple = false)]
    public sealed class ExperimentalAttribute : Attribute
    {
        public string? FeatureName { get; }

        /// <summary>
        /// Marks a member as experimental without a feature name.
        /// </summary>
        internal ExperimentalAttribute() { }

        /// <summary>
        /// Marks a member as experimental with an optional feature name.
        /// </summary>
        /// <param name="featureName">Name of the experimental feature.</param>
        internal ExperimentalAttribute(string featureName)
        {
            FeatureName = featureName;
        }

        internal static void WarnIfExperimental(MemberInfo member)
        {
            var attr = member.GetCustomAttribute<ExperimentalAttribute>(inherit: true);
            if (attr != null)
            {
                string name = member.Name;
                string feature = string.IsNullOrEmpty(attr.FeatureName) ? "this feature" : attr.FeatureName;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"[Experimental Warning] You are using {feature} ({name}). Behavior may change in future versions.");
                Console.ResetColor();
            }
        }

        /// <summary>
        /// Checks if the given member is marked as experimental.
        /// </summary>
        /// <param name="member">The member to check.</param>
        /// <param name="inherit">Whether to check inherited attributes (default: true).</param>
        /// <returns>True if the member has the ExperimentalAttribute; otherwise, false.</returns>
        public static bool IsExperimental(MemberInfo member, bool inherit = true)
            => Attribute.IsDefined(member, typeof(ExperimentalAttribute), inherit);

    }
}