dotnet publish .\Examples.Wasm\Examples.Wasm.csproj -c Debug
dotnet serve -p 42069 --directory Examples.Wasm\bin\Debug\net7.0\browser-wasm\AppBundle\
