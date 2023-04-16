using YamlDotNet.Serialization;
using System.ComponentModel;
using System.Dynamic;

namespace Darcy
{
	/// <summary>
	/// A Basket is an Object that holds an arbitrary amount of ExpandoObjects which are saved to a YAML file.
	/// </summary>
	/// <remarks>
	/// Baskets are sort of like dictionaries, all of its data is organized through key/value pairs, except every value is an ExpandoObject.
	/// </remarks>
	public class Basket
	{
		private string _filePath;
		/// <summary>
		/// Tells the Basket whether or not to automatically save on each property change.
		/// </summary>
		public bool AutoStore = true;

		private readonly Dictionary<object, ExpandoObject>? _entries;

		/// <summary>
		/// The YamlDotNet Serializer
		/// </summary>
		public Serializer Serializer = new Serializer();
		/// <summary>
		/// The YamlDotNet Deserializer
		/// </summary>
		public Deserializer Deserializer = new Deserializer();

		/// <summary>
		/// Initializes a new <c>Basket</c> object.
		/// </summary>
		/// <param name="BasketName"></param>
		/// <param name="Autostore"></param>
		/// <exception cref="FileNotFoundException"></exception>
		public Basket(string BasketName, bool Autostore = true)
		{
			this._filePath = BasketName + ".yaml";
			this.AutoStore = Autostore;
			if (File.Exists(_filePath))
			{
				var file = File.OpenText(_filePath) ?? throw new FileNotFoundException();
				_entries = Deserializer.Deserialize<Dictionary<object, ExpandoObject>>(file);
				file.Close();
			}
			else
			{
				_entries = new Dictionary<object, ExpandoObject>();
			}
		}
		/// <summary>
		/// Saves a basket into <c>BasketName.yaml</c>
		/// </summary>
		/// <returns>A FileInfo pertaining to the saved YAML file.</returns>
		public FileInfo Save()
		{
			var yaml = Serializer.Serialize(_entries);
			File.WriteAllText(_filePath, yaml);
			return new FileInfo(_filePath);
		}

		/// <summary>
		///		Gets the key corresponding to the ExpandoObject, and if it doesn't already exist, creates a new empty one.
		/// </summary>
		/// <param name="key"></param>
		/// <returns>Existing or new ExpandoObject</returns>
		public dynamic GetKey(object key)
		{
			ExpandoObject entry = _entries.TryGetValue(key, out entry) ? entry : new ExpandoObject();
			
			if (AutoStore)
			{
				((INotifyPropertyChanged)entry).PropertyChanged += (sender, args) =>
				{
					Save();
				};
			}

			_entries[key] = entry;
			return entry;
		}

		/// <summary>
		/// Checks to see if a property exists in a given key.
		/// </summary>
		/// <param name="key"></param>
		/// <param name="name"></param>
		/// <returns></returns>
		public bool PropertyExists(ExpandoObject key, string name)
		{
			return ((IDictionary<string, object>)key).ContainsKey(name);
		}

		/// <summary>
		/// Removes a specified key from the Basket
		/// </summary>
		/// <param name="key"></param>
		public void RemoveKey(object key)
		{
			_entries.Remove(key);
		}

	}
}