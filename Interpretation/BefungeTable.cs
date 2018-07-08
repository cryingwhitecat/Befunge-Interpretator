using BefungeInterpreter.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BefungeInterpreter
{
    class BefungeTable
    {
        private BefungeLexer _lexer = new BefungeLexer();
        private BefungeToken[][] _tokens;
        public bool IsEmpty { get; private set; } = true;
        public BefungeTable(string code)
        {
            _tokens = _lexer.ParseRawInput(code);
            IsEmpty = false;
        }
        public BefungeToken GetTokenAt(int x,int y)
        {
            BefungeToken toReturn = null;
            try { toReturn= _tokens[y][x]; }
            catch(Exception)
            {
                toReturn = new BefungeIntToken(0, BefungeTokenType.Int);
            }
            return toReturn;
        }
        public void SetTokenValueAt(int x,int y,int value)
        {
            var tkn = new BefungeIntToken(value, BefungeTokenType.Int);
            _tokens[y][x] = tkn;
        }
        private int GetShortestRow()
        {
            int shortest = int.MaxValue ;
            for (int i = 0; i < _tokens.GetLength(0); i++)
            {
                if (_tokens[i].Length <shortest)
                    shortest= _tokens[i].Length;
            }
            return shortest;
        }
    }
}
