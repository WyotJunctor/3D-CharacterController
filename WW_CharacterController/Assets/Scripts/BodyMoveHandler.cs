using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WW
{
    public class BodyMoveHandler : BodyHandler
    {
        protected Rigidbody rb;

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();
            rb = GetComponent<Rigidbody>();
        }
    }
}
