using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.Interfaces;

namespace WebApi.Enums
{
    public class Gender: Enumeration
    {
        public static Gender Male = new Gender(1, "Male");
        public static Gender Female = new Gender(2, "Female");


        protected Gender() { }

        public Gender(int id, string name) : base(id, name) {}

        public static IEnumerable<Gender> List() => new[] { Male, Female };

        public static Gender FromName(string name)
        {
            var state = List()
                .SingleOrDefault(s => String.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

            if (state == null)
            {
                throw new ArgumentException($"Possible values for Gender: {String.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }

        public static Gender From(int id)
        {
            var state = List().SingleOrDefault(s => s.Id == id);

            if (state == null)
            {
                throw new ArgumentException($"Possible values for Gender: {String.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }
    }
}
