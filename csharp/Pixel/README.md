# README

## Requirements

Install dotnet core <https://www.microsoft.com/net>

## Instructions

Start in the `csharp/Pixel` folder

Run `dotnet build` to build.

Run `dotnet run --project Pixel` to run the program.

Run `dotnet test Pixel.Tests` to test.

### Code Coverage

1. Install Coverlet <https://github.com/tonerdo/coverlet/>

        `dotnet tool install --global coverlet.console`

2. Install Report Generator <https://github.com/danielpalme/ReportGenerator>

        `dotnet tool install --global dotnet-reportgenerator-globaltool --version 4.0.0-rc6`

3. Run dotnet test with coverlet to generate the code coverage stats.

        `dotnet test Pixel.Tests /p:CollectCoverage=true /p:CoverletOutputFormat=opencover /p:CoverletOutput=./coverage.opencover.xml`

4. Run report generator to create the html report and then open a ./coverage/index.htm in a browser. For example, `xdg-open ./coverage/index.htm`

        `reportgenerator -reports:coverage.opencover.xml -targetdir:./coverage`

### Troubleshooting

* After running `reportgenerator` or another dotnet tool you see:

    > A fatal error occurred, the required library libhostfxr.so could not be found.

    Make sure `DOTNET_ROOT` is set. For example, `export DOTNET_ROOT=/opt/dotnet`
