module One.Parser.BooleanLiteral

    open System
    open Superpower
    open Superpower.Parsers
    
    let falseParser =
        Character
            .EqualTo('f')
            .Then(fun f -> Character.EqualTo('a')
                            .Then(fun a -> Character.EqualTo('l')
                                            .Then(fun l -> Character.EqualTo('s')
                                                            .Then(fun s -> Character.EqualTo('e')
                                                            )
                                             )
                            )
            )
            .Select(fun _ -> "false")    
    
    let trueParser =
        Character
            .EqualTo('t')
            .Then(fun f -> Character.EqualTo('r')
                            .Then(fun a -> Character.EqualTo('u')
                                            .Then(fun l -> Character.EqualTo('e')
                                             )
                            )
            )
            .Select(fun _ -> "true")
    
    let booleanLiteralParser =
        falseParser.Try().Or(trueParser)