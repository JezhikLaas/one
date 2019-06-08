namespace One.Parser
    
    type OneNode =
        | ClassHeader of string * string list
        | Invariant of (string * string) list
        | StaticCall of string * string
        | ObjectCall of string * string