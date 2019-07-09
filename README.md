# Picfit
An lightweight image resizing server written in C# .NET Core

Picfit is a reusable .NET Core server to upload images with scale preprocessing.
I selected preprocessing workflow beacause postprocessing more expensive.
Picfit will supports multiple storage backends and multiple processing implementations.

I could not come up with a good name for this application so I used [picfit](https://github.com/thoas/picfit) name

# Open Source
If you want to improve this application, join the team:)

# Server API

Picfit has restfull interface with [Swagger](https://github.com/domaindrivendev/Swashbuckle) documentation.

# Installation

1. Download and install .net core 2.2, git
2. Download it: ` git clone https://github.com/spqr911/picfit.git `
3. Build it:`dotnet build`
4. Run it:`dotnet run`

# Configuration

To configure application, you should change `appsettings.json`

```
{
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
}
```

`storage` can be:

* type:fs - generated images stored in your File system
* type:s3 - generated images stored in Amazon S3 (*in the future*)
* type:... - you can write custom implementation
* location - folder location for `fs` type

`imagepreprocessing` can be:

* `type:imagesharp` - use [SixLabors/ImageSharp](https://github.com/SixLabors/ImageSharp) library to rescale images 
* `type:...` - you can write custom implementation
* `scales: [75,100,150,200]` - how much rescale images you can store in `storage`, the number of scales array is percentage of original image.

# Formats

Picfit currently supports the following image formats:

* `image/jpeg` with the keyword `jpg` or `jpeg`
* `image/png` with the keyword `png`
* `image/gif` with the keyword `gif`
* `image/bmp` with the keyword `bmp`

