.PHONY: build

build:
		dotnet publish -p:PublishProfile=win-x86
		dotnet publish -p:PublishProfile=win-x64
.DEFAULT_GOAL := build