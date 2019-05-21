namespace One.Parser

    open Superpower.Display
    
    type OneToken =
        | [<Token(Example = "and")>] And                                                      =   1
        | [<Token(Example = ":=")>] Assign                                                    =   2
        | [<Token(Example = "true | false")>] BooleanLiteral                                  =   3
        | [<Token(Category = "keyword", Example = "class")>] Class                            =   4
        | [<Token(Example = "check")>] Check                                                  =   5
        | [<Token(Example = ":")>] Colon                                                      =   6
        | [<Token(Example = ",")>] Comma                                                      =   7
        | [<Token(Example = "create")>] Create                                                =   8
        | [<Token(Example = "Current")>] Current                                              =   9
        | DecimalLiteral                                                                      =  10
        | [<Token(Example = "do")>] Do                                                        =  11
        | DoubleLiteral                                                                       =  12
        | [<Token(Example = "end")>] End                                                      =  13
        | [<Token(Example = "ensure")>] Ensure                                                =  14
        | [<Token(Example = "=")>] Equal                                                      =  15
        | FloatLiteral                                                                        =  16
        | [<Token(Example = "implies")>] Implies                                              =  17
        | IntegerLiteral                                                                      =  18
        | [<Token(Example = "inspect")>] Inspect                                              =  19
        | [<Token(Example = "invariant")>] Invariant                                          =  20
        | [<Token(Example = ">")>] Larger                                                     =  21
        | [<Token(Example = ">=")>] LargerOrEqual                                             =  22
        | [<Token(Example = "{")>] LeftBracket                                                =  23
        | [<Token(Example = "(")>] LeftParenthesis                                            =  24
        | [<Token(Example = "[")>] LeftSquareBracket                                          =  25
        | [<Token(Example = "identifier")>] LowerCaseIdentifier                               =  26
        | [<Token(Example = "not")>] Not                                                      =  27
        | [<Token(Example = "<>")>] NotEqual                                                  =  28
        | [<Token(Example = "or")>] Or                                                        =  29
        | [<Token(Example = "require")>] Require                                              =  30
        | [<Token(Example = "}")>] RightBracket                                               =  31
        | [<Token(Example = ")")>] RightParenthesis                                           =  32
        | [<Token(Example = "]")>] RightSquareBracket                                         =  33
        | [<Token(Example = "<")>] Smaller                                                    =  34
        | [<Token(Example = "<=")>] SmallerOrEqual                                            =  35
        | StringLiteral                                                                       =  36
        | [<Token(Example = "Identifier")>] UpperCaseIdentifier                               =  37
        | [<Token(Example = "Identifier_With_Underscore")>] UpperCaseWithUnderscoreIdentifier =  38
        | [<Token(Example = "when")>] When                                                    =  39
        | [<Token(Example = "xor")>] Xor                                                      =  40
