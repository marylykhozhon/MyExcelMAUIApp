grammar LabCalculator;

compileUnit: expression EOF;

expression
    : '(' expression ')'                                  # ParenthesesExpr
    | operatorToken=(INC | DEC) expression                # IncDecExpr
    | NOT expression                                      # NotExpr
    | expression operatorToken=(LT | GT | EQ) expression  # RelationalExpr
    | expression operatorToken=(AND | OR) expression      # LogicalExpr
    | NUMBER                                              # NumberExpr
    | CELL                                                # CellExpr
    ;

INC: 'inc';
DEC: 'dec';
NOT: 'not';
AND: 'and';
OR: 'or';
LT: '<';
GT: '>';
EQ: '==';

NUMBER: [0-9]+ ('.' [0-9]+)?;
CELL: [A-Z]+ [0-9]+;
WS: [ \t\r\n]+ -> skip;