using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Moba
{
    public class External
    {
        public static void AddTag(string tag)
        {
            if (!IsTag(tag))
                UnityEditorInternal.InternalEditorUtility.AddTag(tag);
        }

        public static bool IsTag(string tag)
        {
            foreach (var _tag in UnityEditorInternal.InternalEditorUtility.tags)
            {
                if (_tag.Equals(tag))
                    return true;
            }
            return false;
        }
    }
}
