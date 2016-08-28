using System.Drawing;

namespace ConsoleCore
{
    public class ConsoleSettings
    {
        private Color _consoleColor = Color.FromArgb(70, 131, 187);
        public Color ConsoleColor
        {
            get
            {
                return _consoleColor;
            }
            set
            {
                _consoleColor = value;
            }
        }

        private Color _backColor = Color.Black;
        public Color BackColor
        {
            get
            {
                return _backColor;
            }
            set
            {
                _backColor = value;
            }
        }

        private Color _foreColor = Color.FromArgb(45, 158, 187);
        public Color ForeColor
        {
            get
            {
                return _foreColor;
            }
            set
            {
                _foreColor = value;
            }
        }
        private Font _font = new Font(new FontFamily("Consolas"), 20, FontStyle.Regular);
        public Font Font
        {
            get
            {
                return _font;
            }
            set
            {
                _font = value;
            }
        }
    }
}
