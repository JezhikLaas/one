module One.Parser.Identifier

    open Superpower
    open Superpower.Parsers

    let lowerCaseIdentifierParser =
        Character.Lower
            .Then(fun head -> Character.LetterOrDigit.Many())
            .Select(fun tail -> new string(tail))