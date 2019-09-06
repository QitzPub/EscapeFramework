using System.Collections.Generic;

namespace Qitz.ADVGame.ParseUtil
{
    public struct PreCursorCutVO
    {
        public PreCursorCutVO(ADVLineType type, string text, CommandWrapVO command)
        {
            Type = type;
            StringValue = text;
            Command = command;
        }
        public ADVLineType Type { get; private set; }
        public string StringValue { get; private set; }
        public CommandWrapVO Command { get; private set; }
    }

    public struct PreCursorCutSet
    {
        public PreCursorCutSet(List<PreCursorCutVO> preCursorCuts)
        {
            PreCursorCuts = preCursorCuts;
        }
        public List<PreCursorCutVO> PreCursorCuts { get; private set; }
    }



    public enum ADVLineType
    {
        NONE,
        COMMENT,
        COMMAND,
        NAME,
        TEXT,
        CUT_SPLIT,
    }

    public sealed class MacroParseData
    {
        public const string EMPTY_WORD = "";
        public const string NEW_LINE_STRING = "\n";
        public const string COMMAND_START_WORD = "[";
        public const string COMMAND_END_WORD = "]";
        public const string COMMAND_SPLIT_WORD = " ";
        public const string CHARACTER_NAME_START_WORD = "【";
        public const string CHARACTER_NAME_END_WORD = "】";
        public const string COMMENT_START_WORD = ";";
    }
}
