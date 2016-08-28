using ConsoleCore.Interfaces;

namespace ConsoleCore
{
    public class CaretPosition : ICaretPosition
    {
        public int LineStart { get; set; }
        public int CommandStart { get; set; }

        private readonly Console _console;

        public CaretPosition(Console c)
        {
            _console = c;
        }

        public void Reset()
        {
            LineStart = _console.Text.Length + 1;
            CommandStart = LineStart + _console.Name.Length + 3;
        }
    }
}
