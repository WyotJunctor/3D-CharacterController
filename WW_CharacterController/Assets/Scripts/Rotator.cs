﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WW
{

    public class Rotator : MonoBehaviour
    {
        [HideInInspector]
        public float smooth_speed = 0.1f;
        GameObject target;

        public void Face(GameObject target, bool lockX = true, bool lockY = true, bool lockZ = true)
        {
            Vector3 direction = (target.transform.position - transform.position).normalized;
            Face(direction, lockZ, lockY, lockZ);
        }

        public void Face(Vector3 dir, bool lockX = true, bool lockY = true, bool lockZ = true, bool stop = true)
        {
            float x = transform.rotation.x;
            float y = transform.rotation.y;
            float z = transform.rotation.z;

            if (stop) StopAllCoroutines();
            transform.rotation = Quaternion.Slerp(transform.rotation,
                Quaternion.LookRotation(dir), smooth_speed);

            transform.rotation = new Quaternion((lockX) ? x : transform.rotation.x,
                                                (lockY) ? y : transform.rotation.y,
                                                (lockZ) ? z : transform.rotation.z,
                                                transform.rotation.w);
        }

        public void TurnTo(GameObject target, bool lockX = true, bool lockY = true, bool lockZ = true, bool follow = false)
        {
            Vector3 direction = (target.transform.position - transform.position).normalized;
            TurnTo(direction, lockX, lockY, lockZ, follow);
        }

        public void TurnTo(Vector3 direction, bool lockX = true, bool lockY = true, bool lockZ = true, bool follow = false)
        {
            StopAllCoroutines();
            StartCoroutine(TurnToCo(direction, lockX, lockY, lockZ, follow));
        }

        private IEnumerator TurnToCo(Vector3 direction, bool lockX = true, bool lockY = true, bool lockZ = true, bool follow = false)
        {
            Quaternion to = Quaternion.LookRotation(direction);
            Quaternion from = transform.rotation;
            Vector3 diff = new Vector3(
                (lockX) ? 0 : to.x - from.x,
                (lockY) ? 0 : to.y - from.y,
                (lockZ) ? 0 : to.z - from.z
                );

            while (diff.magnitude > 0.05f || (follow && target))
            {
                Face(direction, lockX, lockY, lockZ, false);
                yield return null;
            }
        }
    }
}
