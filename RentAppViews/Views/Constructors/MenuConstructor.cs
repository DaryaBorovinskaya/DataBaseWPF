using DataBase1WPF.DataBase.Entities.MenuElem;
using DataBase1WPF.DataBase.Entities.UserAbilities;
using DataBase1WPF.Models;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace DataBase1WPF.Views.Constructors
{
    public class MenuConstructor
    {
        private List<IMenuElemDB> _menuElems;
        private IList<IUserAbilitiesDB> _userAbilities;

        private void GetUserAbilities()
        {
            IList<IUserAbilitiesDB> allUserAbilities = 
                DataManager.GetInstance().UserAbilitiesDB_Repository.Read();

            foreach (IUserAbilitiesDB userAbilitiesDB in allUserAbilities)
            {
                if (userAbilitiesDB.UserId == DataManager.GetInstance().CurrentUser.Id)
                    _userAbilities.Add(userAbilitiesDB);
            }
        }

        private MenuItem CreateMenuItem(IMenuElemDB menuElemDB)
        {
            MenuItem menuItem = new MenuItem
            {
                Header = menuElemDB.Name,
                Tag = menuElemDB
            };
            menuItem.FontSize = 16;

            if (!string.IsNullOrEmpty(menuElemDB.FuncName))
            {
                foreach(IUserAbilitiesDB userAbilities in _userAbilities)
                {
                    if (userAbilities.MenuElemId == menuElemDB.Id)
                    {
                        if (userAbilities.R == true)
                        {
                            DependencyProperty commandProperty = MenuItem.CommandProperty;
                            if (!BindingOperations.IsDataBound(menuItem, commandProperty))
                            {
                                Binding binding = new(menuElemDB.FuncName);
                                BindingOperations.SetBinding(menuItem, commandProperty, binding);   
                            }
                        }
                        else
                        {
                            menuItem.Visibility = Visibility.Collapsed;
                        }
                        break;
                    }
                }
            }

            return menuItem;
        }

        private void AddMenuItems(MenuItem parentMenuItem, uint parentId)
        {
            List<IMenuElemDB> menuElems = _menuElems.FindAll(item =>  item.ParentId == parentId);

            menuElems.Sort((item1, item2) => item1.Order.CompareTo(item2.Order));

            foreach(IMenuElemDB menuElem in menuElems)
            {
                MenuItem menuItem = CreateMenuItem(menuElem);
                parentMenuItem.Items.Add(menuItem);
                AddMenuItems(menuItem, menuElem.Id);
            }
        }

        public MenuConstructor()
        {
            _menuElems = new List<IMenuElemDB>();
            _userAbilities = new List<IUserAbilitiesDB>();
            GetUserAbilities();
        }

        public Menu Construct()
        {
            Menu menu = new ();

            _menuElems = DataManager.GetInstance().MenuElemDB_Repository.Read().ToList<IMenuElemDB>();

            List<IMenuElemDB> topLevelMenuElems = _menuElems.FindAll(item => item.ParentId == 0);
            topLevelMenuElems.Sort((item1, item2) => item1.Order.CompareTo(item2.Order));

            foreach(IMenuElemDB topLevelMenuElem in topLevelMenuElems)
            {
                MenuItem menuItem = CreateMenuItem(topLevelMenuElem);
                menu.Items.Add(menuItem);
                AddMenuItems(menuItem, topLevelMenuElem.Id);
            }

            return menu;
        }
    }
}
