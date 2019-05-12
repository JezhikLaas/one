
module One.Parser.IntegerLiteral

    open System
    open Superpower
    open Superpower.Parsers
    
    let notZeroDigitParser = Character
                                 .In('1', '2', '3', '4', '5', '6', '7', '8', '9')
                                 .Select(fun item -> item.ToString())
    
    let twoDigitParserWithoutLeadingZeroParser =
        notZeroDigitParser
            .Repeat(1)
            .Then(fun firstDigit -> Character.Digit
                                        .Repeat(1)
                                        .Select(fun secondDigit -> firstDigit.[0] + secondDigit.[0].ToString()))
    
    let threeDigitParserWithoutLeadingZeroParser =
        notZeroDigitParser
            .Repeat(1)
            .Then(fun firstDigit -> Character.Digit
                                        .Repeat(2)
                                        .Select(fun twoDigits -> firstDigit.[0] + new string(twoDigits)))
    
    let leftMostThreeDigitGroupParser =
        threeDigitParserWithoutLeadingZeroParser.Try()
            .Or(twoDigitParserWithoutLeadingZeroParser.Try())
            .Or(notZeroDigitParser)
    
    let threeDigitGroupParser =
        Character
            .Digit
            .Repeat(3)
            .Select(fun digits -> new string(digits))

    let threeDigitFollowUpParser =
        Character
            .EqualTo('_')
            .Then(fun char -> threeDigitGroupParser
                                  .Repeat(1)
                                  .Select(fun group -> String.Join(String.Empty, group)))
           
    let notZeroIntegerParser =
        Character
            .EqualTo('-')
            .Optional()
            .Then(fun optionalMinus -> leftMostThreeDigitGroupParser
                                        .Repeat(1)
                                        .Then(fun head -> threeDigitFollowUpParser
                                                            .Many()
                                                            .Select(fun tail -> match Option.ofNullable optionalMinus with
                                                                                | Some minus -> minus.ToString() + head.[0] + String.Join("", tail)
                                                                                | None       -> head.[0] + String.Join("", tail)
                                                            )
                                        )
             )
    
    let zeroIntegerParser =
        Character
            .EqualTo('0')
            .Repeat(1)
            .Select(fun zero -> zero.[0].ToString())
    
    let integerParser =
        notZeroIntegerParser.Try().Or(zeroIntegerParser)
