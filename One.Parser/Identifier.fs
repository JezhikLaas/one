module One.Parser.Identifier

    open System
    open Superpower
    open Superpower.Parsers

    let lowerCaseIdentifierParser =
        Character.Lower
            .Then(fun head -> Character.LetterOrDigit.Many()
                                .Select(fun tail -> head.ToString() + new string(tail)))
    
    let upperCaseIdentifierParser =
        Character.Upper
            .Then(fun head -> Character.LetterOrDigit.Many()
                                .Select(fun tail -> head.ToString() + new string(tail)))    

    let upperCaseIdentifierWithUnderscoreParser =
        Character.Upper
            .Then(fun head -> Character.Lower.Try().Or(Character.Digit).Select(fun character -> character.ToString())
                                  .Try()
                                  .Or(Character.EqualTo('_')
                                          .Then(fun underscore -> Character.Upper.Try().Or(Character.Digit))
                                          .Select(fun character -> "_" + (character.ToString()))
                                  )
                                  .Many()
                                .Select(fun tail -> head.ToString() + String.Join(String.Empty, tail)))