module One.Parser.TokenListParsers

    open One.Parser
    open Superpower
    open Superpower.Parsers
    
    let ancestorList =
        Token
            .EqualTo(OneToken.UpperCaseIdentifier)
            .Apply(Identifier.upperCaseIdentifierParser)
            .AtLeastOnceDelimitedBy(Token.EqualTo(OneToken.Comma))
            .Select(fun items -> items |> Array.toList)
    
    let classHeader =
        Token
            .EqualTo(OneToken.Class)
            .Then(fun _ -> Token
                               .EqualTo(OneToken.UpperCaseIdentifier)
                               .Apply(Identifier.upperCaseIdentifierParser)
                               .Then(fun name -> Token
                                                     .EqualTo(OneToken.Colon)
                                                     .Then(fun _ -> ancestorList
                                                                        .Select(fun ancestor -> ClassHeader (name, ancestor)))))

    