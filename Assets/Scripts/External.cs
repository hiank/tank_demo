using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Moba
{
    public class External
    {
        public static void AddTag(string tag)
        {
#if UNITY_EDITOR
            if (!IsTag(tag))
                UnityEditorInternal.InternalEditorUtility.AddTag(tag);
#endif
        }

        public static bool IsTag(string tag)
        {
#if UNITY_EDITOR
            foreach (var _tag in UnityEditorInternal.InternalEditorUtility.tags)
            {
                if (_tag.Equals(tag))
                    return true;
            }
            return false;
#else
            return true;
#endif
        }
    }
}
