using BefungeInterpreter.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BefungeInterpreter
{
    class BefungeLexer
    {
        private const string _digitExpression = "[0-9]";
        private const string _charExpression = @"(?![vpg])[a-zA-Z]";
        private bool _charMode = false;
        public BefungeToken[][] ParseRawInput(string code)
        {
            string[] splited = code.Split('\n');
            
            BefungeToken[][] toReturn = new BefungeToken[splited.Length][];
            
            for (int i=0;i<splited.Length;i++)
            {
                var line = splited[i];
                for(int j=0;j<line.Length;j++)
                {
                    string token = line[j].ToString();
                    BefungeToken discovered = DiscoverToken(token);
                    if (toReturn[i] == null)
                        toReturn[i] = new BefungeToken[line.Length];

                    toReturn[i][j] = discovered;
                }
            }
            return toReturn;

        }

        private BefungeToken DiscoverToken(string token)
        {
            BefungeToken toReturn;
            if (token == "\"")
            {
                _charMode = !_charMode;
                toReturn = new BefungeToken(BefungeTokenType.Quotes);
                return toReturn;
            }
            if (_charMode)
            {
                toReturn = new BefungeIntToken(token[0], BefungeTokenType.Char);
                return toReturn;
            }
            if (Regex.IsMatch(token, _digitExpression))
                toReturn = new BefungeIntToken(int.Parse(token), BefungeTokenType.Int);
            else if (Regex.IsMatch(token, _charExpression))
                toReturn = new BefungeIntToken(token[0], BefungeTokenType.Char);
            else if (token == " ")
                return new BefungeToken(BefungeTokenType.NOP);
            else switch (token)
            {
                case "^":
                    toReturn = new BefungeToken(BefungeTokenType.MoveUp);
                    break;
                case "v":
                    toReturn = new BefungeToken(BefungeTokenType.MoveDown);
                    break;
                case "<":
                    toReturn = new BefungeToken(BefungeTokenType.MoveLeft);
                    break;
                case ">":
                    toReturn = new BefungeToken(BefungeTokenType.MoveRight);
                    break;
                case "_":
                    toReturn = new BefungeToken(BefungeTokenType.MoveRightConditional);
                    break;
                case "|":
                    toReturn = new BefungeToken(BefungeTokenType.MoveDownConditional);
                    break;
                case "?":
                    toReturn = new BefungeToken(BefungeTokenType.MoveRandom);
                    break;
                case "#":
                    toReturn = new BefungeToken(BefungeTokenType.Trampoline);
                    break;
                case "@":
                    toReturn = new BefungeToken(BefungeTokenType.EOF);
                    break;
                case ":":
                    toReturn = new BefungeToken(BefungeTokenType.CopyTop);
                    break;
                case @"\":
                    toReturn = new BefungeToken(BefungeTokenType.SwapTop);
                    break;
                case "$":
                    toReturn = new BefungeToken(BefungeTokenType.RemoveTop);
                    break;
                case "p":
                    toReturn = new BefungeToken(BefungeTokenType.Put);
                    break;
                case "g":
                    toReturn = new BefungeToken(BefungeTokenType.Get);
                    break;
                case "+":
                    toReturn = new BefungeToken(BefungeTokenType.Add);
                    break;
                case "-":
                    toReturn = new BefungeToken(BefungeTokenType.Subtract);
                    break;
                case "*":
                    toReturn = new BefungeToken(BefungeTokenType.Multiply);
                    break;
                case "/":
                    toReturn = new BefungeToken(BefungeTokenType.IntDivide);
                    break;
                case "%":
                    toReturn = new BefungeToken(BefungeTokenType.Modulo);
                    break;
                case "!":
                    toReturn = new BefungeToken(BefungeTokenType.Not);
                    break;
                case "`":
                    toReturn = new BefungeToken(BefungeTokenType.GreaterThan);
                    break;
                case "&":
                    toReturn = new BefungeToken(BefungeTokenType.GetInt);
                    break;
                case "~":
                    toReturn = new BefungeToken(BefungeTokenType.GetSymbol);
                    break;
                case ".":
                    toReturn = new BefungeToken(BefungeTokenType.PrintTopInt);
                    break;
                case ",":
                    toReturn = new BefungeToken(BefungeTokenType.PrintTopSymbol);
                    break;
                default:
                    throw new ArgumentException("can`t recognize token", nameof(token));
            }
            return toReturn;

        }
    }
}
