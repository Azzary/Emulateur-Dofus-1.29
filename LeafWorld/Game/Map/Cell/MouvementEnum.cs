using System;
using System.Collections.Generic;
using System.Text;

namespace LeafWorld.Game.Map.Cell
{
    public enum MovementEnum
    {
        UNWALKABLE = 0,
        DOOR = 1,
        TRIGGER = 2,
        WALKABLE = 4,
        PADDOCK = 5,
        PATH = 7,
    }
}
