import { dotnet } from "./dotnet.js";

await dotnet
    .withDebugging(1)
    .withDiagnosticTracing(true)
    .withApplicationArgumentsFromQuery()
    .create();

globalThis.dotnetwasm = dotnet;

let canvas = document.getElementById("canvas");

dotnet.instance.Module["canvas"] = canvas;

// We're ready to dotnet.run, so let's remove the spinner
const loading_div = document.getElementById("spinner");
loading_div.remove();

await dotnet.run();

let body = document.querySelector("body");

let exports = await dotnetwasm.instance.getAssemblyExports("Examples.Wasm");

function Update(dt) {
    if (exports.Examples.Wasm.Program.IsExample()) {
        dotnetwasm.instance.Module.setCanvasSize(800, 450, false);
        canvas.classList.add("border");
    } else {
        dotnetwasm.instance.Module.setCanvasSize(
            body.clientWidth,
            body.clientHeight,
            false
        );
        if (canvas.classList.contains("border")) {
            canvas.classList.remove("border");
        }
    }
    requestAnimationFrame(Update);
}

requestAnimationFrame(Update);
