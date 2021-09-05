using System;
using System.Collections.Generic;
using Kyoo.Abstractions.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Kyoo.Core.Api
{
	public class PeopleRoleConverter : JsonConverter<PeopleRole>
	{
		public override void WriteJson(JsonWriter writer, PeopleRole value, JsonSerializer serializer)
		{
			ICollection<PeopleRole> oldPeople = value.Show?.People;
			ICollection<PeopleRole> oldRoles = value.People?.Roles;
			if (value.Show != null)
				value.Show.People = null;
			if (value.People != null)
				value.People.Roles = null;

			JObject obj = JObject.FromObject((value.ForPeople ? value.People : value.Show)!, serializer);
			obj.Add("role", value.Role);
			obj.Add("type", value.Type);
			obj.WriteTo(writer);

			if (value.Show != null)
				value.Show.People = oldPeople;
			if (value.People != null)
				value.People.Roles = oldRoles;
		}

		public override PeopleRole ReadJson(JsonReader reader,
			Type objectType,
			PeopleRole existingValue,
			bool hasExistingValue,
			JsonSerializer serializer)
		{
			throw new NotImplementedException();
		}
	}
}
