using Syncfusion.SfDataGrid.XForms;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace DomoticApp.Customs
{
    public class CustomGridStyle : DataGridStyle
    {
        public CustomGridStyle()
        {

        }

        public override Color GetRecordForegroundColor()
        {
            return Color.FromHex("#1E619A");
        }

        public override Color GetSelectionBackgroundColor()
        {
            return Color.DarkGray;
        }
    }
}
