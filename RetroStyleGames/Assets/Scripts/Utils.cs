using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    public static IEnumerator MoveToTarget(Transform obj, Vector3 target, float velocity)
    {
        while (obj.position != target)
        {
            obj.position = Vector3.MoveTowards(obj.position, target, velocity * Time.deltaTime);
            yield return null;
        }
    }
}
