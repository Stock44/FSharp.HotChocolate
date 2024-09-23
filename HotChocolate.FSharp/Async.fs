﻿namespace HotChocolate

open Microsoft.FSharp.Core
open HotChocolate.Configuration
open HotChocolate.Types.Descriptors.Definitions


/// This type interceptor adds support for Async<_> fields.
type FSharpAsyncTypeInterceptor() =
    inherit TypeInterceptor()

    override this.OnBeforeRegisterDependencies(discoveryContext, definition) =
        match definition with
        | :? ObjectTypeDefinition as objectDef ->
            objectDef.Fields |> Seq.iter (convertAsyncToTask discoveryContext.TypeInspector)
        | _ -> ()
