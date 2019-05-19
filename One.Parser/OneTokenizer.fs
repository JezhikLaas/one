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