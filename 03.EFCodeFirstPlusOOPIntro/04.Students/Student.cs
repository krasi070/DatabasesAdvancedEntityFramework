namespace _04.Students
{
    using System;

    public class Student
    {
        public static int InstancesCount = 0;
        private string name;

        public Student(string name)
        {
            this.Name = name;
            InstancesCount++;
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
    }
}