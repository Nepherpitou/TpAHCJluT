using System;
using System.Collections.Generic;
using System.Linq;

namespace TpaHCJluT.modules
{
    internal interface ITextTransformer
    {
        string TransformText(string text);
    }

    internal class TextTransformer : ITextTransformer
    {
        private readonly Dictionary<char, IEnumerable<string>> _possibleTransformations = new Dictionary<char, IEnumerable<string>>
        {
            {
                'а', new List<string>
                {
                    "A", "@"
                }
            },
            {
                'б', new List<string>
                {
                    "6"
                }
            },
            {
                'в', new List<string>
                {
                    "B"
                }
            },
            {
                'г', new List<string>
                {
                    "r"
                }
            },
            {
                'д', new List<string>
                {
                    "D"
                }
            },
            {
                'е', new List<string>
                {
                    "e"
                }
            },
            {
                'ё', new List<string>
                {
                    "e"
                }
            },
            {
                'ж', new List<string>
                {
                    "}l{", ">I<"
                }
            },
            {
                'з', new List<string>
                {
                    "3"
                }
            },
            {
                'и', new List<string>
                {
                    "u", "N"
                }
            },
            {
                'й', new List<string>
                {
                    "j"
                }
            },
            {
                'к', new List<string>
                {
                    "l{", "k"
                }
            },
            {
                'л', new List<string>
                {
                    "Jl", "JI", "/l"
                }
            },
            {
                'м', new List<string>
                {
                    "M"
                }
            },
            {
                'н', new List<string>
                {
                    "H"
                }
            },
            {
                'о', new List<string>
                {
                    "0", "O", "o"
                }
            },
            {
                'п', new List<string>
                {
                    "I7"
                }
            },
            {
                'р', new List<string>
                {
                    "p", "P"
                }
            },
            {
                'с', new List<string>
                {
                    "C", "c"
                }
            },
            {
                'т', new List<string>
                {
                    "T"
                }
            },
            {
                'у', new List<string>
                {
                    "y", "Y"
                }
            },
            {
                'ф', new List<string>
                {
                    "qp"
                }
            },
            {
                'х', new List<string>
                {
                    "x", "}{"
                }
            },
            {
                'ц', new List<string>
                {
                    "Lj"
                }
            },
            {
                'ч', new List<string>
                {
                    "4"
                }
            },
            {
                'ш', new List<string>
                {
                    "LLl"
                }
            },
            {
                'щ', new List<string>
                {
                    "LLj"
                }
            },
            {
                'ъ', new List<string>
                {
                    "`b"
                }
            },
            {
                'ы', new List<string>
                {
                    "bl"
                }
            },
            {
                'ь', new List<string>
                {
                    "b"
                }
            },
            {
                'э', new List<string>
                {
                    "3"
                }
            },
            {
                'ю', new List<string>
                {
                    "l-0"
                }
            },
            {
                'я', new List<string>
                {
                    "9l", "9", "ja"
                }
            }
        };
        private readonly Random _random;


        public TextTransformer()
        {
            _random = new Random(DateTime.Now.Millisecond);
        }

        public string TransformText(string text)
        {
            var result = string.Empty;
            foreach (var c in text)
            {
                if (_possibleTransformations.ContainsKey(c))
                {
                    result += _possibleTransformations[c].ElementAt(_random.Next(0, _possibleTransformations[c].Count()));
                }
                else
                {
                    result += c;
                }
            }
            return result;
        }
    }
}