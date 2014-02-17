﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Toolkit.Base;
using System.Windows.Media;
using System.Globalization;

namespace CPC.POSReport.Model
{
    [Serializable]
    public class ComboItem : NotifyPropertyChangedBase
    {
        public ComboItem(short value, string text)
        {
            Value = value;
            Text = text;
        }
        public ComboItem()
        { }
        private short _value;
        public short Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
            }
        }

        private object _objValue;
        public object ObjValue
        {
            get
            {
                return _objValue;
            }
            set
            {
                _objValue = value;
            }
        }

        private int _intValue;
        public int IntValue
        {
            get
            {
                return _intValue;
            }
            set
            {
                _intValue = value;
            }
        }

        private long _longValue;
        public long LongValue
        {
            get
            {
                return _longValue;
            }
            set
            {
                _longValue = value;
            }
        }

        private string _text;
        public string Text
        {
            get
            {
                return _text;
            }
            set
            {
                if (_text != value)
                {
                    _text = value;
                    OnPropertyChanged(() => Text);
                }
            }
        }

        private bool _flag;
        public bool Flag
        {
            get
            {
                return _flag;
            }
            set
            {
                _flag = value;
            }
        }

        private string _group;
        public string Group
        {
            get
            {
                return _group;
            }
            set
            {
                _group = value;
            }
        }

        private string _symbol;
        public string Symbol
        {
            get
            {
                return _symbol;
            }
            set
            {
                _symbol = value;
            }
        }

        private int _parentId;
        public int ParentId
        {
            get
            {
                return _parentId;
            }
            set
            {
                _parentId = value;
            }
        }

        private object _detail;
        public object Detail
        {
            get
            {
                return _detail;
            }
            set
            {
                _detail = value;
            }
        }

        private bool _islocked;
        public bool Islocked
        {
            get
            {
                return _islocked;
            }
            set
            {
                _islocked = value;
            }
        }

        private bool _hasState;
        public bool HasState
        {
            get
            {
                return _hasState;
            }
            set
            {
                _hasState = value;
            }
        }

        private int _tab;
        public int Tab
        {
            get
            {
                return _tab;
            }
            set
            {
                _tab = value;
            }
        }

        private CultureInfo _cultureInfo;
        /// <summary>
        /// Gets or sets CultureInfo used for language.
        /// </summary>
        public CultureInfo CultureInfo
        {
            get
            {
                return _cultureInfo;
            }
            set
            {
                _cultureInfo = value;
            }
        }

        private string _code;
        /// <summary>
        /// Gets or sets Code used for language.
        /// </summary>
        public string Code
        {
            get
            {
                return _code;
            }
            set
            {
                _code = value;
            }
        }

        private Brush _image;
        /// <summary>
        /// Gets or sets Image used for icon of language.
        /// </summary>
        public Brush Image
        {
            get
            {
                return _image;
            }
            set
            {
                _image = value;
            }
        }

        private string _settingPart;
        /// <summary>
        /// Gets or sets SettingPart.
        /// </summary>
        public string SettingPart
        {
            get
            {
                return _settingPart;
            }
            set
            {
                _settingPart = value;
            }
        }
    }
}