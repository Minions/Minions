// BuiltinExtensions.cs
// 
// Copyright 2012 The Minions Project (http:/github.com/Minions).
// All rights reserved. Usage as permitted by the LICENSE.txt file for this project.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.Serialization.Formatters;
using Fools.cs.Utilities;
using Newtonsoft.Json;

namespace Fools.cs.builtins
{
	public static class BuiltinExtensions
	{
		private static readonly JsonSerializerSettings _json_serializer_settings = new JsonSerializerSettings {
			TypeNameAssemblyFormat = FormatterAssemblyStyle.Simple,
			TypeNameHandling = TypeNameHandling.Objects,
			Formatting = Formatting.Indented,
			Converters = new List<JsonConverter> {new ActionConverter()}
		};

		[NotNull]
		public static string pretty_print(this object value)
		{
			return JsonConvert.SerializeObject(value, _json_serializer_settings);
		}

		private class ActionConverter : JsonConverter
		{
			public override void WriteJson([NotNull] JsonWriter writer, object value, JsonSerializer serializer)
			{
				if (value == null)
				{
					writer.WriteNull();
					return;
				}
				writer.WriteValue(_get_target(value));
			}

			private static string _get_target([NotNull] object action)
			{
				var method_property_info = action.GetType()
					.GetProperty("Method");
				Debug.Assert(method_property_info != null, "method_property_info != null");
				var target = ((MethodInfo) method_property_info.GetValue(action, null));
				Debug.Assert(target != null, "target != null");
				Debug.Assert(target.DeclaringType != null, "target.DeclaringType != null");
				return string.Format("{0}:{1}", target.DeclaringType.FullName, target);
			}

			public override bool CanRead { get { return false; } }

			public override object ReadJson(JsonReader reader, Type object_type, object existing_value, JsonSerializer serializer)
			{
				throw new NotImplementedException();
			}

			public override bool CanConvert([NotNull] Type object_type)
			{
				if (object_type.IsGenericType)
				{
					var non_generic_name = object_type.FullName.Split('`')[0];
					return non_generic_name == "System.Action" || non_generic_name == "System.Func";
				}
				return object_type == typeof (Action);
			}
		}
	}
}
