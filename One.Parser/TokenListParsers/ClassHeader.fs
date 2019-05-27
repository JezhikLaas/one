module One.Parser.TokenListParsers.ClassHeader

    open One.Parser
    open Superpower
    open Superpower.Parsers
    
    let ancestorList =
        Token
            .EqualTo(OneToken.UpperCaseIdentifier)
            .Apply(Identifier.upperCaseIdentifierParser)
            .AtLeastOnceDelimitedBy(Token.EqualTo(OneToken.Semicolon).Or(Token.EqualTo(OneToken.LineFeed)))
            .Select(fun items -> items |> Array.toList)
    
    let classHeader =
        Token
            .EqualTo(OneToken.Class)
            .Then(fun _ -> Token
                               .EqualTo(OneToken.UpperCaseIdentifier)
                               .Apply(Identifier.upperCaseIdentifierParser)
                               .Then(fun name -> Token
                                                     .EqualTo(OneToken.Inherit)
                                                     .Then(fun _ -> ancestorList
                                                                        .Select(fun ancestors -> ClassHeader (name, ancestors)))))

    