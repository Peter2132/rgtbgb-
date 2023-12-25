using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prog10
{
    internal interface ICrud2
    {
        void CreateStroke(string role, string name);
        void Read(int pos, string role, string name);
        void Update(int poos, int pos, string role);
        void Delete(int pos, string role);
    }

    internal class Role : IEquatable<Role>
    {
        public int Pos { get; set; }
        public string RoleName { get; set; }
        public string Name { get; set; }

        public bool Equals(Role other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Pos == other.Pos && RoleName == other.RoleName && Name == other.Name;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Role)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Pos, RoleName, Name);
        }
    }

    internal class RoleDatabase : ICrud2
    {
        private List<Role> Roles { get; set; }

        public RoleDatabase()
        {
            Roles = new List<Role>();
        }

        public void CreateStroke(string role, string name)
        {
            var newRole = new Role { Pos = Roles.Count + 1, RoleName = role, Name = name };
            Roles.Add(newRole);
        }

        public void Read(int pos, string role, string name)
        {
            var roleToRead = Roles.FirstOrDefault(r => r.Pos == pos && r.RoleName == role && r.Name == name);
            if (roleToRead != null)
            {
                Console.WriteLine($"Pos: {roleToRead.Pos}, Role: {roleToRead.RoleName}, Name: {roleToRead.Name}");
            }
            else
            {
                Console.WriteLine("Role not found.");
            }
        }

        public void Update(int poos, int pos, string role)
        {
            var roleToUpdate = Roles.FirstOrDefault(r => r.Pos == poos);
            if (roleToUpdate != null)
            {
                roleToUpdate.RoleName = role;
                roleToUpdate.Pos = pos;
            }
            else
            {
                Console.WriteLine("Role not found.");
            }
        }

        public void Delete(int pos, string role)
        {
            var roleToDelete = Roles.FirstOrDefault(r => r.Pos == pos && r.RoleName == role);
            if (roleToDelete != null)
            {
                Roles.Remove(roleToDelete);
                UpdatePositions();
            }
            else
            {
                Console.WriteLine("Role not found.");
            }
        }

        private void UpdatePositions()
        {
            for (int i = 0; i < Roles.Count; i++)
            {
                Roles[i].Pos = i + 1;
            }
        }
    }


}


