namespace One.Parser

    type OneToken =
        BooleanLiteral
        | DecimalLiteral
        | DoubleLiteral
        | FloatLiteral
        | IntegerLiteral
        | LowerCaseIdentifier
        | StringLiteral
        | UpperCaseIdentifier
        | UpperCaseWithUnderscoreIdentifier