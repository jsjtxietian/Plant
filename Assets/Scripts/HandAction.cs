using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public interface HandAction
{
    bool CheckOutBound(Coordinate nextPos);//todo can work into flexible?
    Coordinate GetInitPos();

    bool CanPick(ComponentType type);
}