using QRCoder;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRCodeGenerationApplication.Model
{
    public class CalendarEventsQRCodeModel : QRCodeMainModel
    {
        #region PrivateFields

        private string _title = "";
        private string _description = "";
        private string _location = "";
        private DateTime _start;
        private DateTime _end;
        private bool _allDayEvent;
        private PayloadGenerator.CalendarEvent.EventEncoding _encoding;

        #endregion

        #region PublicFields

        /// <summary>
        /// Title of the calendar event
        /// </summary>
        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged();
            }
        }
        
        /// <summary>
        /// Description of the event
        /// </summary>
        public string Description
        {
            get => _description;
            set
            {
                _description = value; 
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Location (lat:long or address) of the event
        /// </summary>
        public string Location
        {
            get => _location;
            set
            {
                _location = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Start time of the event
        /// </summary>
        public DateTime Start
        {
            get => _start;
            set
            {
                if (End < value)
                {
                    errors[nameof(this.Start)] = "Начало события должен быть раньше или проходить в тот же день, чем конец.";
                    return;
                }
                _start = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// End time of the event
        /// </summary>
        public DateTime End
        {
            get => _end;
            set
            {
                if (Start > value)
                {
                    errors[nameof(this.End)] = "Конец события должен быть позже или проходить в тот же день, чем начало.";
                    return;
                }
                _end = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Is it a full day event?
        /// </summary>
        public bool AllDayEvent
        {
            get => _allDayEvent;
            set
            {
                _allDayEvent = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Type of encoding (universal or iCal)
        /// <para></para>
        /// If you want to place an event to Apple users you should use <b><i>iCal</i></b> instead of <b><i>Universal</i></b> as encoding.
        /// </summary>
        public PayloadGenerator.CalendarEvent.EventEncoding Encoding
        {
            get => _encoding;
            set
            {
                _encoding = value;
                OnPropertyChanged();
            }
        }

        public Array GetEventEncodings
        {
            get => Enum.GetValues(typeof(PayloadGenerator.CalendarEvent.EventEncoding));
        }

        #endregion

        #region OverridePublicFields

        public override string Payload
        {
            get
            {
                return new PayloadGenerator.CalendarEvent(
                    this.Title, 
                    this.Description, 
                    this.Location, 
                    this.Start, 
                    this.End, 
                    this.AllDayEvent, 
                    this.Encoding).ToString();
            }
        }

        #endregion
    }
}
