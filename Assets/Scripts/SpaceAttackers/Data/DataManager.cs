using System;
using System.IO;
using UnityEngine;

namespace SpaceAttackers.Data
{
	public abstract class DataManager<T> where T : new()
	{
		private T _data;
		protected abstract string FileName { get; }
		private string Path => $"{Application.persistentDataPath}/{FileName}.json";
		public T Data => _data;
		public Action DataChanged;

		protected DataManager()
		{
			GetData();
		}

		public void SetData(T data)
		{
			WriteData(SerializeData(data));
			_data = data;
			DataChanged?.Invoke();
		}

		public void RefreshData()
		{
			GetData();
		}

		private void GetData()
		{
			try
			{
				if (!File.Exists(Path))
				{
					_data = new T();
				}

				var contents = File.ReadAllText(Path);
				_data = DeserializeData(contents) ?? new T();
			}
			catch (Exception e)
			{
				Debug.LogError(e);
				_data = new T();
			}
		}

		private void WriteData(string jsonData)
		{
			File.WriteAllText(Path, jsonData);
		}

		private static T DeserializeData(string jsonData)
		{
			return JsonUtility.FromJson<T>(jsonData);
		}

		private static string SerializeData(T data)
		{
			return JsonUtility.ToJson(data);
		}
	}
}
