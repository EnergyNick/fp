﻿using System.Collections.Generic;
using ResultPattern;

namespace SyntaxTextParser.Architecture
{
    public abstract class BaseElementParser
    {
        protected readonly IElementFormatter ElementFormatter;

        protected BaseElementParser(IElementFormatter elementFormatter)
        {
            ElementFormatter = elementFormatter;
        }

        public abstract Result<List<TextElement>> ParseElementsFromText(string text);
    }
}