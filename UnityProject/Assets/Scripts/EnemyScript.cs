﻿using UnityEngine;
    public Constants.Dir direction;
        direction = Constants.Dir.S;
        rigidbody2D.velocity = Constants.getDirectionVector(direction) * speed;