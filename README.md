# picfit
An image resizing server written in C# .NET Core

Picfit is a reusable .NET Core server to upload images with scale preprocessing.
I selected preprocessing workflow beacause postprocessing more expensive.
Picfit will supports multiple storage backends and multiple processing implementations.

I could not come up with a good name for this application so I used *picfit*  from : https://github.com/thoas/picfit

# Installation

1. Download and install .net core 2.2, git
2. Download it: ``` git clone https://github.com/spqr911/picfit.git ```
3. Build it: ```dotnet build```
4. Run it: ```dotnet run```

# Configuration

To configure application, you should change ```appsettings.json```

```

{
  "kvstore": {
    "type": "redis", // redis | cache
    "redis": {
      "host": "127.0.0.1",
      "port": "16379"
    }
  },
  "storage": {
    "type": "fs", // fs
    "location": "D:\\git\\picfit\\_static" // path/to/directory
  },
  "imagepreprocessing": {
    "type": "imagesharp", // imagesharp
    "scales": [
      75,
      100,
      150,
      200
    ]
  },
  "Logging": {
    "LogLevel": {
      "Default": "Warning"
    }
  },
  "AllowedHosts": "*"
}

```



