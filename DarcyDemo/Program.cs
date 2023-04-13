using Darcy;
namespace DarcyDemo
{
	internal class Program
	{
		static void Main(string[] args)
		{
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
			Console.WriteLine($"Name: {user.name}. Gender: {user.gender}, birthdate: {user.birthdate}");
			
		}
	}
}