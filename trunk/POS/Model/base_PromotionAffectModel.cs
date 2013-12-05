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
using System.Linq;
using System.Linq.Expressions;
using CPC.Helper;
using CPC.POS.Database;
using CPC.Toolkit.Base;

namespace CPC.POS.Model
{
    /// <summary>
    /// Model for table base_PromotionAffect
    /// </summary>
    [Serializable]
    public partial class base_PromotionAffectModel : ModelBase, IDataErrorInfo
    {
        #region Constructor

        // Default constructor
        public base_PromotionAffectModel()
        {
            this.IsNew = true;
            this.base_PromotionAffect = new base_PromotionAffect();
        }

        // Default constructor that set entity to field
        public base_PromotionAffectModel(base_PromotionAffect base_promotionaffect, bool isRaiseProperties = false)
        {
            this.base_PromotionAffect = base_promotionaffect;
            if (!isRaiseProperties)
                this.ToModel();
            else
                this.ToModelAndRaise();
            this.IsDirty = false;
        }

        #endregion

        #region Entity Properties

        public base_PromotionAffect base_PromotionAffect { get; private set; }

        #endregion

        #region Primitive Properties

        protected int _id;
        /// <summary>
        /// Property Model
        /// <param>Gets or sets the Id</param>
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

        protected int _promotionId;
        /// <summary>
        /// Property Model
        /// <param>Gets or sets the PromotionId</param>
        /// </summary>
        public int PromotionId
        {
            get { return this._promotionId; }
            set
            {
                if (this._promotionId != value)
                {
                    this.IsDirty = true;
                    this._promotionId = value;
                    OnPropertyChanged(() => PromotionId);
                    PropertyChangedCompleted(() => PromotionId);
                }
            }
        }

        protected long _itemId;
        /// <summary>
        /// Property Model
        /// <param>Gets or sets the ItemId</param>
        /// </summary>
        public long ItemId
        {
            get { return this._itemId; }
            set
            {
                if (this._itemId != value)
                {
                    this.IsDirty = true;
                    this._itemId = value;
                    OnPropertyChanged(() => ItemId);
                    PropertyChangedCompleted(() => ItemId);
                }
            }
        }

        protected decimal _price1;
        /// <summary>
        /// Property Model
        /// <param>Gets or sets the Price1</param>
        /// </summary>
        public decimal Price1
        {
            get { return this._price1; }
            set
            {
                if (this._price1 != value)
                {
                    this.IsDirty = true;
                    this._price1 = value;
                    OnPropertyChanged(() => Price1);
                    PropertyChangedCompleted(() => Price1);
                }
            }
        }

        protected decimal _price2;
        /// <summary>
        /// Property Model
        /// <param>Gets or sets the Price2</param>
        /// </summary>
        public decimal Price2
        {
            get { return this._price2; }
            set
            {
                if (this._price2 != value)
                {
                    this.IsDirty = true;
                    this._price2 = value;
                    OnPropertyChanged(() => Price2);
                    PropertyChangedCompleted(() => Price2);
                }
            }
        }

        protected decimal _price3;
        /// <summary>
        /// Property Model
        /// <param>Gets or sets the Price3</param>
        /// </summary>
        public decimal Price3
        {
            get { return this._price3; }
            set
            {
                if (this._price3 != value)
                {
                    this.IsDirty = true;
                    this._price3 = value;
                    OnPropertyChanged(() => Price3);
                    PropertyChangedCompleted(() => Price3);
                }
            }
        }

        protected decimal _price4;
        /// <summary>
        /// Property Model
        /// <param>Gets or sets the Price4</param>
        /// </summary>
        public decimal Price4
        {
            get { return this._price4; }
            set
            {
                if (this._price4 != value)
                {
                    this.IsDirty = true;
                    this._price4 = value;
                    OnPropertyChanged(() => Price4);
                    PropertyChangedCompleted(() => Price4);
                }
            }
        }

        protected decimal _price5;
        /// <summary>
        /// Property Model
        /// <param>Gets or sets the Price5</param>
        /// </summary>
        public decimal Price5
        {
            get { return this._price5; }
            set
            {
                if (this._price5 != value)
                {
                    this.IsDirty = true;
                    this._price5 = value;
                    OnPropertyChanged(() => Price5);
                    PropertyChangedCompleted(() => Price5);
                }
            }
        }

        protected decimal _discount1;
        /// <summary>
        /// Property Model
        /// <param>Gets or sets the Discount1</param>
        /// </summary>
        public decimal Discount1
        {
            get { return this._discount1; }
            set
            {
                if (this._discount1 != value)
                {
                    this.IsDirty = true;
                    this._discount1 = value;
                    OnPropertyChanged(() => Discount1);
                    PropertyChangedCompleted(() => Discount1);
                }
            }
        }

