using DataBase1WPF.Models.Services.Tables;
using DataBase1WPF.Models.Services.Tables.UserManagement;
using System.Data;
using System.Windows;
using System.Windows.Input;

namespace DataBase1WPF.ViewModels.UserManagement
{
    /// <summary>
    /// Обработка и получение данных из окна AddOrEditUserManagementWindow
    /// </summary>
    public class EditUserManagementVM : BasicVM
    {
        private ITableService _tableService;
        private DataRow _row;
        private string _windowTitle;
        private string _buttonContent;
        private bool _r;
        private bool _w;
        private bool _e;
        private bool _d;


        private List<string> _userLoginComboBox;
        private int _selectedIndexUserLogin;
        private int _selectedIndexMenuElem;
        private List<string> _menuElemComboBox;
        public Action<string> OnApply;

        public string WindowTitle
        {
            get { return _windowTitle; }
        }

        public string ButtonContent
        {
            get { return _buttonContent; }
        }


        public List<string> UserLoginComboBox
        {
            get { return _userLoginComboBox; }
            set
            {
                Set(ref _userLoginComboBox, value);
            }
        }

        public int SelectedIndexUserLogin
        {
            get { return _selectedIndexUserLogin; }
            set
            {
                Set(ref _selectedIndexUserLogin, value);
            }
        }


        public List<string> MenuElemComboBox
        {
            get { return _menuElemComboBox; }
            set
            {
                Set(ref _menuElemComboBox, value);
            }
        }

        public int SelectedIndexMenuElem
        {
            get { return _selectedIndexMenuElem; }
            set
            {
                Set(ref _selectedIndexMenuElem, value);
            }
        }


        public bool R
        {
            get { return _r; }
            set
            {
                Set(ref _r, value);
            }
        }

        public bool W
        {
            get { return _w; }
            set
            {
                Set(ref _w, value);
            }
        }

        public bool E
        {
            get { return _e; }
            set
            {
                Set(ref _e, value);
            }
        }

        public bool D
        {
            get { return _d; }
            set
            {
                Set(ref _d, value);
            }
        }

        public EditUserManagementVM(DataRow row, ITableService tableService)
        {
            _tableService = tableService;
            _row = row;
            _windowTitle = $"Изменение данных таблицы: {_tableService.GetTableName()}";
            _buttonContent = "Внести изменения";

            if (_tableService is UserManagementService service)
            {
                _userLoginComboBox = service.GetUsers();
                _menuElemComboBox = service.GetMenuElems();
                _selectedIndexMenuElem = service.GetMenuElemSelectedIndex(row);
                _selectedIndexUserLogin = service.GetUserSelectedIndex(row);
            }

            _r = bool.Parse(row[5].ToString());
            _w = bool.Parse(row[6].ToString());
            _e = bool.Parse(row[7].ToString());
            _d = bool.Parse(row[8].ToString());
        }


        /// <summary>
        /// Нажатие на кнопку
        /// </summary>
        public ICommand Click
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    if (SelectedIndexMenuElem == -1)
                        MessageBox.Show("ОШИБКА: не выбрано значение элемента меню");
                    else if (SelectedIndexUserLogin == -1)
                        MessageBox.Show("ОШИБКА: не выбрано значение пользователя");

                    else
                    {
                        OnApply?.Invoke(" пользователь " + UserLoginComboBox[SelectedIndexUserLogin]
                + " элемент меню " + MenuElemComboBox[SelectedIndexMenuElem]);
                    }
                });
            }
        }

        /// <summary>
        /// Изменение
        /// </summary>
        public void Edit()
        {
            if (_tableService is UserManagementService service)
                service.Update(_row, SelectedIndexUserLogin, SelectedIndexMenuElem, R, W, E, D);
            
        }





    }
}
