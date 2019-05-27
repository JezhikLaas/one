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
        | [<Token(Example = "inherit")>] Inherit                                              =  19
        | IntegerLiteral                                                                      =  20
        | [<Token(Example = "inspect")>] Inspect                                              =  21
        | [<Token(Example = "invariant")>] Invariant                                          =  22
        | [<Token(Example = ">")>] Larger                                                     =  23
        | [<Token(Example = ">=")>] LargerOrEqual                                             =  24
        | [<Token(Example = "{")>] LeftBracket                                                =  25
        | [<Token(Example = "(")>] LeftParenthesis                                            =  26
        | [<Token(Example = "[")>] LeftSquareBracket                                          =  27
        | LineFeed                                                                            =  28
        | [<Token(Example = "identifier")>] LowerCaseIdentifier                               =  29
        | [<Token(Example = "identifier_with_underscore")>] LowerCaseWithUnderscoreIdentifier =  30
        | [<Token(Example = "not")>] Not                                                      =  31
        | [<Token(Example = "<>")>] NotEqual                                                  =  32
        | [<Token(Example = "or")>] Or                                                        =  33
        | [<Token(Example = "|")>] Pipe                                                       =  34
        | [<Token(Example = ".")>] Point                                                      =  35
        | [<Token(Example = "require")>] Require                                              =  36
        | [<Token(Example = "}")>] RightBracket                                               =  37
        | [<Token(Example = ")")>] RightParenthesis                                           =  38
        | [<Token(Example = "]")>] RightSquareBracket                                         =  39
        | [<Token(Example = ";")>] Semicolon                                                  =  41
        | [<Token(Example = "<")>] Smaller                                                    =  42
        | [<Token(Example = "<=")>] SmallerOrEqual                                            =  43
        | StringLiteral                                                                       =  44
        | [<Token(Example = "Identifier")>] UpperCaseIdentifier                               =  45
        | [<Token(Example = "Identifier_With_Underscore")>] UpperCaseWithUnderscoreIdentifier =  46
        | [<Token(Example = "watch")>] Watch                                                  =  47
        | [<Token(Example = "when")>] When                                                    =  48
        | [<Token(Example = "xor")>] Xor                                                      =  49
