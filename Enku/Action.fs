﻿//----------------------------------------------------------------------------
//
// Copyright (c) 2013, Toshihiro Nakamura.
//
// This source code is subject to terms and conditions of the Apache License, Version 2.0. A 
// copy of the license can be found in the LICENSE.txt file at the root of this distribution. 
// By using this source code in any fashion, you are agreeing to be bound 
// by the terms of the Apache License, Version 2.0.
//
// You must not remove this notice, or any other, from this software.
//----------------------------------------------------------------------------

namespace Enku

open System.Net.Http

[<RequireQualifiedAccess>]
[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module Action = 

  let make predicate = Action(fun req res body ->
    if predicate req then
      Right <| body req res
    else 
      Left())

  let run req res body (Action f) = f req res body

[<AutoOpen>]
module ActionDirectives =

  let private isTargetRequest m (Request req) = 
    req.Method = m

  let get = Action.make (isTargetRequest HttpMethod.Get)
  let post = Action.make (isTargetRequest HttpMethod.Post)
  let put = Action.make (isTargetRequest HttpMethod.Put)
  let delete = Action.make (isTargetRequest HttpMethod.Delete)
  let head = Action.make (isTargetRequest HttpMethod.Head)
  let options = Action.make (isTargetRequest HttpMethod.Options)
  let trace = Action.make (isTargetRequest HttpMethod.Trace)
  let patch = Action.make (isTargetRequest <| HttpMethod "PATCH")
  let any = Action.make (fun _ -> true)

  let (<|>) (x: Action) (y: Action) = Action(fun req res body ->
      match Action.run req res body x with
      | Left _ -> Action.run req res body y
      | Right r -> Right r )