﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Collision
{
    interface ICollisionControlFactory
    {
        CollisionControl GetCollisonControl();
    }
}
