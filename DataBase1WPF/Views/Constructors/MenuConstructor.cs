using DataBase1WPF.DataBase.Entities.MenuElem;
using DataBase1WPF.DataBase.Entities.UserAbilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DataBase1WPF.Views.Constructors
{
    public class MenuConstructor
    {
        private IList<IMenuElemDB> _menuElemsDB;
        private IList<IUserAbilitiesDB> _userAbilitiesDB;


        private void GetUserAbilities()
        {

        }

        private MenuItem CreateMenuItem(IMenuElemDB menuElemDB)
        {
            MenuItem menuItem = new MenuItem
            {
                Header = menuElemDB.Name,
                Tag = menuElemDB
            };

            return menuItem;
        }

        private void AddMenuItems(MenuItem parentMenuItem, uint parentId)
        {

        }


        public MenuConstructor()
        {
            _menuElemsDB = new List<IMenuElemDB>();
            _userAbilitiesDB = new List<IUserAbilitiesDB>();
            GetUserAbilities();
        }

        public Menu Construct()
        {
            Menu menu = new ();

            return menu;
        }
    }
}
