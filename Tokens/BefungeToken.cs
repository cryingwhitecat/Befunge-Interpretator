using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BefungeInterpreter.Tokens
{
    class BefungeToken
    {
        public BefungeTokenType TokenType { get; }
        public BefungeToken(BefungeTokenType tokenType)
        {
            TokenType = tokenType;
        }
        public override string ToString()
        {
            return TokenType.ToString();
        }
    }
}
