module One.Parser.Identifier

    open System
    open Superpower
    open Superpower.Model
    open Superpower.Parsers

    let lowerCaseIdentifierParser =
        Character.Lower
            .Then(fun head -> Character.LetterOrDigit.Many()
                                .Select(fun tail -> head.ToString() + new string(tail)))
    
    let lowerCaseIdentifierRecognizer =
        Character.Lower.Value(Unit.Value)
            .Then(fun head -> Character.LetterOrDigit
                                  .Many()
                                  .Value(Unit.Value))
    
    let upperCaseIdentifierParser =
        Character.Upper
            .Then(fun head -> Character.LetterOrDigit.Many()
                                .Select(fun tail -> head.ToString() + new string(tail)))    

    
    let upperCaseIdentifierRecognizer =
        Character.Upper.Value(Unit.Value)
            .Then(fun head -> Character.LetterOrDigit
                                  .Many()
                                  .Value(Unit.Value))    

    let upperCaseWithUnderscoreIdentifierParser =
        Character.Upper
            .Then(fun head -> Character.Lower.Try().Or(Character.Digit).Select(fun character -> character.ToString())
                                  .Try()
                                  .Or(Character.EqualTo('_')
                                          .Then(fun underscore -> Character.Upper.Try().Or(Character.Digit))
                                          .Select(fun character -> "_" + (character.ToString()))
                                  )
                                  .Many()
                                .Select(fun tail -> head.ToString() + String.Join(String.Empty, tail)))
    
    let upperCaseWithUnderscoreIdentifierRecognizer =
        Character.Upper.Value(Unit.Value)
            .Then(fun head -> Character.Lower.Try().Or(Character.Digit).Value(Unit.Value)
                                  .Try()
                                  .Or(Character.EqualTo('_')
                                          .Then(fun underscore -> Character.Upper.Try().Or(Character.Digit))
                                          .Value(Unit.Value)
                                  )
                                  .Many()
                                .Value(Unit.Value))

    let lowerCaseWithUnderscoreIdentifierParser =
        Character.Lower
            .Then(fun head -> Character.Lower.Try().Or(Character.Digit).Select(fun character -> character.ToString())
                                  .Try()
                                  .Or(Character.EqualTo('_')
                                          .Then(fun underscore -> Character.Lower.Try().Or(Character.Digit))
                                          .Select(fun character -> "_" + (character.ToString()))
                                  )
                                  .Many()
                                .Select(fun tail -> head.ToString() + String.Join(String.Empty, tail)))
    
    let lowerCaseWithUnderscoreIdentifierRecognizer =
        Character.Lower.Value(Unit.Value)
            .Then(fun head -> Character.Lower.Try().Or(Character.Digit).Value(Unit.Value)
                                  .Try()
                                  .Or(Character.EqualTo('_')
                                          .Then(fun underscore -> Character.Lower.Try().Or(Character.Digit))
                                          .Value(Unit.Value)
                                  )
                                  .Many()
                                .Value(Unit.Value))