        protected decimal _discount2;
        /// <summary>
        /// Property Model
        /// <param>Gets or sets the Discount2</param>
        /// </summary>
        public decimal Discount2
        {
            get { return this._discount2; }
            set
            {
                if (this._discount2 != value)
                {
                    this.IsDirty = true;
                    this._discount2 = value;
                    OnPropertyChanged(() => Discount2);
                    PropertyChangedCompleted(() => Discount2);
                }
            }
        }

        protected decimal _discount3;
        /// <summary>
        /// Property Model
        /// <param>Gets or sets the Discount3</param>
        /// </summary>
        public decimal Discount3
        {
            get { return this._discount3; }
            set
            {
                if (this._discount3 != value)
                {
                    this.IsDirty = true;
                    this._discount3 = value;
                    OnPropertyChanged(() => Discount3);
                    PropertyChangedCompleted(() => Discount3);
                }
            }
        }

        protected decimal _discount4;
        /// <summary>
        /// Property Model
        /// <param>Gets or sets the Discount4</param>
        /// </summary>
        public decimal Discount4
        {
            get { return this._discount4; }
            set
            {
                if (this._discount4 != value)
                {
                    this.IsDirty = true;
                    this._discount4 = value;
                    OnPropertyChanged(() => Discount4);
                    PropertyChangedCompleted(() => Discount4);
                }
            }
        }

        protected decimal _discount5;
        /// <summary>
        /// Property Model
        /// <param>Gets or sets the Discount5</param>
        /// </summary>
        public decimal Discount5
        {
            get { return this._discount5; }
            set
            {
                if (this._discount5 != value)
                {
                    this.IsDirty = true;
                    this._discount5 = value;
                    OnPropertyChanged(() => Discount5);
                    PropertyChangedCompleted(() => Discount5);
                }
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// <param>Public Method</param>
        /// Method for set IsNew & IsDirty = false;
        /// </summary>
        public void EndUpdate()
        {
            this.IsNew = false;
            this.IsDirty = false;
        }

        /// <summary>
        /// Public Method
        /// <param>Method for set PropertyModel to Entity</param>
        /// </summary>
        public void ToEntity()
        {
            if (IsNew)
                this.base_PromotionAffect.Id = this.Id;
            this.base_PromotionAffect.PromotionId = this.PromotionId;
            this.base_PromotionAffect.ItemId = this.ItemId;
            this.base_PromotionAffect.Price1 = this.Price1;
            this.base_PromotionAffect.Price2 = this.Price2;
            this.base_PromotionAffect.Price3 = this.Price3;
            this.base_PromotionAffect.Price4 = this.Price4;
            this.base_PromotionAffect.Price5 = this.Price5;
            this.base_PromotionAffect.Discount1 = this.Discount1;
            this.base_PromotionAffect.Discount2 = this.Discount2;
            this.base_PromotionAffect.Discount3 = this.Discount3;
            this.base_PromotionAffect.Discount4 = this.Discount4;
            this.base_PromotionAffect.Discount5 = this.Discount5;
        }

        /// <summary>
        /// Public Method
        /// <param>Method for set Entity to PropertyModel</param>
        /// </summary
        public void ToModel()
        {
            this._id = this.base_PromotionAffect.Id;
            this._promotionId = this.base_PromotionAffect.PromotionId;
            this._itemId = this.base_PromotionAffect.ItemId;
            this._price1 = this.base_PromotionAffect.Price1;
            this._price2 = this.base_PromotionAffect.Price2;
            this._price3 = this.base_PromotionAffect.Price3;
            this._price4 = this.base_PromotionAffect.Price4;
            this._price5 = this.base_PromotionAffect.Price5;
            this._discount1 = this.base_PromotionAffect.Discount1;
            this._discount2 = this.base_PromotionAffect.Discount2;
            this._discount3 = this.base_PromotionAffect.Discount3;
            this._discount4 = this.base_PromotionAffect.Discount4;
            this._discount5 = this.base_PromotionAffect.Discount5;
        }

        /// <summary>
        /// Public Method
        /// <param>Method for set Entity to PropertyModel</param>
        /// </summary
        public void ToModelAndRaise()
        {
            this.Id = this.base_PromotionAffect.Id;
            this.PromotionId = this.base_PromotionAffect.PromotionId;
            this.ItemId = this.base_PromotionAffect.ItemId;
            this.Price1 = this.base_PromotionAffect.Price1;
            this.Price2 = this.base_PromotionAffect.Price2;
            this.Price3 = this.base_PromotionAffect.Price3;
            this.Price4 = this.base_PromotionAffect.Price4;
            this.Price5 = this.base_PromotionAffect.Price5;
            this.Discount1 = this.base_PromotionAffect.Discount1;
            this.Discount2 = this.base_PromotionAffect.Discount2;
            this.Discount3 = this.base_PromotionAffect.Discount3;
            this.Discount4 = this.base_PromotionAffect.Discount4;
            this.Discount5 = this.base_PromotionAffect.Discount5;
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
                    case "PromotionId":
                        break;
                    case "ItemId":
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