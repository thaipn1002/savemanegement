//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using CPC.POSReport.Database;
using Toolkit.Base;

namespace CPC.POSReport.Model
{
    /// <summary>
    /// Model for table base_CustomField
    /// </summary>
    [Serializable]
    public partial class base_CustomFieldModel : ModelBase, IDataErrorInfo
    {
        #region Constructor

        // Default constructor
        public base_CustomFieldModel()
        {
            this.IsNew = true;
            this.base_CustomField = new base_CustomField();
        }

        // Default constructor that set entity to field
        public base_CustomFieldModel(base_CustomField base_customfield, bool isRaiseProperties = false)
        {
            this.base_CustomField = base_customfield;
            if (!isRaiseProperties)
                this.ToModel();
            else
                this.ToModelAndRaise();
            this.IsDirty = false;
        }

        #endregion

        #region Entity Properties

        public base_CustomField base_CustomField { get; private set; }

        #endregion

        #region Primitive Properties

        protected int _id;
        /// <summary>
        /// Property Model
        /// <para>Gets or sets the Id</para>
        /// </summary>
        public int Id
        {
            get { return this._id; }
            set
            {
                if (this._id != value)
                {
                    this.IsDirty = true;
                    this._id = value;
                    OnPropertyChanged(() => Id);
                    PropertyChangedCompleted(() => Id);
                }
            }
        }

        protected string _mark;
        /// <summary>
        /// Property Model
        /// <para>Gets or sets the Mark</para>
        /// </summary>
        public string Mark
        {
            get { return this._mark; }
            set
            {
                if (this._mark != value)
                {
                    this.IsDirty = true;
                    this._mark = value;
                    OnPropertyChanged(() => Mark);
                    PropertyChangedCompleted(() => Mark);
                }
            }
        }

        protected string _fieldName;
        /// <summary>
        /// Property Model
        /// <para>Gets or sets the FieldName</para>
        /// </summary>
        public string FieldName
        {
            get { return this._fieldName; }
            set
            {
                if (this._fieldName != value)
                {
                    this.IsDirty = true;
                    this._fieldName = value;
                    OnPropertyChanged(() => FieldName);
                    PropertyChangedCompleted(() => FieldName);
                }
            }
        }

        protected bool _isShow;
        /// <summary>
        /// Property Model
        /// <para>Gets or sets the IsShow</para>
        /// </summary>
        public bool IsShow
        {
            get { return this._isShow; }
            set
            {
                if (this._isShow != value)
                {
                    this.IsDirty = true;
                    this._isShow = value;
                    OnPropertyChanged(() => IsShow);
                    PropertyChangedCompleted(() => IsShow);
                }
            }
        }

        protected string _label;
        /// <summary>
        /// Property Model
        /// <para>Gets or sets the Label</para>
        /// </summary>
        public string Label
        {
            get { return this._label; }
            set
            {
                if (this._label != value)
                {
                    this.IsDirty = true;
                    this._label = value;
                    OnPropertyChanged(() => Label);
                    PropertyChangedCompleted(() => Label);
                }
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// <para>Public Method</para>
        /// Method for set IsNew & IsDirty = false;
        /// </summary>
        public void EndUpdate()
        {
            this.IsNew = false;
            this.IsDirty = false;
        }

        /// <summary>
        /// Public Method
        /// <para>Method for set PropertyModel to Entity</para>
        /// </summary>
        public void ToEntity()
        {
            if (IsNew)
                this.base_CustomField.Id = this.Id;
            this.base_CustomField.Mark = this.Mark;
            this.base_CustomField.FieldName = this.FieldName;
            this.base_CustomField.IsShow = this.IsShow;
            this.base_CustomField.Label = this.Label;
        }

        /// <summary>
        /// Public Method
        /// <para>Method for set Entity to PropertyModel</para>
        /// </summary
        public void ToModel()
        {
            this._id = this.base_CustomField.Id;
            this._mark = this.base_CustomField.Mark;
            this._fieldName = this.base_CustomField.FieldName;
            this._isShow = this.base_CustomField.IsShow;
            this._label = this.base_CustomField.Label;
        }

        /// <summary>
        /// Public Method
        /// <para>Method for set Entity to PropertyModel</para>
        /// </summary
        public void ToModelAndRaise()
        {
            this.Id = this.base_CustomField.Id;
            this.Mark = this.base_CustomField.Mark;
            this.FieldName = this.base_CustomField.FieldName;
            this.IsShow = this.base_CustomField.IsShow;
            this.Label = this.base_CustomField.Label;
        }

        #endregion

        #region Custom Code


        #endregion

        #region IDataErrorInfo Members

        public string Error
        {
            get { throw new NotImplementedException(); }
        }

        public string this[string columnName]
        {
            get
            {
                string message = string.Empty;

                switch (columnName)
                {
                    case "Id":
                        break;
                    case "Mark":
                        break;
                    case "FieldName":
                        break;
                    case "IsShow":
                        break;
                    case "Label":
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