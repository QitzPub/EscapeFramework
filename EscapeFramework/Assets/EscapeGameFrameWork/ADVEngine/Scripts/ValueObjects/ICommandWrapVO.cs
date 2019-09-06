using System;
using System.Collections.Generic;

namespace Qitz.ADVGame
{
    public interface ICommandWrapVO
    {
        CommandHeadVO CommandHeadVO { get; }
        List<CommandVO> CommandValues { get; }
        string SelTagValue { get; }
        string BGMValue { get; }
        string BGValue { get; }
        string SEValue { get; }
        string JumpToValue { get; }
    }
}
