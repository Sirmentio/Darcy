# Darcy
Darcy is a synchronous, single file YAML based database library targeting .NET 7 and up. Darcy is dead simple, only depending
on [YamlDotNet](https://github.com/aaubry/YamlDotNet) for what it does.

Data is stored in what are called "Baskets", a class that can hold an arbitrary amount of keys whose values are dynamic objects.
This is useful for the likes of holding user facing data such as configuration files, or generally holding data to be used later or between program runs.

## Example

For a runnable example, see DarcyDemo, here's the syntax of how baskets work.

```
// Create a new basket called "users", saved as "users.json"
var Users = new Basket("users", Autostore: true);
// Technically there's no "0" entry yet, when this function is called, it internally creates an ExpandoObject
var user = Users.GetKey("0");
user.name = "Darcy Gale";
user.gender = "Unknown";
// Properties can be overwritten easily and will be reflected upon through a file change, since Autostore is enabled.
user.gender = "Female";
// Let's get funky with some different data types, it supports as much as YamlDotNet and the yaml specification does.
user.possessions = new List<String>() { "Red Hoodie", "Basket", "Smartphone" };
user.birthdate = DateTime.Parse("06-10-1960");
user.age = null;
user.chance_of_spontaneous_combustion = 0.0000000000000001f;
```

outputs the following YAML file:
```
0:
  name: Darcy Gale
  gender: Female
  birthdate: 1960-06-10T00:00:00.0000000
  age: 
  possessions:
  - Red Hoodie
  - Basket
  - Smartphone
  chance_of_spontaneous_combustion: 1E-16
```
Data is saved automatically to the basket by default, but you can explicitly set ``Autostore`` to ``false`` and then use ``Users.save()``
to commit your changes to the basket.

## Installation
TBA

## FAQ

### Q: Why the name Darcy and why Baskets?
The whole idea was remniscent of a character I wrote named Darcy. She owns a basket which allows her to take anything out from it, so long as it
exists somewhere. Baskets in this library are a lot like that; as long as it exists in the yaml file, you can get it, with the obvious benefit of
adding your own data programmatically.

### Q: What brought you to making this?
I wanted something to easily store as much data as I can into somewhere human readable with loose syntax.

## Licensing
Check out [LICENSE.txt](LICENSE.txt)