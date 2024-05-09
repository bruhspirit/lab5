using Avalonia.Styling;
using CompilerLab.Properties;
using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.AxHost;

namespace CompilerLab
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;

        }
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //createTransitions();
        }

        private string condition = "Ожидание";
        private string lang = "rus";
        private string filename = "";

        private void CreateFileDialog(object sender, RoutedEventArgs e)
        {
            CloseFileWindow closeFileWindow = new CloseFileWindow();
            if (filename != "")
            {
                if (closeFileWindow.ShowDialog() == true)
                {
                    using (StreamWriter writer = new StreamWriter(filename, false))
                    {
                        writer.WriteLine(Input.Text);
                    }
                }
                else if (closeFileWindow.IsCanceled) { }
                else
                {
                    return;
                }
            }

            var dialog = new Microsoft.Win32.SaveFileDialog();
            dialog.FileName = "Document";
            dialog.DefaultExt = ".txt";
            dialog.Filter = "Text documents (.txt)|*.txt";

            bool? result = dialog.ShowDialog();

            if (result == true)
            {
                filename = dialog.FileName;

                FileStream fs = File.Create(filename);

                if (!Input.IsEnabled)
                {
                    Input.IsEnabled = true;
                    Input.Text = "";
                }
                else
                {
                    Input.Text = "";
                }
                fs.Close();
            }
            SaveAsOption.IsEnabled = true;
            SaveButton.IsEnabled = true;
            SaveOption.IsEnabled = true;
            RunButton.IsEnabled = true;
            RunOption.IsEnabled = true;
            CloseFileOption.IsEnabled = true;
            EditOption.IsEnabled = true;
            CancelButton.IsEnabled = true;
            RepeatButton.IsEnabled = true;
            CopyButton.IsEnabled = true;
            CutButton.IsEnabled = true;
            PasteButton.IsEnabled = true;
            if (lang == "rus")
            {
                condition = "Редактирование";
                Condition.Content = condition;
            }
            if (lang == "eng")
            {
                condition = "Editing";
                Condition.Content = condition;
            }

            Input.Text = "";


        }
        private void SaveAsFileDialog(object sender, RoutedEventArgs e)
        {
            // Configure save file dialog box
            var dialog = new Microsoft.Win32.SaveFileDialog();
            dialog.FileName = "Document"; // Default file name
            dialog.DefaultExt = ".txt"; // Default file extension
            dialog.Filter = "Text documents (.txt)|*.txt"; // Filter files by extension

            // Show save file dialog box
            bool? result = dialog.ShowDialog();

            // Process save file dialog box results
            if (result == true)
            {
                // Save document
                filename = dialog.FileName;

                FileStream fs = File.Create(filename);
                fs.Close();

                using (StreamWriter writer = new StreamWriter(filename, false))
                {
                    writer.WriteLine(Input.Text);
                }
                MessageBox.Show("Данные сохранены в " + filename);
            }
        }

        private void SaveFileDialog(object sender, RoutedEventArgs e)
        {
            using (StreamWriter writer = new StreamWriter(filename, false))
            {
                writer.WriteLine(Input.Text);
            }

            MessageBox.Show("Данные сохранены в " + filename);
        }

        private void OpenFileDialog(object sender, RoutedEventArgs e)
        {
            CloseFileWindow closeFileWindow = new CloseFileWindow();
            if (filename != "")
            {
                if (closeFileWindow.ShowDialog() == true)
                {
                    using (StreamWriter writer = new StreamWriter(filename, false))
                    {
                        writer.WriteLine(Input.Text);
                        writer.Close();
                    }
                }
                else if (closeFileWindow.IsCanceled) { }
                else
                {
                    return;
                }
            }
            // Configure open file dialog box
            var dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.FileName = "Document"; // Default file name
            dialog.DefaultExt = ".txt"; // Default file extension
            dialog.Filter = "Text documents (.txt)|*.txt"; // Filter files by extension

            // Show open file dialog box
            bool? result = dialog.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                // Open document
                filename = dialog.FileName;

                if (!Input.IsEnabled)
                {
                    Input.IsEnabled = true;
                    Input.Text = "";
                }

                using (StreamReader reader = new StreamReader(filename))
                {
                    string text = reader.ReadToEnd();
                    Input.Text = text;
                }

                SaveAsOption.IsEnabled = true;
                SaveButton.IsEnabled = true;
                SaveOption.IsEnabled = true;
                RunButton.IsEnabled = true;
                RunOption.IsEnabled = true;
                CloseFileOption.IsEnabled = true;
                EditOption.IsEnabled = true;
                CancelButton.IsEnabled = true;
                RepeatButton.IsEnabled = true;
                CopyButton.IsEnabled = true;
                CutButton.IsEnabled = true;
                PasteButton.IsEnabled = true;
                if (lang == "rus")
                {
                    condition = "Редактирование";
                    Condition.Content = condition;
                }
                if (lang == "eng")
                {
                    condition = "Editing";
                    Condition.Content = condition;
                }
            }
        }



        private void CloseFile(object sender, RoutedEventArgs e)
        {
            CloseFileWindow closeFileWindow = new CloseFileWindow();

            if (closeFileWindow.ShowDialog() == true)
            {
                using (StreamWriter writer = new StreamWriter(filename, false))
                {
                    writer.WriteLine(Input.Text);
                }
            }
            else if (closeFileWindow.IsCanceled) { return; }
            else
            {
                return;
            }
            Input.IsEnabled = false;
            SaveAsOption.IsEnabled = false;
            SaveButton.IsEnabled = false;
            SaveOption.IsEnabled = false;
            RunButton.IsEnabled = false;
            RunOption.IsEnabled = false;
            CloseFileOption.IsEnabled = false;
            EditOption.IsEnabled = false;
            CancelButton.IsEnabled = false;
            RepeatButton.IsEnabled = false;
            CopyButton.IsEnabled = false;
            CutButton.IsEnabled = false;
            PasteButton.IsEnabled = false;
            filename = "";
            if (lang == "rus")
            {
                condition = "Ожидание";
                Condition.Content = condition;
            }
            if (lang == "eng")
            {
                condition = "Waiting";
                Condition.Content = condition;
            }
        }
        void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            if (lang == "rus")
            {
                condition = "Выход";
                Condition.Content = condition;
            }
            if (lang == "eng")
            {
                condition = "Exit";
                Condition.Content = condition;
            }

            if (filename != "")
            {
                CloseFileWindow closeFileWindow = new CloseFileWindow();

                if (closeFileWindow.ShowDialog() == true)
                {
                    using (StreamWriter writer = new StreamWriter(filename, false))
                    {
                        writer.WriteLine(Input.Text);
                    }
                    Process.GetCurrentProcess().Kill();
                }
                else if (closeFileWindow.IsCanceled)
                {
                    Process.GetCurrentProcess().Kill();
                }
                else
                {
                    if (lang == "rus")
                    {
                        condition = "Редактирование";
                        Condition.Content = condition;
                    }
                    if (lang == "eng")
                    {
                        condition = "Editing";
                        Condition.Content = condition;
                    }
                    e.Cancel = true;
                    return;
                }
            }
            else
            {
                Process.GetCurrentProcess().Kill();
            }

        }

        private void Undo(object sender, RoutedEventArgs e)
        {
            Input.Undo();
        }

        private void Redo(object sender, RoutedEventArgs e)
        {
            Input.Redo();
        }

        private void Copy(object sender, RoutedEventArgs e)
        {
            Input.Copy();
        }

        private void Paste(object sender, RoutedEventArgs e)
        {
            Input.Paste();
        }

        private void Cut(object sender, RoutedEventArgs e)
        {
            Input.Cut();
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            Input.Cut();
            Clipboard.Clear();
        }

        private void SelectAll(object sender, RoutedEventArgs e)
        {
            Input.SelectAll();
        }

        private void OutputFont_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (OutputFont.Text == "")
            {
                Output.FontSize = 14;
            }
            else
            {
                Output.FontSize = Convert.ToInt32(OutputFont.SelectedItem.ToString().Replace("System.Windows.Controls.ComboBoxItem: ", ""));
            }

        }
        private void InputFont_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (InputFont.Text == "")
            {
                Input.FontSize = 14;
            }
            else
            {
                Input.FontSize = Convert.ToInt32(InputFont.SelectedItem.ToString().Replace("System.Windows.Controls.ComboBoxItem: ", ""));
            }
        }

        private void SwitchToRussian(object sender, RoutedEventArgs e)
        {
            string[] rus = {"Файл","Создать","Открыть","Сохранить","Сохранить как","Выход", "Правка","Отменить", "Повторить",
                "Вырезать", "Копировать", "Вставить", "Удалить", "Выделить все",
                "Текст", "Постановка задачи", "Грамматика", "Классификация грамматики",
                "Метод анализа", "Диагностика и нейтрализация ошибок", "Текстовый пример", "Список литературы", "Исходный код программы", "Справка"
                , "Вызов справки", "О программе", "Настройки", "Язык", "Пуск"};
            FileOption.Header = rus[0];
            CreateOption.Header = rus[1];
            OpenOption.Header = rus[2];
            SaveOption.Header = rus[3];
            SaveAsOption.Header = rus[4];
            CloseFileOption.Header = rus[5];
            EditOption.Header = rus[6];
            UndoOption.Header = rus[7];
            RedoOption.Header = rus[8];
            CutOption.Header = rus[9];
            CopyOption.Header = rus[10];
            PasteOption.Header = rus[11];
            DeleteOption.Header = rus[12];
            SelectAllOption.Header = rus[13];
            TextOption.Header = rus[14];
            Formulation.Header = rus[15];
            Grammatic.Header = rus[16];
            GrammaticClass.Header = rus[17];
            AnalysMethod.Header = rus[18];
            Troubleshooter.Header = rus[19];
            Example.Header = rus[20];
            Literature.Header = rus[21];
            SourceCode.Header = rus[22];
            Help.Header = rus[23];
            HelpOption.Header = rus[24];
            AboutProgram.Header = rus[25];
            Settings.Header = rus[26];
            Language.Header = rus[27];
            RunOption.Header = rus[28];
            CreateFileButton.ToolTip = "Создать файл";
            OpenFileButton.ToolTip = "Открыть файл";
            SaveButton.ToolTip = "Сохранить файл";
            CancelButton.ToolTip = "Отмена изменений";
            RepeatButton.ToolTip = "Повтор последнего изменения";
            CopyButton.ToolTip = "Копировать";
            CutButton.ToolTip = "Вырезать";
            PasteButton.ToolTip = "Вставить";
            RunButton.ToolTip = "Пуск";
            HelpButton.ToolTip = "Справка";
            AboutProgramButton.ToolTip = "О программе";
            InputFont.ToolTip = "Размер шрифта в окне редактирования";
            OutputFont.ToolTip = "Размер шрифта в окне вывода";

            lang = "rus";
            if (condition == "Waiting")
            {
                condition = "Ожидание";
                Condition.Content = condition;
            }
            if (condition == "Exit")
            {
                condition = "Выход";
                Condition.Content = condition;
            }
            if (condition == "Editing")
            {
                condition = "Редактирование";
                Condition.Content = condition;
            }
        }
        private void SwitchToEnglish(object sender, RoutedEventArgs e)
        {
            string[] eng = {"File","Create","Open","Save","Save as","Exit", "Edit","Undo", "Redo",
             "Cut", "Copy", "Paste", "Delete", "Select all",
             "Text", "Problem statement", "Grammar", "Grammar classification",
             "Analysis method", "Diagnosis and error neutralization", "Text example", "List of literature", "Source code of the program", "Help"
             , "Call for help", "About the program", "Settings", "Language", "Start"};
            FileOption.Header = eng[0];
            CreateOption.Header = eng[1];
            OpenOption.Header = eng[2];
            SaveOption.Header = eng[3];
            SaveAsOption.Header = eng[4];
            CloseFileOption.Header = eng[5];
            EditOption.Header = eng[6];
            UndoOption.Header = eng[7];
            RedoOption.Header = eng[8];
            CutOption.Header = eng[9];
            CopyOption.Header = eng[10];
            PasteOption.Header = eng[11];
            DeleteOption.Header = eng[12];
            SelectAllOption.Header = eng[13];
            TextOption.Header = eng[14];
            Formulation.Header = eng[15];
            Grammatic.Header = eng[16];
            GrammaticClass.Header = eng[17];
            AnalysMethod.Header = eng[18];
            Troubleshooter.Header = eng[19];
            Example.Header = eng[20];
            Literature.Header = eng[21];
            SourceCode.Header = eng[22];
            Help.Header = eng[23];
            HelpOption.Header = eng[24];
            AboutProgram.Header = eng[25];
            Settings.Header = eng[26];
            Language.Header = eng[27];
            RunOption.Header = eng[28];
            CreateFileButton.ToolTip = "Create file";
            OpenFileButton.ToolTip = "Open file";
            SaveButton.ToolTip = "Save file";
            CancelButton.ToolTip = "Undo";
            RepeatButton.ToolTip = "Redo";
            CopyButton.ToolTip = "Copy";
            CutButton.ToolTip = "Cut";
            PasteButton.ToolTip = "Paste";
            RunButton.ToolTip = "Run";
            HelpButton.ToolTip = "Help";
            AboutProgramButton.ToolTip = "About program";
            InputFont.ToolTip = "Editor font-size";
            OutputFont.ToolTip = "Output font-size";
            lang = "eng";
            if (condition == "Ожидание")
            {
                condition = "Waiting";
                Condition.Content = condition;
            }
            if (condition == "Выход")
            {
                condition = "Exit";
                Condition.Content = condition;
            }
            if (condition == "Редактирование")
            {
                condition = "Editing";
                Condition.Content = condition;
            }
        }

        private void CallHelp(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("C:\\Users\\myndc\\OneDrive\\Desktop\\123\\CompilerLab-main\\CompilerLab\\help.html");
            }
            catch (System.ComponentModel.Win32Exception noBrowser)
            {
                if (noBrowser.ErrorCode == -2147467259)
                    MessageBox.Show(noBrowser.Message);
            }
            catch (System.Exception other)
            {
                MessageBox.Show(other.Message);
            }

        }

        private void About(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("C:\\Users\\myndc\\OneDrive\\Desktop\\123\\CompilerLab-main\\CompilerLab\\About.html");
            }
            catch (System.ComponentModel.Win32Exception noBrowser)
            {
                if (noBrowser.ErrorCode == -2147467259)
                    MessageBox.Show(noBrowser.Message);
            }
            catch (System.Exception other)
            {
                MessageBox.Show(other.Message);
            }
        }

        public class OutputItem
        {
            public string Code { get; set; }
            public string Type { get; set; }
            public string Lexem { get; set; }
            public string Symbol { get; set; }
            public string String { get; set; }
        }

        private void TextChangedEventHandler(object sender, TextChangedEventArgs args)
        {

            /* Output.Items.Clear();
             int strings_counter = 0;
             List<string> strings = new List<string>();
             string word_buffer = "";
             for (int i = 0; i < Input.Text.Length; i++)
             {
                 if (Input.Text[i] == '\n')
                 {      
                     strings_counter++;
                     CheckCodes(word_buffer, strings_counter, i-1, true);
                     word_buffer = "";                 
                     CheckCodes('\n' + "", strings_counter, i, true);           
                     continue;
                 }
                 if (((((i + 1) < Input.Text.Length)  && Input.Text[i + 1] == '\r') || i == Input.Text.Length - 1) && Input.Text[i] == '\n')
                 {
                     strings_counter++;
                     word_buffer += Input.Text[i]; 
                     if (word_buffer != "")
                         CheckCodes(word_buffer, strings_counter, i, true);
                     word_buffer = "";
                     continue;
                 }
                 if (Input.Text[i] == '\r')
                     continue;

                 if (i == Input.Text.Length - 1)
                 {
                     strings_counter++;
                     word_buffer += Input.Text[i];
                     CheckCodes(word_buffer, strings_counter, i, true);
                 }

                 if (Input.Text[i] == ' ')
                 {
                     if (word_buffer != "")
                         CheckCodes(word_buffer, strings_counter, i-1, false);
                     CheckCodes(" ", strings_counter, i, false);
                     word_buffer = "";
                     continue;
                 }
                 else
                 {         
                     word_buffer += Input.Text[i];
                 }
             }*/
        }

        private bool def(string s, int n)
        {
            string word = "";
            int code = 0;
            for (int j = 0; j < s.Length; j++)
            {
                if (word != "" && s[j] == ' ')
                {
                    code = 19;
                    Output.Items.Add(new OutputItem { Code = "" + code, Type = "Ошибка: недопустимый символ", Lexem = word, Symbol = "" + j, String = "" + (n + 1) });
                    break;
                }
                else if (s[j] == '\r')
                {
                    continue;
                }
                else if (word == "" && s[j] == ' ')
                {
                    continue;
                }
                else if (s[j] == '=')
                {
                    return true;
                }
                else
                {
                    if (!((s[j] >= 'a' && s[j] <= 'z') || (s[j] >= 'A' && s[j] <= 'Z') || (s[j] == '_') || (s[j] == '-') || (s[j] >= '0' && s[j] <= '9' && (j != 0 || s[j - 1] != ' '))))
                        word += s[j];
                    else
                    {
                        code = 19;
                        Output.Items.Add(new OutputItem { Code = "" + code, Type = "Ошибка: недопустимый символ", Lexem = word, Symbol = "" + j, String = "" + (n + 1) });
                        break;
                    }
                }
            }
            return false;
        }
        private bool assignment(string s, int n)
        {
            return false;
        }

        enum StatmentsEnum
        {
            DEF,
            LISTNAME,
            ASSIGNTMENT,
            ITEMS,
            NUMBER,
            NUMBERREM,
            DECIMAL,
            DECIMALREM,
            STRING
        }

        private Dictionary<string, Dictionary<string, string>> FillDictionaryWithLetters(Dictionary<string, Dictionary<string, string>> tmp, string stm, string next_stm)
        {
            for (int i = 0; i < 26; i++)
            {
                string s = (char)('a' + i) + "";
                tmp[stm].Add(s, next_stm);
            }
            for (int i = 0; i < 26; i++)
            {
                string s = (char)('A' + i) + "";
                tmp[stm].Add(s, next_stm);
            }
            string str = Convert.ToString('_');
            tmp[stm].Add(str, next_stm);
            return tmp;
        }

        private Dictionary<string, Dictionary<string, string>> FillDictionaryWithSymbols(Dictionary<string, Dictionary<string, string>> tmp, string stm, string next_stm)
        {
            for (int i = 0; i < 255; i++)
            {
                string s = (char)i + "";
                tmp[stm].Add(s, next_stm);
            }
            return tmp;
        }

        private Dictionary<string, Dictionary<string, string>> FillDictionaryWithNumbers(Dictionary<string, Dictionary<string, string>> tmp, string stm, string next_stm)
        {
            for (int i = 0; i < 10; i++)
            {
                string s = Convert.ToString(i);
                tmp[stm].Add(s, next_stm);
            }
            return tmp;
        }

        private int string_counter = 0;
        private int char_counter = 0;
        private string[] get_new_char()
        {
            string[] result = new string[3];
            return result;
        }



        private Dictionary<string, Dictionary<string, string>> transitions = new Dictionary<string, Dictionary<string, string>>();



        /*1) DEF->letter LISTNAME
        2) LISTNAME->letter LISTNAME | = ASSIGNTMENT
        3) ASSIGNTMENT-> [ITEMS
        4) ITEMS-> [+| -] NUMBER | " STRING | ]
        5) NUMBER->digit NUMBERREM
        6) NUMBERREM-> , ITEMS | ] | digit NUMBERREM | .DECIMAL
        7) DECIMAL->digit DECIMALREM
        8) DECIMALREM-> , ITEMS | ] | digit DECIMALREM
        9) STRING-> "] | ", ITEMS | symbol STRING*/

        private int Lexer(string word)
        {
            int code = 0;
            string type = "";

            if (word == "int")
            {
                code = 1;
                type = "Ключевое слово";
                return code;
            }
            else if (word == "bool")
            {
                code = 2;
                type = "Ключевое слово";
                return code;
            }
            else if (word == "char")
            {
                code = 3;
                type = "Ключевое слово";
                return code;
            }
            else if (word == "string")
            {
                code = 4;
                type = "Ключевое слово";
                return code;
            }
            else if (word == "true")
            {
                code = 6;
                type = "Ключевое слово";
                return code;
            }
            else if (word == "false")
            {
                code = 7;
                type = "Ключевое слово";
                return code;
            }
            else if (word == " " || word == ";" || word == ",")
            {
                code = 8;
                type = "Разделитель";
                return code;
            }
            else if (word.Contains('\r') && word.Length == 1)
            {
                code = 9;
                type = "Переход на новую строку";
                return code;
            }
            else if (word == "=")
            {
                code = 10;
                type = "Оператор присваивания";
                return code;
            }
            else if (word == "[")
            {
                code = 11;
                type = "Объявление блока инициализации";
                return code;
            }
            else if (word == "]")
            {
                code = 12;
                type = "Конец блока инициализации";
                return code;
            }
            else if (Int32.TryParse(word, out int number))
            {
                code = 13;
                type = "Целое число";
                return code;
            }
            else if (Double.TryParse(word, out double number2))
            {
                code = 14;
                type = "Вещественное число";
                return code;
            }
            else if (word[0] == '\"' && word.Last() == '\"')
            {
                code = 15;
                type = "Строка";
                return code;
            }
            else if (IdCheck(word))
            {
                code = 5;
                type = "Идентификатор";
                return code;
            }
            else
            {
                code = 19;
                type = "Ошибка: недопустимые символы";
                return code;
            }
        }

        /*private void createTransitions()
        {
            
        }*/
        private bool IdCheck(string str)
        {
            bool isValidated = true;

            for (int c = 0; c < str.Length; c++)
            {
                if (c == 0)
                {
                    if (!((str[c] >= 'a' && str[c] <= 'z') || (str[c] >= 'A' && str[c] <= 'Z') || (str[c] == '_')))
                    {
                        isValidated = false;
                    }
                }
                else
                {
                    if (!((str[c] >= 'a' && str[c] <= 'z') || (str[c] >= 'A' && str[c] <= 'Z') || (str[c] >= '0' && str[c] <= '9') || (str[c] == '_')))
                    {
                        isValidated = false;
                    }
                }
            }
            return isValidated;
        }
        // a + b - c * d = x
        List<string> tokens = new List<string>();
        List<string> operators = new List<string>();

        Dictionary<string, string> results = new Dictionary<string, string>();
        private void FillTokens()
        {
            for (int i = 0; i < 26; i++)
            {
                string s = (char)('a' + i) + "";
                tokens.Add(s);
            }
            for (int i = 0; i < 26; i++)
            {
                string s = (char)('A' + i) + "";
                tokens.Add(s);
            }
            for (int i = 0; i <= 9; i++)
            {
                string s = Convert.ToString(i);
                tokens.Add(s);
            }
            operators.Add("+");
            operators.Add("=");
            operators.Add("-");
            operators.Add("*");
            operators.Add("/");
        }

        class Result
        {
            public string arg1 = "";
            public string arg2 = "";
            public string op = "";
            public string res = "";
        }
        public int count = 1;

        private void Task21(string str, int n)
        {
            string pattern = @"\d*(\,?\d*)";

            Regex regex = new Regex(pattern);
            MatchCollection matches = regex.Matches(str);
            if (matches.Count > 0)
            {
                foreach (Match match in matches)
                {
                    if (match.Value != "" && (match.Value != " " && (match.Value != "\r")))
                    {
                        string res = match.Value;
                        if (res.Last() == ',')
                            res += "0";
                        Output.Items.Add(new OutputItem { Code = "", Type = "", Lexem = "" + res, Symbol = "" + match.Index, String = Convert.ToString(n + 1) });
                    }

                }
            }
        }
        private void Task5(string str, int n)
        {
            string pattern = @"4\d{3}-\d{4}-\d{4}-\d{4}";
            Regex regex = new Regex(pattern);
            MatchCollection matches = regex.Matches(str);
            if (matches.Count > 0)
            {
                foreach (Match match in matches)
                {
                    if (match.Value != "" && (match.Value != " " && (match.Value != "\r")))
                    {
                        string res = match.Value;
                        Output.Items.Add(new OutputItem { Code = "", Type = "", Lexem = "" + res, Symbol = "" + match.Index, String = Convert.ToString(n + 1) });
                    }

                }
            }
        }
        private void Task17(string str, int n)
        {
            string pattern = @"[-+]?([0-9]|[1-8][0-9]|90)\.?(?<=\.)\d*?[NS]";
            Regex regex = new Regex(pattern);
            MatchCollection matches = regex.Matches(str);
            if (matches.Count > 0)
            {
                foreach (Match match in matches)
                {
                    if (match.Value != "" && (match.Value != " " && (match.Value != "\r")))
                    {
                        string res = match.Value;
                        if (res.Last() == '.')
                            res += "0";
                        Output.Items.Add(new OutputItem { Code = "", Type = "", Lexem = "" + res, Symbol = "" + match.Index, String = Convert.ToString(n + 1) });
                    }

                }
            }
        }
        private int ct = 21;

        private void Sign(string str, int n)
        {
            result += "S ";
        }

        private int Number(string str, int n)
        {
            result += "N ";
            if (n + 1 < str.Length)
            {
                if (Char.IsDigit(str[n + 1]))
                {
                    
                    n = Number(str, n + 1);
                    Digit(str, n + 1);
                    //return n;
                }
            }
            Digit(str, n);
            return n;

        }

        private void SubFormula(string str, int n)
        {
            result += "F ";
        }

        private void Digit(string str, int n)
        {
            result += "D ";
        }


        private void Formula(string str, int n)
        {
            result += "F -> ";
            if (n < str.Length)
            {
                for (int i = 0; i < str.Length; i++)
                {
                    if (Char.IsDigit(str[i]))
                    {
                        SubFormula(str, i);
                        i = Number(str, i);
                        continue;
                    }
                    else if (str[i] == '+' || str[i] == '-' || str[i] == '*' || str[i] == '/')
                    {
                        Sign(str, i);
                        continue;
                    }
                    else if (str[i] == '(' || str[i] == ')')
                    {
                        if (str[i] == '(' && !str.Contains(')'))
                        {
                            Output.Items.Add(new OutputItem { Code = "", Type = "", Lexem = "Отсутствует закрывающая скобка", Symbol = "" + i, String = "" + n });
                            break;
                        }
                        if (str[i] == ')' && !str.Contains('('))
                        {
                            Output.Items.Add(new OutputItem { Code = "", Type = "", Lexem = "Отсутствует открывающая скобка", Symbol = "" + i, String = "" + n });
                            break;
                        }

                        continue;
                    }
                    else
                    {
                        if (str[i] != '\r' && str[i] != ' ')
                        {
                            Output.Items.Add(new OutputItem { Code = "", Type = "", Lexem = "Неожиданный символ", Symbol = "" + i, String = "" + n });
                            break;
                        }
                                      
                    }
                }
            }
        }
        string result = "";
        private void Parser(string str, int n)
        {
            
            Formula(str, n);
            Output.Items.Add(new OutputItem { Code = "", Type = "", Lexem = result, Symbol = "", String = "" });
        }

        private void nums(object sender, EventArgs e)
        {
            ct = 21;
        }
        private void latitude(object sender, EventArgs e)
        {
            ct = 17;
        }
        private void visa(object sender, EventArgs e)
        {
            ct = 5;
        }
        private void Run(object sender, EventArgs e)
        {
            Output.Items.Clear();
            result = "";
            count = 1;
            string[] allStrings = Input.Text.Split('\n');
            FillTokens();
            for (int i = 0; i < allStrings.Count(); i++)
            {
                if (allStrings[i] != "\r" && allStrings[i] != " " && allStrings[i] != "")
                    Parser(allStrings[i], i);
            }
        }






        private void Grammatic_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(@"..\..\Grammar.html");
            }
            catch (System.ComponentModel.Win32Exception noBrowser)
            {
                if (noBrowser.ErrorCode == -2147467259)
                    MessageBox.Show(noBrowser.Message);
            }
            catch (System.Exception other)
            {
                MessageBox.Show(other.Message);
            }
        }

        private void GrammaticClass_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(@"..\..\Classification.html");
            }
            catch (System.ComponentModel.Win32Exception noBrowser)
            {
                if (noBrowser.ErrorCode == -2147467259)
                    MessageBox.Show(noBrowser.Message);
            }
            catch (System.Exception other)
            {
                MessageBox.Show(other.Message);
            }
        }

        private void AnalysMethod_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(@"..\..\methodAnalysis.html");
            }
            catch (System.ComponentModel.Win32Exception noBrowser)
            {
                if (noBrowser.ErrorCode == -2147467259)
                    MessageBox.Show(noBrowser.Message);
            }
            catch (System.Exception other)
            {
                MessageBox.Show(other.Message);
            }
        }

        private void Formulation_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(@"..\..\problem.html");
            }
            catch (System.ComponentModel.Win32Exception noBrowser)
            {
                if (noBrowser.ErrorCode == -2147467259)
                    MessageBox.Show(noBrowser.Message);
            }
            catch (System.Exception other)
            {
                MessageBox.Show(other.Message);
            }
        }

        private void Troubleshooter_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(@"..\..\Neutralizing.html");
            }
            catch (System.ComponentModel.Win32Exception noBrowser)
            {
                if (noBrowser.ErrorCode == -2147467259)
                    MessageBox.Show(noBrowser.Message);
            }
            catch (System.Exception other)
            {
                MessageBox.Show(other.Message);
            }
        }

        private void Example_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(@"..\..\testExamples.html");
            }
            catch (System.ComponentModel.Win32Exception noBrowser)
            {
                if (noBrowser.ErrorCode == -2147467259)
                    MessageBox.Show(noBrowser.Message);
            }
            catch (System.Exception other)
            {
                MessageBox.Show(other.Message);
            }
        }

        private void Literature_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(@"..\..\LitList.html");
            }
            catch (System.ComponentModel.Win32Exception noBrowser)
            {
                if (noBrowser.ErrorCode == -2147467259)
                    MessageBox.Show(noBrowser.Message);
            }
            catch (System.Exception other)
            {
                MessageBox.Show(other.Message);
            }
        }

        private void SourceCode_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("https://github.com/bruhspirit/coursework");
            }
            catch (System.ComponentModel.Win32Exception noBrowser)
            {
                if (noBrowser.ErrorCode == -2147467259)
                    MessageBox.Show(noBrowser.Message);
            }
            catch (System.Exception other)
            {
                MessageBox.Show(other.Message);
            }
        }
    }
}