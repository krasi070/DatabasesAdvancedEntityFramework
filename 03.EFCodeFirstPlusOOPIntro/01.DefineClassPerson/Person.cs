namespace _01.DefineClassPerson
{
    using System;

    public class Person
    {
        private string name;
        private int age;

        public Person()
            : this(string.Empty, 1) { }

        public Person(int age)
            : this(string.Empty, age) { }

        public Person(string name)
            : this(name, 1) { }

        public Person(string name, int age)
        {
            this.Name = name;
            this.Age = age;
        }

        public string Name
        {
            get
            {
                return this.name;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Name cannot be null!");
                }

                this.name = value;
            }
        }

        public int Age
        {
            get
            {
                return this.age;
            }

            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("Age must be non-negative!");
                }

                this.age = value;
            }
        }
    }
}