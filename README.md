# ServerStatusClient
Display server statistics in your desktop PC console. At the moment it is only capable of reading the CPU count.

I am only writing this in C# for practice. This would of course make more sense to be made in a more suitable language like Python, Go, or another cli-oriented language. Since I work with JSON quite a bit, JavaScript would also make sense.

All user config is stored on `config/config.json`.

You can enter the IP and port of the server executable in the config, which will be displayed when starting the executable.

## Installation

Simply download the latest release and run from a console. If you would like to run the server configuration mode, add "-c" as an argument when running the executable e.g. `./ServerStatusClient -c`

## Server

The server, that runs on (you guessed it) the server machine, is available in [this repo](https://github.com/vigetious/ServerStatus/releases).
