namespace OrchardProject.ViewModels
{
    public class Translator
    {
        private string _language;

        public Translator()
        {
            _language = Properties.Settings.Default.Language;
        }

        public Translator(string language)
        {
            _language = language;
        }

        public string getTranslation(string key)
        {
            switch (key)
            {
                case "Type":
                    if (_language == "en")
                        return "Type";
                    else if (_language == "es")
                        return "Tipo";
                    break;
                case "Back":
                    if (Properties.Settings.Default.Language == "en")
                        return "Back";
                    else if (Properties.Settings.Default.Language == "es")
                        return "previo";
                    break;
                case "Chemical":
                    if (Properties.Settings.Default.Language == "en")
                        return "Chemical";
                    else if (Properties.Settings.Default.Language == "es")
                        return "Químico";
                    break;
                case "Quantity":
                    if (Properties.Settings.Default.Language == "en")
                        return "Quantity";
                    else if (Properties.Settings.Default.Language == "es")
                        return "Cantidad";
                    break;
                case "Add To Order":
                    if (Properties.Settings.Default.Language == "en")
                        return "Add To Order";
                    else if (Properties.Settings.Default.Language == "es")
                        return "Agregar";
                    break;
                case "Date":
                    if (Properties.Settings.Default.Language == "en")
                        return "Date";
                    else if (Properties.Settings.Default.Language == "es")
                        return "Fecha";
                    break;
                case "Submit":
                    if (Properties.Settings.Default.Language == "en")
                        return "Submit";
                    else if (Properties.Settings.Default.Language == "es")
                        return "enviar";
                    break;
                case "KTW Organic":
                    if (Properties.Settings.Default.Language == "en")
                        return "KTW Organic";
                    else if (Properties.Settings.Default.Language == "es")
                        return "KTW Orgánico";
                    break;
                case "SMW Organic":
                    if (Properties.Settings.Default.Language == "en")
                        return "SMW Organic";
                    else if (Properties.Settings.Default.Language == "es")
                        return "SMW Orgánico";
                    break;
                case "Print":
                    if (Properties.Settings.Default.Language == "en")
                        return "Print";
                    else if (Properties.Settings.Default.Language == "es")
                        return "imprimir";
                    break;

                case "Remove":
                    if (Properties.Settings.Default.Language == "en")
                        return "Remove";
                    else if (Properties.Settings.Default.Language == "es")
                        return "retirar";
                    break;
                case "Organic":
                    if (Properties.Settings.Default.Language == "en")
                        return "Organic";
                    else if (Properties.Settings.Default.Language == "es")
                        return "Orgánico";
                    break;
                case "Conventional":
                    if (Properties.Settings.Default.Language == "en")
                        return "Conventional";
                    else if (Properties.Settings.Default.Language == "es")
                        return "Convencional";
                    break;
                case "Order Name":
                    if (Properties.Settings.Default.Language == "en")
                        return "Order Name";
                    else if (Properties.Settings.Default.Language == "es")
                        return "Nombre del pedido";
                    break;

                case "Total Chemicals":
                    if (Properties.Settings.Default.Language == "en")
                        return "Total Chemicals";
                    else if (Properties.Settings.Default.Language == "es")
                        return "tamaño";
                    break;
            }

            return key;
        }
    }
}