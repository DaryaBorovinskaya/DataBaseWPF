using DataBase1WPF.Models.Services.Tables;
using DataBase1WPF.Models.Services.Tables.Handbooks;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase1WPF.ViewModels
{
    public class HandbooksVM : BasicVM
    {
        private static DistrictsService _districtsService;
        private static StreetsService _streetsService;
        private static BanksService _banksService;
        private static RentalPurposesService _rentalPurposesService;
        private static PaymentFrequencyService _paymentFrequencyService;
        private static TypesOfFinishingService _typesOfFinishingService;
        private static FineService _fineService;
        private static PositionsService _positionsService;

        private Dictionary<HandbooksEnum, ITableService> _handbooks = new()
        {
            {HandbooksEnum.Districts, _districtsService=new()},
            {HandbooksEnum.Streets, _streetsService=new()},
            {HandbooksEnum.Banks, _banksService = new()},
            {HandbooksEnum.RentalPurposes, _rentalPurposesService=new()},
            {HandbooksEnum.PaymentFrequency, _paymentFrequencyService = new()},
            {HandbooksEnum.TypesOfFinishing, _typesOfFinishingService=new()},
            {HandbooksEnum.Fine, _fineService = new()},
            {HandbooksEnum.Positions, _positionsService = new()}
        };



        private DataTable _dataTableHandbooks;
        private string _dataTableTitle;

        

        public HandbooksVM(HandbooksEnum handbook)
        {
            DataTableHandbooks = _handbooks[handbook].GetValuesTable();
            DataTableTitle = _handbooks[handbook].GetTableName();
        }



        public DataTable DataTableHandbooks
        {
            get { return _dataTableHandbooks; }
            set
            {
                if (_dataTableHandbooks == null )
                    _dataTableHandbooks = value;
                else
                    Set(ref _dataTableHandbooks, value);
            }
        }

        public string DataTableTitle
        {
            get { return _dataTableTitle; }
            set
            {
                if (_dataTableTitle == null)
                    _dataTableTitle = value;
                else
                    Set(ref _dataTableTitle, value);
            }
        }
    }
}
