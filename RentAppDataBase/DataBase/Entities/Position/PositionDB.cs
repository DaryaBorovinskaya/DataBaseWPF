using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase1WPF.DataBase.Entities.Position
{
    /// <summary>
    /// Данные о должностях из базы данных
    /// </summary>
    public class PositionDB : IPositionDB
    {
        public uint Id { get ; set ; }
        public string Name { get ; set ; }
        public float Salary { get ; set ; }

        public PositionDB(uint id, string name, float salary)
        {
            Id = id;
            Name = name;
            Salary = salary;
        }

        public PositionDB( string name, float salary)
        {
            Name = name;
            Salary = salary;
        }
    }
}
