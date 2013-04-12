# Enku - F# Lightweight Web API Framework

Enku provides Web API in a F#-way.

- Simple
- Powerful
- Functional

```fsharp
route "example/{?id}" <| fun _ -> 
  [ 
    get, fun req res -> async {
      return res.ok "Accept GET" [] }

    put <|> post, fun req res -> async {
      return res.ok "Accept PUT or POST" [] }

    delete, fun req res -> async {
      return res.ok "Accept DELETE" [] }

    any, fun req res -> async {
      return res.ok "Accept any HTTP methods" [] }
  ]
```

## Install

N/A

## Examples

N/A

## Quick Start

N/A

## License

Apache License, Version 2.0

http://www.apache.org/licenses/LICENSE-2.0.html