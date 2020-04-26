namespace Web.UI.Forms.Reports.Controls
{
    public interface IComboControl
    {
        void Select(object id);
        void SetLabel(string newLabel);
        Ext.Net.ComboBox GetComboBox();
    }
}