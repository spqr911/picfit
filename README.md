# picfit
An image resizing server written in C# .NET Core

Picfit is a reusable .NET Core server to upload images with scale preprocessing.
I selected preprocessing workflow beacause postprocessing more expensive.
Picfit will supports multiple storage backends and multiple processing implementations.

I could not come up with a good name for this application so I used *picfit*  from : https://github.com/thoas/picfit

# Installation

1. Download and install .net core 2.2, git
2. Download it:
``` git clone https://github.com/spqr911/picfit.git ```
3. Run it:
```dotnet build```