# Circumflang

A metalanguage based on the circumflex (^).\
This repository contains the parsers in different languages for Circumflang.

## Circumflang Parsers

[C# parser](./Parsers/Circumflang.cs)

## Circumflang rules and syntax

Circumflang has 4 keywords:

| Keyword | Description                               |
|---------|-------------------------------------------|
| ^^^^    | Splits groups of elements                 |
| ^^^     | Splits elements                           |
| ^^      | Assigns one or more values to the element |
| ^       | Splits the values of the element          |

Circumflang element exemple: `ElementName ^^ value1 ^ value2`\
Circumflang group exemple: `element1  ^^ value ^^^ element2 ^^ hello ^^ 123`\
Circumflang data exemple: `element1  ^^ value ^^^ element2 ^^ hello ^^ 123 ^^^^ element1  ^^ value ^^^ element2 ^^ bye ^^ 123`\
\
Complete exemple:
```
id ^^ 5699 ^^^
keys ^^ kcfrdujkDDrt ^ TFURDersIKvj ^^^
info ^^
  Information on the data. ^
  Another information on the data. ^
  Another information on the data. ^
  Another information on the data.
  
^^^^

id ^^ 2901 ^^^
keys ^^ UtiLHICDYX ^ TFURDersIKvj ^^^
info ^^
  Information on the data. ^
  Another information on the data. ^
  Another information on the data. ^
  Another information on the data.
```

Elements names are case-insensitive!\
Spaces are not considered outside the values!\
Returns and newlines are never considered!
