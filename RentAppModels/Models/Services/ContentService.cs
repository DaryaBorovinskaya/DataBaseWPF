using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentAppModels.Models.Services
{
    /// <summary>
    /// Сервис Содержание
    /// </summary>
    public class ContentService
    {
        public string ContentText => "Иерархическая структура меню:\n" +
    "-Разное\n" +
        "\t-Настройки\n" +
        "\t-Сменить пароль\n" +
    "-Клиенты\n" +
        "\t-Физич. лица\n" +
        "\t-Юрид. лица\n" +
    "-Сотрудники\n" +
    "-Документы\n" +
        "\t-Экспорт договоров\n" +
    "-Справочники\n" +
        "\t-Районы\n" +
        "\t-Улицы\n" +
        "\t-Банки\n" +
        "\t-Здания\n" +
        "\t-Должности\n" +
        "\t-Периодичность оплаты\n" +
        "\t-Цели аренды\n" +
        "\t-Вид отделки\n" +
        "\t-Штраф\n" +
    "-Справка\n" +
        "\t-Содержание\n" +
        "\t-О программе\n" ;
    }
}
