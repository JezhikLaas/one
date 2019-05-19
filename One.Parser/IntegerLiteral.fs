
module One.Parser.IntegerLiteral

    open System
    open Superpower
    open Superpower.Model
    open Superpower.Parsers
    
    let notZeroDigitParser = Character
                                 .In('1', '2', '3', '4', '5', '6', '7', '8', '9')
                                 .Select(fun item -> item.ToString())
    
    let notZeroDigitRecognizer = Character
                                    .In('1', '2', '3', '4', '5', '6', '7', '8', '9')
                                    .Value(Unit.Value)
    
    let twoDigitParserWithoutLeadingZeroParser =
        notZeroDigitParser
            .Repeat(1)
            .Then(fun firstDigit -> Character.Digit
                                        .Repeat(1)
                                        .Select(fun secondDigit -> firstDigit.[0] + secondDigit.[0].ToString()))
    
    let twoDigitParserWithoutLeadingZeroRecognizer =
        notZeroDigitRecognizer
            .Repeat(1)
            .Then(fun firstDigit -> Character.Digit
                                        .Repeat(1)
                                        .Value(Unit.Value))
    
    let threeDigitParserWithoutLeadingZeroParser =
        notZeroDigitParser
            .Repeat(1)
            .Then(fun firstDigit -> Character.Digit
                                        .Repeat(2)
                                        .Select(fun twoDigits -> firstDigit.[0] + new string(twoDigits)))
    
    let threeDigitParserWithoutLeadingZeroRecognizer =
        notZeroDigitRecognizer
            .Repeat(1)
            .Then(fun firstDigit -> Character.Digit
                                        .Repeat(2)
                                        .Value(Unit.Value))
    
    let leftMostThreeDigitGroupParser =
        threeDigitParserWithoutLeadingZeroParser.Try()
            .Or(twoDigitParserWithoutLeadingZeroParser.Try())
            .Or(notZeroDigitParser)
    
    let leftMostThreeDigitGroupRecognizer =
        threeDigitParserWithoutLeadingZeroRecognizer.Try()
            .Or(twoDigitParserWithoutLeadingZeroRecognizer.Try())
            .Or(notZeroDigitRecognizer)
    
    let threeDigitGroupParser =
        Character
            .Digit
            .Repeat(3)
            .Select(fun digits -> new string(digits))
    
    let threeDigitGroupRecognizer =
        Character
            .Digit
            .Repeat(3)
            .Value(Unit.Value)

    let threeDigitFollowUpParser =
        Character
            .EqualTo('_')
            .Then(fun char -> threeDigitGroupParser
                                  .Repeat(1)
                                  .Select(fun group -> String.Join(String.Empty, group)))

    let threeDigitFollowUpRecognizer =
        Character
            .EqualTo('_')
            .Then(fun char -> threeDigitGroupRecognizer
                                  .Repeat(1)
                                  .Value(Unit.Value))
           
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
           
    let notZeroIntegerRecognizer =
        Character
            .EqualTo('-')
            .Optional()
            .Then(fun optionalMinus -> leftMostThreeDigitGroupRecognizer
                                        .Repeat(1)
                                        .Then(fun head -> threeDigitFollowUpRecognizer
                                                            .Many()
                                                            .Value(Unit.Value)
                                        )
             )
    
    let zeroIntegerParser =
        Character
            .EqualTo('0')
            .Repeat(1)
            .Select(fun zero -> zero.[0].ToString())
    
    let zeroIntegerRecognizer =
        Character
            .EqualTo('0')
            .Repeat(1)
            .Value(Unit.Value)
    
    let integerLiteralParser =
        notZeroIntegerParser.Try().Or(zeroIntegerParser)
    
    let integerLiteralRecognizer =
        notZeroIntegerRecognizer.Try().Or(zeroIntegerRecognizer)
