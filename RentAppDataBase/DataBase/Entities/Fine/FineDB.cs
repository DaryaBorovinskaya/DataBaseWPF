using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase1WPF.DataBase.Entities.Fine
{
    /// <summary>
    /// Данные о штрафе из базы данных
    /// </summary>
    public class FineDB : IFineDB
    {
        public uint Id { get; set; }
        public float Amount { get; set; }

        public FineDB(uint id, float amount)
        {
            Id = id;
            Amount = amount;
        }

        public FineDB(float amount)
        {
            Amount = amount;
        }
    }
}
