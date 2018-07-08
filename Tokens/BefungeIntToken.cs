using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BefungeInterpreter.Tokens
{
    class BefungeIntToken : BefungeToken
    {
        public int Value { get; }
        public BefungeIntToken(int value,BefungeTokenType tokenType) : base(tokenType)
        {
            Value = value;
        }
        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
