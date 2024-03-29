﻿// See https://aka.ms/new-console-template for more information


using SoftTouch.ECS;
using SoftTouch.ECS.Example;
using SoftTouch.ECS.Querying;
using SoftTouch.ECS.Storage;
using System.Diagnostics;


var app =
    new App()
    .AddStartupProcessor<StartupProcessor>()
    .AddProcessor<SayHello>()
    .AddProcessor<WriteAge>();

app.Run();

