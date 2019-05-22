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
        | [<Token(Example = "feature")>] Feature                                              =  16
        | FloatLiteral                                                                        =  17
        | [<Token(Example = "implies")>] Implies                                              =  18
        | IntegerLiteral                                                                      =  19
        | [<Token(Example = "inspect")>] Inspect                                              =  20
        | [<Token(Example = "invariant")>] Invariant                                          =  21
        | [<Token(Example = ">")>] Larger                                                     =  22
        | [<Token(Example = ">=")>] LargerOrEqual                                             =  23
        | [<Token(Example = "{")>] LeftBracket                                                =  24
        | [<Token(Example = "(")>] LeftParenthesis                                            =  25
        | [<Token(Example = "[")>] LeftSquareBracket                                          =  26
        | [<Token(Example = "identifier")>] LowerCaseIdentifier                               =  27
        | [<Token(Example = "not")>] Not                                                      =  28
        | [<Token(Example = "<>")>] NotEqual                                                  =  29
        | [<Token(Example = "or")>] Or                                                        =  30
        | [<Token(Example = "|")>] Pipe                                                       =  31
        | [<Token(Example = ".")>] Point                                                      =  32
        | [<Token(Example = "require")>] Require                                              =  33
        | [<Token(Example = "}")>] RightBracket                                               =  34
        | [<Token(Example = ")")>] RightParenthesis                                           =  35
        | [<Token(Example = "]")>] RightSquareBracket                                         =  36
        | [<Token(Example = "<")>] Smaller                                                    =  37
        | [<Token(Example = "<=")>] SmallerOrEqual                                            =  38
        | StringLiteral                                                                       =  39
        | [<Token(Example = "Identifier")>] UpperCaseIdentifier                               =  40
        | [<Token(Example = "Identifier_With_Underscore")>] UpperCaseWithUnderscoreIdentifier =  41
        | [<Token(Example = "watch")>] Watch                                                  =  42
        | [<Token(Example = "when")>] When                                                    =  43
        | [<Token(Example = "xor")>] Xor                                                      =  44
