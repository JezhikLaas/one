module One.Parser.TokenListParsers.Invariant

    open One.Parser
    open Superpower
    open Superpower.Parsers
    
    let booleanExpression =
        Token
            .EqualTo(OneToken.BooleanLiteral)
            .Apply(BooleanLiteral.booleanLiteralParser)
            .Select(fun value -> value)

    let taggedExpressionList =
        Token
            .EqualTo(OneToken.LowerCaseWithUnderscoreIdentifier)
            .Apply(Identifier.lowerCaseWithUnderscoreIdentifierParser)
            .Then(fun tag -> Token
                                 .EqualTo(OneToken.Colon)
                                 .Then(fun _ -> booleanExpression.Select(fun expression -> (tag, expression))))
            .AtLeastOnceDelimitedBy(Token.EqualTo(OneToken.Semicolon).Or(Token.EqualTo(OneToken.LineFeed)))
            .Select(fun items -> items |> Array.toList)
    
    let invariant =
        Token
            .EqualTo(OneToken.Invariant)
            .Then(fun _ -> Token.EqualTo(OneToken.LineFeed))
            .Then(fun _ -> taggedExpressionList.Select(fun expressions -> Invariant expressions))