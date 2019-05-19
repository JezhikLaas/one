module One.Parser.FloatingPointLiteral

    open System
    open One.Parser.IntegerLiteral
    open Superpower
    open Superpower.Model
    open Superpower.Parsers
    
    let notZeroDigitParser = Character
                                 .In('1', '2', '3', '4', '5', '6', '7', '8', '9')
    
    let notZeroDigitRecognizer = Character
                                   .In('1', '2', '3', '4', '5', '6', '7', '8', '9')
                                   .Value(Unit.Value)
    
    let fractionalPartParser =
        Character.EqualTo('0')
            .Many()
            .Then(fun digits -> notZeroDigitParser
                                    .AtLeastOnce()
                                    .Select(fun notZeroDigits -> new string(digits) + new string(notZeroDigits)))
            .Then(fun head -> Character.Digit.Many().Select(fun tail -> head + new string(tail)))
            .Try()
            .Or(Character.EqualTo('0').Select(fun _ -> "0"))

    
    let fractionalPartRecognizer =
        Character.EqualTo('0')
            .Many()
            .Then(fun digits -> notZeroDigitRecognizer
                                    .AtLeastOnce())
            .Then(fun head -> Character.Digit.Many().Value(Unit.Value))
            .Try()
            .Or(Character.EqualTo('0').Value(Unit.Value))

    let decimalLiteralParser =
        integerLiteralParser
            .Then(fun integer -> Character.EqualTo('.')
                                    .Then(fun head -> fractionalPartParser
                                                          .Select(fun tail -> integer + "." + tail)))

    let decimalLiteralRecognizer =
        integerLiteralRecognizer
            .Then(fun integer -> Character.EqualTo('.').Value(Unit.Value)
                                    .Then(fun head -> fractionalPartRecognizer))

    let doubleLiteralParser =
        decimalLiteralParser
            .Then(fun floating -> Character.EqualTo('d')
                                      .Select(fun _ -> floating + "d"))

    let doubleLiteralRecognizer =
        decimalLiteralRecognizer
            .Then(fun floating -> Character.EqualTo('d')
                                    .Value(Unit.Value))

    let floatLiteralParser =
        decimalLiteralParser
            .Then(fun floating -> Character.EqualTo('f')
                                    .Select(fun _ -> floating + "f"))

    let floatLiteralRecognizer =
        decimalLiteralRecognizer
            .Then(fun floating -> Character.EqualTo('f')
                                    .Value(Unit.Value))