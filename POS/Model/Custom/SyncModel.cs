using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CPC.Toolkit.Base;
using System.ComponentModel;

namespace CPC.POS.Model
{
    [Serializable]
    public class SyncModel : ModelBase
    {
        #region Ctor
        public SyncModel()
        {

        }
        #endregion

        #region Properties

        #region Id
        private object _id;
        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        public object Id
        {
            get { return _id; }
            set
            {
                if (_id != value)
                {
                    _id = value;
                    IsDirty = true;
                    OnPropertyChanged(() => Id);
                }
            }
        }
        #endregion

        #region Resource
        private Guid _resource;
        /// <summary>
        /// Gets or sets the Resource.
        /// </summary>
        public Guid Resource
        {
            get { return _resource; }
            set
            {
                if (_resource != value)
                {
                    _resource = value;
                    IsDirty = true;
                    OnPropertyChanged(() => Resource);
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

                    _text = value.Replace(",", "");
                    IsDirty = true;
                    OnPropertyChanged(() => Text);
                }
            }
        }
        #endregion

        #region CreatedDate
        private DateTime _createdDate;
        /// <summary>
        /// Gets or sets the CreatedDate.
        /// </summary>
        public DateTime CreatedDate
        {
            get { return _createdDate; }
            set
            {
                if (_createdDate != value)
                {
                    _createdDate = value;
                    IsDirty = true;
                    OnPropertyChanged(() => CreatedDate);
                }
            }
        }
        #endregion

        #region Name
        private string _name;
        /// <summary>
        /// Gets or sets the Name.
        /// </summary>
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged(() => Name);
                }
            }
        }
        #endregion

        #region Content
        private object _content;
        /// <summary>
        /// Gets or sets the Content.
        /// </summary>
        public object Content
        {
            get { return _content; }
            set
            {
                if (_content != value)
                {
                    _content = value;
                    OnPropertyChanged(() => Content);
                }
            }
        }
        #endregion

        #endregion
    }
}
