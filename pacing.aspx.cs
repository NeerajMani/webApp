using System;
using System.Web.UI;

namespace WorkloadModelUI
{
    public partial class WorkloadModelUI : Page
    {
        protected void CalculateButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(vusersField.Text) && !string.IsNullOrWhiteSpace(tphField.Text) &&
                !string.IsNullOrWhiteSpace(baselineField.Text) && !string.IsNullOrWhiteSpace(thinkTimeField.Text))
            {
               double vusers = double.Parse(vusersField.Text);
              double tph = double.Parse(tphField.Text);
              double scriptExecutionTime = double.Parse(baselineField.Text);
              double thinkTime = double.Parse(thinkTimeField.Text);
              double R = scriptExecutionTime + thinkTime;
              double pacing = ((vusers * 3600) / tph) - (baseline + thinkTime);
              pacingField.Text = pacing.ToString();
              double pacingNew = pacing + R;
              pacingFieldNew.Text = pacingNew.ToString();
            }
            else
            {
                pacingField.Text = "Enter valid inputs.";
                pacingFieldNew.Text = "Enter valid inputs.";
            }
        }

        protected void ClearButton_Click(object sender, EventArgs e)
        {
            vusersField.Text = string.Empty;
            tphField.Text = string.Empty;
            baselineField.Text = string.Empty;
            thinkTimeField.Text = string.Empty;
            pacingField.Text = string.Empty;
            pacingFieldNew.Text = string.Empty;
        }
    }
}
