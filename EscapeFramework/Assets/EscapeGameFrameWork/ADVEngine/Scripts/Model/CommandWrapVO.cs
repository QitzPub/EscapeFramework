using System;
using System.Collections.Generic;
using System.Linq;

namespace Qitz.ADVGame
{
    public class CommandHeadVO
    {
        public CommandHeadVO(CommandType commandType, string commandValue)
        {
            CommandType = commandType;
            CommandValue = commandValue;
        }
        public CommandType CommandType { get; private set; }
        public string CommandValue { get; private set; }
    }

    public class CommandWrapVO : ICommandWrapVO
    {

        public CommandWrapVO(CommandHeadVO commandHeadVO, List<CommandVO> commandValues)
        {
            this.CommandHeadVO = commandHeadVO;
            this.CommandValues = commandValues;
        }

        public CommandHeadVO CommandHeadVO { get; private set; }
        public List<CommandVO> CommandValues { get; private set; }

        public string SelTagValue => CommandHeadVO.CommandType == CommandType.SELTAG 
                                     ? CommandValues.FirstOrDefault(cd=>cd.CommandValueType== CommandValueType.TARGET).Value : "";

        public string BGMValue => CommandHeadVO.CommandType == CommandType.BGM
                                     ? CommandValues.FirstOrDefault(cd => cd.CommandValueType == CommandValueType.FILE).Value : "";

        public string BGValue => CommandHeadVO.CommandType == CommandType.BG
                                     ? CommandValues.FirstOrDefault(cd => cd.CommandValueType == CommandValueType.FILE).Value : "";

        public string JumpToValue => CommandHeadVO.CommandType == CommandType.JUMPTO
                                     ? CommandValues.FirstOrDefault(cd => cd.CommandValueType == CommandValueType.TARGET).Value : "";

        public string SEValue => CommandHeadVO.CommandType == CommandType.SE
                                     ? CommandValues.FirstOrDefault(cd => cd.CommandValueType == CommandValueType.FILE).Value : "";
    }
    public class CommandVO
    {
        public CommandVO(CommandValueType commandValueType, string value)
        {
            CommandValueType = commandValueType;
            Value = value;
        }
        public CommandValueType CommandValueType { get; private set; }
        public string Value { get; private set; }
    }

}
