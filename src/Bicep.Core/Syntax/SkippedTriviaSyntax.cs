﻿// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Bicep.Core.Diagnostics;
using Bicep.Core.Extensions;
using Bicep.Core.Parser;

namespace Bicep.Core.Syntax
{
    public class SkippedTriviaSyntax : SyntaxBase
    {
        public SkippedTriviaSyntax(TextSpan span, IEnumerable<TokenOrSyntax> elements, IEnumerable<Diagnostic> diagnostics)
        {
            this.Span = span;
            this.Elements = elements.ToImmutableArray();
            this.Diagnostics = diagnostics.ToImmutableArray();
        }

        public SkippedTriviaSyntax(Token token, Diagnostic diagnostic)
        {
            this.Span = token.Span;
            this.Elements = new TokenOrSyntax(token).AsEnumerable().ToImmutableArray();
            this.Diagnostics = diagnostic.AsEnumerable().ToImmutableArray();
        }

        public override bool IsSkipped => true;

        /// <summary>
        /// The elements that were skipped.
        /// </summary>
        public ImmutableArray<TokenOrSyntax> Elements { get; }

        /// <summary>
        /// Diagnostics to raise.
        /// </summary>
        public ImmutableArray<Diagnostic> Diagnostics { get; }

        public override void Accept(SyntaxVisitor visitor)
            => visitor.VisitSkippedTriviaSyntax(this);

        public override TextSpan Span { get; }
    }
}