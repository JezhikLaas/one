module One.Parser.OneTokenizer

    open Superpower.Parsers
    open Superpower.Tokenizers
    open One.Parser.BooleanLiteral
    open One.Parser.FloatingPointLiteral
    open One.Parser.Identifier
    open One.Parser.IntegerLiteral
    open One.Parser.StringLiteral

    let builder = new TokenizerBuilder<OneToken>()

    let tokenizer =
        builder
            .Ignore(Span.WhiteSpace)
            // keywords
            .Match(Span.EqualTo("and"), OneToken.And)
            .Match(Span.EqualTo("class"), OneToken.Class)
            .Match(Span.EqualTo("check"), OneToken.Check)
            .Match(Span.EqualTo("create"), OneToken.Create)
            .Match(Span.EqualTo("Current"), OneToken.Current)
            .Match(Span.EqualTo("do"), OneToken.Do)
            .Match(Span.EqualTo("end"), OneToken.End)
            .Match(Span.EqualTo("ensure"), OneToken.Ensure)
            .Match(Span.EqualTo("inspect"), OneToken.Inspect)
            .Match(Span.EqualTo("invariant"), OneToken.Invariant)
            .Match(Span.EqualTo("not"), OneToken.Not)
            .Match(Span.EqualTo("or"), OneToken.Or)
            .Match(Span.EqualTo("require"), OneToken.Require)
            .Match(Span.EqualTo("when"), OneToken.When)
            .Match(Span.EqualTo("xor"), OneToken.Xor)
            // recognizers
            .Match(booleanLiteralRecognizer, OneToken.BooleanLiteral)
            .Match(decimalLiteralRecognizer, OneToken.DecimalLiteral)
            .Match(doubleLiteralRecognizer, OneToken.DoubleLiteral)
            .Match(floatLiteralRecognizer, OneToken.FloatLiteral)
            .Match(integerLiteralRecognizer, OneToken.IntegerLiteral)
            .Match(lowerCaseIdentifierRecognizer, OneToken.LowerCaseIdentifier)
            .Match(stringLiteralRecognizer, OneToken.StringLiteral)
            .Match(upperCaseIdentifierRecognizer, OneToken.UpperCaseIdentifier)
            .Match(upperCaseWithUnderscoreIdentifierRecognizer, OneToken.UpperCaseWithUnderscoreIdentifier)
            .Build()