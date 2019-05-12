module One.Parser.FloatingPointLiteral

    open System
    open One.Parser.IntegerLiteral
    open Superpower
    open Superpower.Parsers
    
    let notZeroDigitParser = Character
                                 .In('1', '2', '3', '4', '5', '6', '7', '8', '9')
    
    let fractionalPartParser =
        Character.EqualTo('0')
            .Many()
            .Then(fun digits -> notZeroDigitParser
                                    .AtLeastOnce()
                                    .Select(fun notZeroDigits -> new string(digits) + new string(notZeroDigits)))
            .Then(fun head -> Character.Digit.Many().Select(fun tail -> head + new string(tail)))
            .Try()
            .Or(Character.EqualTo('0').Select(fun _ -> "0"))

    let decimalLiteralParser =
        integerLiteralParser
            .Then(fun integer -> Character.EqualTo('.')
                                    .Then(fun head -> fractionalPartParser
                                                          .Select(fun tail -> integer + "." + tail)))

    let doubleLiteralParser =
        decimalLiteralParser.Then(fun floating -> Character.EqualTo('d').Select(fun _ -> floating + "d"))

    let floatLiteralParser =
        decimalLiteralParser.Then(fun floating -> Character.EqualTo('f').Select(fun _ -> floating + "f"))