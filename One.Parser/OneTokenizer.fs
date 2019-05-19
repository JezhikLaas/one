module One.Parser.OneTokenizer

    open Superpower.Parsers
    open Superpower.Tokenizers
    open One.Parser.BooleanLiteral

    let builder = new TokenizerBuilder<OneToken>()

    let tokenizer =
        builder
            .Ignore(Span.WhiteSpace)
            .Match(booleanLiteralRecognizer, OneToken.BooleanLiteral)
            .Build()