module One.Parser.StringLiteral

    open System
    open Superpower
    open Superpower.Model
    open Superpower.Parsers
    
    let esacapeSequenceParser = 
        Character.EqualTo('\\')
            .Then(fun head -> Character.EqualTo('"') 
                                .Or(Character.EqualTo('\\'))
                                .Or(Character.EqualTo('b'))
                                .Or(Character.EqualTo('f'))
                                .Or(Character.EqualTo('n'))
                                .Or(Character.EqualTo('r'))
                                .Or(Character.EqualTo('t'))
                                .Select(fun character -> head.ToString() + character.ToString())
                                .Or(Character.EqualTo('u')
                                .Then(fun character -> Span.MatchedBy(Character.HexDigit.Repeat(4))
                                                        .Select(fun characters -> characters.Source))))
                                .Named("escape sequence")
    
    let esacapeSequenceRecognizer = 
        Character.EqualTo('\\').Value(Unit.Value)
            .Then(fun head -> Character.EqualTo('"').Value(Unit.Value) 
                                .Or(Character.EqualTo('\\').Value(Unit.Value))
                                .Or(Character.EqualTo('b').Value(Unit.Value))
                                .Or(Character.EqualTo('f').Value(Unit.Value))
                                .Or(Character.EqualTo('n').Value(Unit.Value))
                                .Or(Character.EqualTo('r').Value(Unit.Value))
                                .Or(Character.EqualTo('t').Value(Unit.Value))
                                .Value(Unit.Value)
                                .Or(Character.EqualTo('u').Value(Unit.Value)
                                .Then(fun character -> Span.MatchedBy(Character.HexDigit.Repeat(4))
                                                        .Value(Unit.Value))))
                                .Named("escape sequence")
    
    let stringLiteralParser =
            Character.EqualTo('"').Then(fun _ ->
                Character
                    .ExceptIn('"', '\\')
                    .Select(fun character -> character.ToString())
                    .Try()
                    .Or(esacapeSequenceParser.Try()).Many())
                .Then(fun strings -> Character.EqualTo('"').Select(fun _ -> "\"" + String.Join(String.Empty, strings) + "\""))
    
    let stringLiteralRecognizer =
            Character.EqualTo('"').Value(Unit.Value).Then(fun _ ->
                Character
                    .ExceptIn('"', '\\')
                    .Value(Unit.Value)
                    .Try()
                    .Or(esacapeSequenceRecognizer.Try()).Many())
                .Then(fun strings -> Character.EqualTo('"').Value(Unit.Value))
