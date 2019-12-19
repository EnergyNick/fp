﻿using SyntaxTextParser;
using System.Drawing;
using ResultPattern;
using TagsCloudGenerator.CloudPrepossessing;

namespace TagsCloudGenerator
{
    public abstract class CloudBuilder<T>
    {
        protected readonly TextParser Parser;
        protected readonly ITagsPrepossessing TagPlacer;

        protected CloudBuilder(TextParser parser,
            ITagsPrepossessing tagPlacer)
        {
            Parser = parser;
            TagPlacer = tagPlacer;
        }

        public abstract Result<T> CreateTagCloudRepresentation(string fullPath, Size imageSize, CloudSettings settings);
    }
}