namespace _03.OldestFamilyMember
{
    using System.Linq;
    using System.Collections.Generic;
    using _01.DefineClassPerson;

    public class Family
    {
        public Family()
        {
            this.Members = new List<Person>();
        }

        public List<Person> Members { get; set; }

        public void AddMember(Person newMember)
        {
            this.Members.Add(newMember);
        }

        public Person GetOldestMember()
        {
            if (this.Members.Count == 0)
            {
                return null;
            }

            return this.Members
                .OrderByDescending(m => m.Age)
                .ToList()[0];
        }
    }
}