using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Qitz.ADVGame.ParseUtil
{
    public sealed class QitzADVMacroParseUtil
    {

        string advMacro;

        public QitzADVMacroParseUtil(string advMacro)
        {
            this.advMacro = advMacro;
        }

        public List<ICutVO> Deserialize()
        {
            var macroLines = advMacro.Split(MacroParseData.NEW_LINE_STRING.ToCharArray());
            List<PreCursorCutVO> preCursorCutVOs = GetPreCursorCuts(macroLines);
            List<PreCursorCutSet> preCursorCutSets = ConvertPreCursprCutSets(preCursorCutVOs);
            return GetCutVOs(preCursorCutSets);
        }


        List<ICutVO> GetCutVOs(List<PreCursorCutSet> preCursorCutSets)
        {
            List<ICutVO> cutVOs = new List<ICutVO>();
            int number = 0;
            foreach (var preCursorCutSet in preCursorCutSets)
            {
                var cutVO = GetCutVO(preCursorCutSet, number);
                if (!cutVO.IsEmptyVO)
                {
                    cutVOs.Add(cutVO);
                }
                number++;
            }
            return cutVOs;
        }

        CutVO GetCutVO(PreCursorCutSet preCursorCutSet,int number)
        {
            CutVO cutVO = new CutVO(number);
            foreach (var pcv in preCursorCutSet.PreCursorCuts)
            {
                SetCutVO(ref cutVO, pcv);
            }
            return cutVO;
        }

        void SetCutVO(ref CutVO cutVO,PreCursorCutVO preCursorCut)
        {
            switch (preCursorCut.Type)
            {
                case ADVLineType.COMMAND:
                    cutVO.AddCommand(preCursorCut.Command);
                    break;
                case ADVLineType.COMMENT:
                    break;
                case ADVLineType.CUT_SPLIT:
                    break;
                case ADVLineType.NAME:
                    cutVO.SetWindowCharacterName(preCursorCut.StringValue);
                    break;
                case ADVLineType.TEXT:
                    cutVO.SetWindowText(preCursorCut.StringValue);
                    break;
                default:
                    break;
            }
        }

        List<PreCursorCutVO> GetPreCursorCuts(string[] macroLines)
        {
            List<PreCursorCutVO> preCursorCutVOs = new List<PreCursorCutVO>();
            foreach (var macroLine in macroLines)
            {
                var preCursorCutVO = GetPreCursorCutVO(macroLine);
                preCursorCutVOs.Add(preCursorCutVO);
            }
            return preCursorCutVOs;
        }

        List<PreCursorCutSet> ConvertPreCursprCutSets(List<PreCursorCutVO> preCursorCutVOs)
        {
            List<PreCursorCutSet> preCursorCutSets = new List<PreCursorCutSet>();
            List<PreCursorCutVO> preCursorCuts = new List<PreCursorCutVO>();
            foreach (var pcv in preCursorCutVOs)
            {
                if(pcv.Type == ADVLineType.CUT_SPLIT)
                {
                    var _preCursorCuts = new List<PreCursorCutVO>(preCursorCuts);
                    preCursorCutSets.Add(new PreCursorCutSet(_preCursorCuts));
                    preCursorCuts.Clear();
                }
                else
                {
                    preCursorCuts.Add(pcv);
                }
            }
            return preCursorCutSets;
        }

        PreCursorCutVO GetPreCursorCutVO(string macroLine)
        {
            ADVLineType aDVLineType = JudgeAdvMacroLineAttribute(macroLine);
            switch (aDVLineType)
            {
                case ADVLineType.COMMAND:
                    return new PreCursorCutVO(aDVLineType, MacroParseData.EMPTY_WORD, ConvertCommandVOFromCommandMacroLine(macroLine));
                case ADVLineType.COMMENT:
                    return new PreCursorCutVO(aDVLineType, MacroParseData.EMPTY_WORD, null);
                case ADVLineType.CUT_SPLIT:
                    return new PreCursorCutVO(aDVLineType, MacroParseData.EMPTY_WORD, null);
                case ADVLineType.NAME:
                    return new PreCursorCutVO(aDVLineType, GetNameFromNameMacroLine(macroLine), null);
                case ADVLineType.TEXT:
                    return new PreCursorCutVO(aDVLineType, macroLine, null);
                default:
                    throw new Exception($"無効な形式の文字列です:{macroLine}");
            }
        }

        ADVLineType JudgeAdvMacroLineAttribute(string advMacroLine)
        {
            string leadString = advMacroLine == MacroParseData.EMPTY_WORD 
                                ? MacroParseData.EMPTY_WORD : advMacroLine.Substring(0, 1);
            string lastString = advMacroLine == MacroParseData.EMPTY_WORD 
                                ? MacroParseData.EMPTY_WORD : advMacroLine.Substring(advMacroLine.Length-1, 1);

            if (leadString == MacroParseData.COMMENT_START_WORD)
            {
                return ADVLineType.COMMENT;
            }
            else if (leadString == MacroParseData.COMMAND_START_WORD && lastString == MacroParseData.COMMAND_END_WORD)
            {
                return ADVLineType.COMMAND;
            }
            else if (leadString == MacroParseData.CHARACTER_NAME_START_WORD && lastString == MacroParseData.CHARACTER_NAME_END_WORD)
            {
                return ADVLineType.NAME;
            }
            else if (advMacroLine == MacroParseData.EMPTY_WORD)
            {
                return ADVLineType.CUT_SPLIT;
            }
            else
            {
                return ADVLineType.TEXT;
            }
        }

        string GetNameFromNameMacroLine(string nameMacroLine)
        {
            return nameMacroLine.Replace(MacroParseData.CHARACTER_NAME_START_WORD, "").Replace(MacroParseData.CHARACTER_NAME_END_WORD,"");
        }

        CommandWrapVO ConvertCommandVOFromCommandMacroLine(string commandMacroLine)
        {
            string pureCommandMacroLine = commandMacroLine.Replace(MacroParseData.COMMAND_START_WORD,"").Replace(MacroParseData.COMMAND_END_WORD,"");
            string[] commands = pureCommandMacroLine.Split(MacroParseData.COMMAND_SPLIT_WORD.ToCharArray());
            string leadCommandWord = commands[0];
            string[] commandValues = RemoveArryElement(commands, leadCommandWord);
            List<CommandVO> commandValueList = new List<CommandVO>();
            foreach (var commandValueWord in commandValues)
            {
                var cVo = GetCommandVOFromCommandValueWord(commandValueWord);
                commandValueList.Add(cVo);
            }
            CommandHeadVO commandHeadVO = GetCommandTypeFromCommandWord(leadCommandWord);

            var commandVO = new CommandWrapVO(commandHeadVO, commandValueList);
            return commandVO;
        }

        CommandVO GetCommandVOFromCommandValueWord(string commandValueWord)
        {
            if (commandValueWord == CommandValueString.stop.ToString())
            {
                return new CommandVO(CommandValueType.STOP, "");

            }
            else if (commandValueWord.IndexOf(CommandValueString.file.ToString()) != -1)
            {
                string fileName = commandValueWord.Replace("\"", "").Split('=')[1];
                return new CommandVO(CommandValueType.FILE, fileName);
            }
            else if (commandValueWord == CommandValueString.tb.ToString())
            {
                return new CommandVO(CommandValueType.TB, "");
            }
            else if (commandValueWord.IndexOf(CommandValueString.time.ToString()) != -1)
            {
                string time = commandValueWord.Split('=')[1];
                return new CommandVO(CommandValueType.TIME, time);
            }
            else if (commandValueWord == CommandValueString.up.ToString())
            {
                return new CommandVO(CommandValueType.GO_UP, "");
            }
            else if (commandValueWord == CommandValueString.出.ToString())
            {
                return new CommandVO(CommandValueType.APPEAR, "");
            }
            else if (commandValueWord == CommandValueString.消.ToString())
            {
                return new CommandVO(CommandValueType.DISAPPEAR, "");
            }
            else if (commandValueWord == CommandValueString.顔.ToString())
            {
                return new CommandVO(CommandValueType.SHOW_WINDOW_FACE_MARK, "");
            }
            else if (commandValueWord == CommandValueString.keyinput.ToString())
            {
                return new CommandVO(CommandValueType.KEY_INPUT, "");
            }
            else if (0 <= Array.IndexOf(ParseCommandList.costumeList, commandValueWord))
            {
                return new CommandVO(CommandValueType.SET_COSTUME, commandValueWord);
            }
            else if (0 <= Array.IndexOf(ParseCommandList.faceList, commandValueWord))
            {
                return new CommandVO(CommandValueType.SET_FACE, commandValueWord);
            }
            else if (commandValueWord.IndexOf(CommandValueString.text.ToString()) != -1)
            {
                string textValue = commandValueWord.Replace("\"\"", "").Split('=')[1];
                return new CommandVO(CommandValueType.TEXT, textValue);
            }
            else if (commandValueWord.IndexOf(CommandValueString.target.ToString()) != -1)
            {
                string targetName = commandValueWord.Split('=')[1];
                return new CommandVO(CommandValueType.TARGET, targetName);
            }
            else
            {
                throw new Exception($"想定されていない形式です:{commandValueWord}");
            }
        }

        CommandHeadVO GetCommandTypeFromCommandWord(string commandWord)
        {
            if (commandWord == CommandString.bg.ToString())
            {
                return new CommandHeadVO(CommandType.BG,"");
            }
            else if (commandWord == CommandString.bgm.ToString())
            {
                return new CommandHeadVO(CommandType.BGM, "");
            }
            else if (commandWord == CommandString.messageoff.ToString())
            {
                return new CommandHeadVO(CommandType.MESSAGEOFF, "");
            }
            else if (commandWord == CommandString.messageon.ToString())
            {
                return new CommandHeadVO(CommandType.MESSAGEON, "");
            }
            else if (commandWord == CommandString.wait.ToString())
            {
                return new CommandHeadVO(CommandType.WAIT, "");
            }
            else if (commandWord == CommandString.暗転共通.ToString())
            {
                return new CommandHeadVO(CommandType.BLAKOUT, "");
            }
            else if (0 <= Array.IndexOf(ParseCommandList.characterList, commandWord))
            {
                return new CommandHeadVO(CommandType.CARACTER, commandWord);
            }
            else if (commandWord == CommandString.ev.ToString())
            {
                return new CommandHeadVO(CommandType.EV, "");
            }
            else if (commandWord == CommandString.seladd.ToString())
            {
                return new CommandHeadVO(CommandType.SELADD, "");
            }
            else if (commandWord == CommandString.select.ToString())
            {
                return new CommandHeadVO(CommandType.SELECT, "");
            }
            else if (commandWord == CommandString.seltag.ToString())
            {
                return new CommandHeadVO(CommandType.SELTAG, "");
            }
            else if (commandWord == CommandString.jumpto.ToString())
            {
                return new CommandHeadVO(CommandType.JUMPTO, "");
            }
            else if (commandWord == CommandString.se.ToString())
            {
                return new CommandHeadVO(CommandType.SE, "");
            }

            throw new Exception($"想定されないコマンドです:{commandWord}");
        }

        string[] RemoveArryElement(string[] originaryArry,string removeElement)
        {
            var list = new List<string>();
            list.AddRange(originaryArry);
            list.Remove(removeElement);
            return list.ToArray();
        }

    }
}
