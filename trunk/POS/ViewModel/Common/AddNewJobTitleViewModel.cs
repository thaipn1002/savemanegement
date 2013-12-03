using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CPC.Toolkit.Command;
using CPC.Toolkit.Base;
using System.IO;
using CPC.Helper;
using System.Xml.Linq;
using CPC.POS.Model;
using System.ComponentModel;

namespace CPC.POS.ViewModel
{
    class AddNewJobTitleViewModel : ViewModelBase, IDataErrorInfo
    {
        #region Define

        #endregion

        #region Constructors
        public AddNewJobTitleViewModel()
        {
            _ownerViewModel = this;
            this.InitialCommand();
        }
        #endregion

        #region Properties

        #region ID
        private int _id;
        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        public int ID
        {
            get { return _id; }
            set
            {
                if (_id != value)
                {
                    _id = value;
                    OnPropertyChanged(() => ID);
                }
            }
        }
        #endregion

        #region Text
        private string _text;
        /// <summary>
        /// Gets or sets the Text.
        /// </summary>
        public string Text
        {
            get { return _text; }
            set
            {
                if (_text != value)
                {
                    _text = value;
                    OnPropertyChanged(() => Text);
                }
            }
        }
        #endregion

        #region Symbol
        private string _symbol = string.Empty;
        /// <summary>
        /// Gets or sets the Symbol.
        /// </summary>
        public string Symbol
        {
            get { return _symbol; }
            set
            {
                if (_symbol != value)
                {
                    _symbol = value;
                    OnPropertyChanged(() => Symbol);
                }
            }
        }
        #endregion

        #region ItemJobTitle
        private ComboItem _itemJobTitle;
        /// <summary>
        /// Gets or sets the ItemJobTitle.
        /// </summary>
        public ComboItem ItemJobTitle
        {
            get { return _itemJobTitle; }
            set
            {
                if (_itemJobTitle != value)
                {
                    _itemJobTitle = value;
                    OnPropertyChanged(() => ItemJobTitle);
                }
            }
        }
        #endregion

        #endregion

        #region Commands Methods

        #region OkCommand
        /// <summary>
        /// Gets the OkCommand Command.
        /// <summary>
        public RelayCommand OkCommand { get; private set; }


        /// <summary>
        /// Method to check whether the OkCommand command can be executed.
        /// </summary>
        /// <returns><c>true</c> if the command can be executed; otherwise <c>false</c></returns>
        private bool OnOkCommandCanExecute()
        {
            return IsValid;
        }


        /// <summary>
        /// Method to invoke when the OkCommand command is executed.
        /// </summary>
        private void OnOkCommandExecute()
        {
            this.SaveStateToXml();

            this.FindOwnerWindow(_ownerViewModel).DialogResult = true;
        }

        private void SaveStateToXml()
        {
            // Load XML file.
            Stream stream = Common.LoadCurrentLanguagePackage();
            // Get file path.
            string fileLanguage = (stream as FileStream).Name;
            XDocument xDoc = XDocument.Load(stream);
            stream.Close();
            stream.Dispose();

            //// Get Prices in xml element.
            var collection = xDoc.Root.Elements("combo").FirstOrDefault(x => x.Attribute("key").Value == "JobTitles");
            int maxIdState = Common.JobTitles.Max(x => x.Value);
            int Id = maxIdState + 1;
            if (collection == null)
            {
                XElement comboItem = new XElement("combo");
                comboItem.Add(new XAttribute("key", "JobTitles"));

                XElement item = new XElement("item");
                item.Add(new XElement("value", 0));
                item.Add(new XElement("name", string.Empty));
           
                XElement item1 = new XElement("item");
                item1.Add(new XElement("value", 1));
                item1.Add(new XElement("name", this.Text.Trim()));
                xDoc.Add(comboItem);
            }
            else
            {
                XElement root = new XElement("item");
                root.Add(new XElement("value", Id));
                root.Add(new XElement("name", this.Text.Trim()));
                collection.Add(root);
            }

            xDoc.Save(fileLanguage);

            ItemJobTitle = new ComboItem()
            {
                ObjValue = Id,
                Value = Convert.ToInt16(Id),
                Text = this.Text,
                Symbol = Symbol.Trim()
            };

            //Insert to Current Collection
            Common.JobTitles.Add(ItemJobTitle);
        }
        #endregion

        #region CancelCommand
        /// <summary>
        /// Gets the Cancel Command.
        /// <summary>
        public RelayCommand CancelCommand { get; private set; }

        /// <summary>
        /// Method to check whether the Cancel command can be executed.
        /// </summary>
        /// <returns><c>true</c> if the command can be executed; otherwise <c>false</c></returns>
        private bool OnCancelCommandCanExecute()
        {
            return true;
        }


        /// <summary>
        /// Method to invoke when the Cancel command is executed.
        /// </summary>
        private void OnCancelCommandExecute()
        {
            FindOwnerWindow(_ownerViewModel).DialogResult = false;
        }
        #endregion

        #endregion

        #region Private Methods
        private void InitialCommand()
        {
            OkCommand = new RelayCommand(OnOkCommandExecute, OnOkCommandCanExecute);
            CancelCommand = new RelayCommand(OnCancelCommandExecute, OnCancelCommandCanExecute);
        }
        #endregion

        #region Public Methods
        #endregion

        #region IDataError
        public string Error
        {
            get
            {
                List<string> errors = new List<string>();
                PropertyDescriptorCollection props = TypeDescriptor.GetProperties(this);
                foreach (PropertyDescriptor prop in props)
                {
                    string msg = this[prop.Name];
                    if (!string.IsNullOrWhiteSpace(msg))
                    {
                        errors.Add(msg);
                    }
                }
                return string.Join(Environment.NewLine, errors);
            }
        }

        public bool IsValid
        {
            get
            {
                return string.IsNullOrWhiteSpace(Error);
            }
        }

        public string this[string columnName]
        {
            get
            {
                string message = string.Empty;

                switch (columnName)
                {
                    case "Text":
                        if (string.IsNullOrWhiteSpace(this.Text))
                            message = "JobTitle is required.";
                        else if (Common.JobTitles != null && Common.JobTitles.Any(x => x.Text.ToLower().Equals(this.Text.ToLower())))
                            message = "JobTitle existed.";
                        break;
                }


                if (!string.IsNullOrWhiteSpace(message))
                    return message;

                return null;
            }
        }
        #endregion
    }


}
