using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BefungeInterpreter.Tokens
{
    enum BefungeTokenType
    {
        //-----------------------------//
        //    movement tokens
        //-----------------------------//
                MoveLeft, 
                MoveRight, 
                MoveDown, 
                MoveUp,
                MoveRandom, 
                MoveDownConditional,
                MoveRightConditional,
                Trampoline,
                EOF,
        //-----------------------------//
        //    stack manipulation tokens
        //-----------------------------//
               CopyTop,
               SwapTop,
               RemoveTop,
               PutInt,
               Put,
               Get,
               Pop,
               NOP,
        //-----------------------------//
        //    stack ariphmetic tokens
        //-----------------------------//
                Add,
                Subtract,
                Multiply,
                Divide,
                Modulo,
                IntDivide,
        //-----------------------------//
        //    stack logical tokens
        //-----------------------------//
                Not,
                GreaterThan,
        //-----------------------------//
        //    I/O  tokens
        //-----------------------------//
                GetInt,
                GetSymbol,
                PrintTopInt,
                PrintTopSymbol,
        //-----------------------------//
        //    Datatype tokens
        //-----------------------------//
                Int,
                Char,
                Quotes,

    }
}
