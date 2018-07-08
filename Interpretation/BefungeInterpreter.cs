using BefungeInterpreter.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BefungeInterpreter.Interpretation
{
   public  class BefungeInterpreter
    {
        private Stack<int> _intStack = new Stack<int>();
        private bool _charMode = false;
        private Direction _direction = Direction.Right;
        private BefungeTable _table;
        private BefungePointer _pointer;
        private string output = "";

        public string Interpret(string code)
        {
            BefungeTable table = new BefungeTable(code);
            _table = table;
            _pointer = new BefungePointer(table);
            BefungeToken token = null;
            do
            {
                token = _pointer.GetToken();
                InterpretToken(token);
                _pointer.Move(_direction);
            } while (token.TokenType != BefungeTokenType.EOF);
            return output;
        }
        private void InterpretToken(BefungeToken token)
        {
            switch (token.TokenType)
            {
                case BefungeTokenType.Int:
                    _intStack.Push((token as BefungeIntToken).Value);
                    break;
                case BefungeTokenType.Char:
                    var tkn = (BefungeIntToken)token;
                    if (_charMode)
                        _intStack.Push(tkn.Value);
                    break;
                #region ariphmetic
                case BefungeTokenType.Add:
                    PerformAddition();
                    break;
                case BefungeTokenType.Divide:
                    PerformDivision();
                    break;
                case BefungeTokenType.Subtract:
                    PerformSubtraction();
                    break;
                case BefungeTokenType.Multiply:
                    PerformMultiplication();
                    break;
                case BefungeTokenType.Modulo:
                    PerformModulo();
                    break;
                #endregion
                #region directions
                case BefungeTokenType.MoveUp:
                    _direction = Direction.Up;
                    break;
                case BefungeTokenType.MoveDown:
                    _direction = Direction.Down;
                    break;
                case BefungeTokenType.MoveLeft:
                    _direction = Direction.Left;
                    break;
                case BefungeTokenType.MoveRight:
                    _direction = Direction.Right;
                    break;
                #endregion
                case BefungeTokenType.MoveRightConditional:
                    if (_intStack.Count == 0)
                    {
                        _direction = Direction.Right;
                        return;
                    }
                    _direction = _intStack.Pop() == 0 ? Direction.Right : Direction.Left;
                    break;
                case BefungeTokenType.MoveDownConditional:
                    if (_intStack.Count == 0)
                    {
                        _direction = Direction.Down;
                        return;
                    }
                    _direction = _intStack.Pop() == 0 ? Direction.Down : Direction.Up;
                    break;
                case BefungeTokenType.Quotes:
                    _charMode = !_charMode;
                    break;
                case BefungeTokenType.NOP:
                    return;
                case BefungeTokenType.MoveRandom:
                    var random = new Random().Next(100);
                    if (random < 25)
                        _direction = Direction.Right;
                    else if (random > 25 && random < 50)
                        _direction = Direction.Left;
                    else if (random > 50 && random < 75)
                        _direction = Direction.Up;
                    else if (random > 75)
                        _direction = Direction.Down;
                    break;
                case BefungeTokenType.Not:
                    var p = _intStack.Pop() == 0 ? 1 : 0;
                    _intStack.Push(p);
                    break;
                case BefungeTokenType.CopyTop:
                    if (_intStack.Count == 0)
                        return;
                    var copy = _intStack.Peek();
                    _intStack.Push(copy);
                    break;
                case BefungeTokenType.GreaterThan:
                    var a = _intStack.Pop();
                    var b = _intStack.Pop();
                    if (b > a)
                        _intStack.Push(0);
                    else
                        _intStack.Push(1);
                    break;
                case BefungeTokenType.SwapTop:
                    var top = _intStack.Pop();
                    var subTop = _intStack.Pop();
                    _intStack.Push(top);
                    _intStack.Push(subTop);
                    break;
                case BefungeTokenType.Pop:
                    _intStack.Pop();
                    break;
                case BefungeTokenType.PrintTopInt:
                    var i = _intStack.Pop();
                    output += i.ToString();
                    break;
                case BefungeTokenType.PrintTopSymbol:
                    var sym = _intStack.Pop();
                    output += ((char)sym).ToString();
                    break;
                case BefungeTokenType.Trampoline:
                    _pointer.Move(_direction);
                    break;
                case BefungeTokenType.Get:
                    var x = _intStack.Pop();
                    var y = _intStack.Pop();
                    try
                    {
                        var result = _table.GetTokenAt(x, y);
                        var asInt = result as BefungeIntToken;
                            _intStack.Push(asInt.Value);
                    }
                    catch (IndexOutOfRangeException)
                    {
                        _intStack.Push(0);
                    }
                    break;
                case BefungeTokenType.Put:
                    var x1 = _intStack.Pop();
                    var y1 = _intStack.Pop();
                    var v = _intStack.Pop();
                    _table.SetTokenValueAt(x1, y1, v);
                    break;
                case BefungeTokenType.GetInt:
                case BefungeTokenType.GetSymbol:
                    int i1 = int.Parse(Console.ReadLine());
                    _intStack.Push(i1);
                    break;
                case BefungeTokenType.EOF:
                    return;
                      
            }            
        }
        #region Ariphmetic
        private void PerformAddition()
        {
            var a = _intStack.Pop();
            var b = _intStack.Pop();
            var result = a + b;
            _intStack.Push(result);
        }
        private void PerformSubtraction()
        {
            var a = _intStack.Pop();
            var b = _intStack.Pop();
            var result = a - b;
            _intStack.Push(result);
        }
        private void PerformMultiplication()
        {
            var a = _intStack.Pop();
            var b = _intStack.Pop();
            var result = a * b;
            _intStack.Push(result);
        }
        private void PerformDivision()
        {
            var a = _intStack.Pop();
            var b = _intStack.Pop();
            var result = a / b;
            _intStack.Push(result);
        }
        private void PerformModulo()
        {
            var a = _intStack.Pop();
            var b = _intStack.Pop();
            var result = a % b;
            _intStack.Push(result);
        }
        #endregion
    }

}
