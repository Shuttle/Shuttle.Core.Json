# Shuttle.Core.Json

Json.Net implementation of the `ISerializer` interface.

## Usage

```
PM> Install-Package Shuttle.Core.Json
```

``` c#
	_bus = ServiceBus.Create(c => 
				c.MessageSerializer(JsonSerializer.Default())
		   ).Start();
````

You can also specify `JsonSerializerSettings` when using the constructor to create the `JsonSerializer`:

``` c#
	_bus = ServiceBus.Create(c => 
				c.MessageSerializer(new JsonSerializer(new JsonSerializerSettings()))
		   ).Start();
````

