module One.Parser.TokenListParsers.Expression

    open System
    open One.Parser
    open Superpower
    open Superpower.Parsers
    
    let unqualifiedCall =
        Token
            .EqualTo(OneToken.LowerCaseIdentifier)
            .Apply(Identifier.lowerCaseIdentifierParser)
    
    let nonObjectCall =
        Token
            .EqualTo(OneToken.LeftBracket)
            .Then(fun _ -> Token
                               .EqualTo(OneToken.UpperCaseIdentifier)
                               .Apply(Identifier.upperCaseIdentifierParser)
                               .Then(fun typename -> Token
                                                         .EqualTo(OneToken.RightBracket)
                                                         .Then(fun _ -> Token
                                                                            .EqualTo(OneToken.Point)
                                                                            .Then(fun _ -> Token
                                                                                               .EqualTo(OneToken.LowerCaseIdentifier)
                                                                                               .Apply(Identifier.lowerCaseIdentifierParser)
                                                                                               .Or(Token.EqualTo(OneToken.UpperCaseIdentifier).Apply(Identifier.upperCaseIdentifierParser))
                                                                                               .Select(fun feature -> StaticCall (typename, feature))))))
    
    let objectCall =
        Token
            .EqualTo(OneToken.LowerCaseIdentifier)
            .Apply(Identifier.lowerCaseIdentifierParser)
            .Then(fun _ -> Token.EqualTo(OneToken.Point))
            .Optional()
            .Then(fun _ -> unqualifiedCall)
            .Select(fun feature -> ObjectCall (String.Empty, feature))
    
    let call =
        objectCall.Or(nonObjectCall)

    let expression =
        call