using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WW
{
    public class JumpHandler : BodyMoveHandler
    {

        float jump_cooldown = 1f, last_jump = -1f;

        [HideInInspector]
        public float jump_force = 10f, jump_rise = 20f;

        public void Act(bool jump, bool hold_jump)
        {
            if (pb.action_state > priority)
                return;
            if (jump && pb.jump_checker.Grounded && Time.time - last_jump > jump_cooldown)
            {
                last_jump = Time.time;
                rb.AddForce(-pb.jump_checker.ground_direction * jump_force, ForceMode.Impulse);
            }
        }
    }
}