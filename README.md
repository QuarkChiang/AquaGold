# AquaGold

AquaGold is a tool designed to compare the contents of two directories and copy the differences to a specified directory. This project is developed using C# 12.0 and targets .NET 8.

## Features

- Compare the contents of the source and target directories
- Copy the differing files to a specified directory

## Installation

Ensure that you have .NET 8 SDK installed. You can download and install it from the [official website](https://dotnet.microsoft.com/download/dotnet/8.0).

## Usage

Run the following command in the command line:

```cmd
.\AquaGold.exe -s "B:\1.0" -t "B:\2.0" -c "B:\Target"
```

### Parameters

- `-s, --SoucreDirectory`: The source directory to compare (required)
- `-t, --TargetDirectory`: The target directory to compare (required)
- `-c, --CopyDirectory`: The directory to copy the differing files to (required)


## Code Structure

- `Program.cs`: The main entry point of the application, includes argument parsing and main logic
- `DirectoryFile.cs`: Class for handling directory contents
- `CompareFile.cs`: Class for comparing files
- `CopyFiles.cs`: Class for copying files

## Contributing

We welcome issues and pull requests. Please follow these steps to contribute:

1. Fork the repository
2. Create your feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a pull request
