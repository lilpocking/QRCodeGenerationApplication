using QRCoder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRCodeGenerationApplication.Model
{
    public class BookmarkQRCodeModel : QRCodeMainModel
    {
        #region PrivateFields

        private string _url = "";
        private string _title = "";

        #endregion

        #region PublicFields

        public string Url { 
            get => _url;
            set { 
                _url = value; 
                OnPropertyChanged();
            }
        }
        public string Title { 
            get => _title;
            set { 
                _title = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region OverridePublicFields

        public override string Payload
        {
            get
            {
                return new PayloadGenerator.Bookmark(this.Url, this.Title).ToString();
            }
        }

        #endregion
    }
}
