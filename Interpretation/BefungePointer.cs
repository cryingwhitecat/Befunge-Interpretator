using BefungeInterpreter.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BefungeInterpreter.Interpretation
{
    class BefungePointer
    {
        private int _xpos = 0;
        private int _ypos = 0;
        private BefungeTable _table;
        public void Move(Direction direction)
        {
            if (direction == Direction.Up)
                --_ypos;
            if (direction == Direction.Down)
                ++_ypos;
            if (direction == Direction.Right)
                ++_xpos;
            if (direction == Direction.Left)
                --_xpos;
        }
        public BefungePointer(BefungeTable table)
        {
            _table = table;
            if (_table.IsEmpty)
                throw new ArgumentException("table must be initialized", nameof(table));
        }
        public BefungePointer(string code)
        {
            _table = new BefungeTable(code);
        }
        public BefungeToken GetToken()
        {
            return _table.GetTokenAt(_xpos, _ypos);
        }
    }
}
